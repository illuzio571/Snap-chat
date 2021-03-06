﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterOAuth;

namespace Snap_chat
{
    public partial class frmMain : Form
    {
        const int AUDIO_BUFFER = 200;
        const int TOLERANCE = 4500;
        const int MAX_CHARACTERS = 255;

        WaveInEvent waveIn = new WaveInEvent();
        WaveFileWriter writer = null;

        //Output folder is the Desktop of the user.
        string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
        string outputFilePath = String.Empty;

        List<Panel> panels = new List<Panel>();
        Panel selectedPanel;
        string message = "";
        int selectedGroup = 0;
        int snaps = 0;
        bool isClosing = false;
        bool canSnap = false;

        private void ReadSnaps()
        {
            using (WaveFileReader reader = new WaveFileReader(outputFilePath))
            {
                bool hasRecentlySnapped = false;
                int numberOfSkippedBytes = 0;

                byte[] buffer = new byte[reader.Length];
                int read = reader.Read(buffer, 0, buffer.Length);
                short[] preSampleBuffer = new short[read / 2];
                Buffer.BlockCopy(buffer, 0, preSampleBuffer, 0, read);

                //Make a new sample buffer of only positive values
                short[] sampleBuffer = new short[read / 2];
                int sampleBufferIndex = 0;

                //Loop through the preSampleBuffer that has negative values, and only add positive values to the new array
                for (int i = 0; i < preSampleBuffer.Length; i++)
                {
                    if (preSampleBuffer[i] > 0)
                    {
                        sampleBuffer[sampleBufferIndex] = preSampleBuffer[i];
                        sampleBufferIndex++;
                    }
                }

                //Loop through each byte in the array from the .wav file
                for (int i = 3; i < sampleBuffer.Length; i++)
                {
                    //If the difference between two bytes is bigger than the tolerance, is positive, and has not recently snapped
                    int minimum = Math.Min(sampleBuffer[i - 1], sampleBuffer[i - 2]);
                    minimum = Math.Min(minimum, sampleBuffer[i - 3]);
                    if ((sampleBuffer[i] - minimum) > TOLERANCE && !hasRecentlySnapped)
                    {
                        hasRecentlySnapped = true;
                        snaps++;
                    }

                    if (hasRecentlySnapped)
                    {
                        numberOfSkippedBytes++;
                    }

                    if (numberOfSkippedBytes > AUDIO_BUFFER)
                    {
                        hasRecentlySnapped = false;
                        numberOfSkippedBytes = 0;
                    }
                }
            }
        }

        #region Form Events
        public frmMain()
        {
            InitializeComponent();

            //Create the output folder
            Directory.CreateDirectory(outputFolder);
            outputFilePath = Path.Combine(outputFolder, "testRecording.wav");

            //Add a DataAvailable function which stops the recording if it's over 30s long
            WaveIn_DataAvailable();

            //Add a RecordingStopped function that closes and ends the recording, then writes log files
            WaveIn_RecordingStopped();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            waveIn.StopRecording();
        }

        private void Reset(bool hardReset)
        {
            if (hardReset)
            {
                canSnap = false;
                tmrCheckForSnaps.Stop();
                pbTime.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    pbTime.Value = 0;
                    pbTime.Maximum = 50;
                    lblAlert.Visible = false;
                    btnStart.Visible = true;
                });
                SetPanelVisiblity(true);
            }
            panels = new List<Panel>();
            selectedGroup = 0;
            selectedPanel = null;
            snaps = 0;
        }

        private void SetPanelVisiblity(bool makeVisible)
        {
            foreach (Panel panel in panels)
            {
                if (panel != selectedPanel)
                {
                    panel.Invoke((MethodInvoker)delegate
                    {
                        // Running on the UI thread
                        if (makeVisible)
                        {
                            panel.Visible = true;
                        }
                        else
                        {
                            panel.Visible = false;
                        }
                    });
                }
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Reset(true);
            SetPanelVisiblity(true);
        }

        private void tmrCheckForSnaps_Tick(object sender, EventArgs e)
        {
            txtMessage.Text = message;
            lblChars.Text = message.Length.ToString();
            lblCharLimit.Text = "/ " + MAX_CHARACTERS.ToString();

            if (pbTime.Value < pbTime.Maximum)
            {
                pbTime.Value++;
                pbTime.Value--;
                pbTime.Value++;
            }
            else
            {
                if (!canSnap)
                {
                    canSnap = true;
                    lblAlert.ForeColor = Color.Green;
                    lblAlert.Text = "SNAP!";
                    pbTime.Maximum = 50;
                    writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
                    waveIn.StartRecording();
                }
                else
                {
                    canSnap = false;
                    lblAlert.ForeColor = Color.Red;
                    lblAlert.Text = "WAIT!";
                    pbTime.Maximum = 10;
                    waveIn.StopRecording();
                }
                pbTime.Value = 0;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            canSnap = true;
            tmrCheckForSnaps.Start();
            lblAlert.ForeColor = Color.Green;
            lblAlert.Text = "SNAP!";
            lblAlert.Visible = true;
            btnStart.Visible = false;
        }
        #endregion

        #region WaveIn Functions
        private void WaveIn_DataAvailable()
        {
            waveIn.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 30)
                {
                    waveIn.StopRecording();
                }
            };
        }

        private void WaveIn_RecordingStopped()
        {
            waveIn.RecordingStopped += (s, a) =>
            {
                //Dispose of the WaveFileWriter
                writer?.Dispose();
                writer = null;
                if (isClosing)
                {
                    waveIn.Dispose();
                }

                ReadSnaps();

                Console.WriteLine("Number of Snaps: " + snaps);

                //Select what letter group we need
                if (snaps > 0)
                {
                    //If we haven't selected a group yet
                    if (selectedGroup == 0)
                    {
                        SelectLetterGroup(snaps);
                    }
                    else
                    {
                        SelectLetter(snaps);
                        Reset(false);
                    }
                }

                snaps = 0;
            };
        }
        #endregion

        #region Select Letter
        private void SelectLetterGroup(int snaps)
        {
            //Grab all the panels on the form for some manipulation
            foreach (Control control in this.Controls)
            {
                if (control.GetType().Equals(typeof(Panel)))
                {
                    Panel panel = (Panel)(control);
                    panels.Add(panel);
                }
            }

            //If the snaps are less than 5 (the max), make the group == snaps; If they go over, just default to 5
            if (snaps <= 5)
            {
                selectedGroup = snaps;
            }
            else
            {
                selectedGroup = 5;
            }

            switch (selectedGroup)
            {
                case 1:
                    selectedPanel = pnl1;
                    break;
                case 2:
                    selectedPanel = pnl2;
                    break;
                case 3:
                    selectedPanel = pnl3;
                    break;
                case 4:
                    selectedPanel = pnl4;
                    break;
                case 5:
                    selectedPanel = pnl5;
                    break;
                default:
                    selectedPanel = null;
                    break;
            }

            SetPanelVisiblity(false);
        }

        private void SelectLetter(int snaps)
        {
            //Grab a list of all the labels in a group for manipulation
            List<Label> labels = new List<Label>();
            String selectedLetter;
            int letterIndex;

            foreach (Label label in selectedPanel.Controls)
            {
                labels.Add(label);
            }

            if (snaps <= labels.Count)
            {
                letterIndex = snaps;
            }
            else
            {
                letterIndex = labels.Count;
            }

            foreach (Label label in labels)
            {
                //If that index is the one we want, print
                int index = Convert.ToInt32(label.Tag);
                if (index == letterIndex)
                {
                    //Print out a letter
                    selectedLetter = label.Text.ToLower();
                    message += selectedLetter;

                    SetPanelVisiblity(true);
                    break;
                }
            }
        }

        private async void btnTweet_Click(object sender, EventArgs e)
        {
            await SendTweet(message);
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            txtMessage.SelectionStart = txtMessage.Text.Length;
            message = txtMessage.Text;
        }
        #endregion

        async Task<string> SendTweet(string message)
        {
            var twitter = new TwitterApi("FCp5NOLNvHXYFukLAEWcGqPDK", "0sFYlIdAPAdapJBnrm3QUru7c148FIOgPiUsq96ZwlxdFyQmQN",
                "983005254835953664-e4ZhiDoCJvWp3q8bbwsTNUzh6OpfsUS", "OfMixTMJrEddA4Xnu79mCDk7u5jfD6wnPoaFPwDaVzuW1");
            var response = await twitter.Tweet("👏 " + message + " 👏 @hackpsu #snapchat");
            return null;
        }
    }
}
