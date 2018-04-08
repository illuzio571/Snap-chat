﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snap_chat
{
    public partial class frmMain : Form
    {
        const int THRESHHOLD = 30000;
        const int AUDIO_BUFFER = 200;
        const int TOLERANCE = 10000;

        WaveInEvent waveIn = new WaveInEvent();
        WaveFileWriter writer = null;

        //Output folder is the Desktop of the user.
        string outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
        string outputFilePath = String.Empty;
        int snaps = 0;
        bool isClosing = false;

        public frmMain()
        {
            InitializeComponent();

            //Create the output folder
            Directory.CreateDirectory(outputFolder);
            outputFilePath = Path.Combine(outputFolder, "testRecording.wav");

            //Add a DataAvailable function which stops the recording if it's over 30s long
            waveIn.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 30)
                {
                    waveIn.StopRecording();
                }
            };

            //Add a RecordingStopped function that closes and ends the recording, then writes log files
            waveIn.RecordingStopped += (s, a) =>
            {
                writer?.Dispose();
                writer = null;
                if (isClosing)
                {
                    waveIn.Dispose();
                }

                WriteLongFile("\\longFile.csv");

                WriteTestFile("\\~testFile.csv");

                WriteTestFileWithSnaps("\\~testFileWithSnaps.csv");

                Console.WriteLine("Number of Snaps: " + snaps);

                snaps = 0;
            };
        }

        #region Write Files
        private void WriteLongFile(string fileName)
        {
            using (WaveFileReader reader = new WaveFileReader(outputFilePath))
            {
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

                List<String> lines = new List<String>();
                for (int i = 1; i < sampleBuffer.Length; i++)
                {
                    if (sampleBuffer[i] > 0)
                    {
                        lines.Add(sampleBuffer[i].ToString());
                    }
                }
                using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(outputFolder + fileName))
                {
                    foreach (string line in lines)
                    {
                        file.WriteLine(line);
                    }
                }
            };
        }

        private void WriteTestFile(string fileName)
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

                List<String> lines = new List<String>();
                //Loop through each byte in the array from the .wav file
                for (int i = 1; i < sampleBuffer.Length; i++)
                {
                    //If byte is bigger than threshhold and has not recently snapped
                    if ((sampleBuffer[i] - sampleBuffer[i - 1]) > TOLERANCE && !hasRecentlySnapped && sampleBuffer[i] > 0)
                    {
                        hasRecentlySnapped = true;
                        lines.Add(sampleBuffer[i].ToString());
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
                using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(outputFolder + fileName))
                {
                    foreach (string line in lines)
                    {
                        file.WriteLine(line);
                    }
                }
            }
        }

        private void WriteTestFileWithSnaps(string fileName)
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

                List<String> lines = new List<String>();
                //Loop through each byte in the array from the .wav file
                for (int i = 1; i < sampleBuffer.Length; i++)
                {
                    //If the difference between two bytes is bigger than the tolerance, is positive, and has not recently snapped
                    if ((sampleBuffer[i] - sampleBuffer[i - 1]) > TOLERANCE && !hasRecentlySnapped && sampleBuffer[i] > 0)
                    {
                        hasRecentlySnapped = true;
                        lines.Add(sampleBuffer[i].ToString() + " - Snapped");
                        snaps++;
                    }
                    else
                    {
                        if (sampleBuffer[i] > 0)
                        {
                            lines.Add(sampleBuffer[i].ToString());
                        }
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
                using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(outputFolder + fileName))
                {
                    foreach (string line in lines)
                    {
                        file.WriteLine(line);
                    }
                }
            }
        }
        #endregion

        #region Record / Stop Test Functions
        private void btnRecord_Click(object sender, EventArgs e)
        {
            writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
            waveIn.StartRecording();
            btnRecord.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnRecord.Enabled = true;
            btnStop.Enabled = false;
            waveIn.StopRecording();
        }
        #endregion

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            waveIn.StopRecording();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
                waveIn.StartRecording();
                btnRecord.Enabled = false;
                btnStop.Enabled = true;
            }
            else if (e.KeyCode == Keys.E)
            {
                btnRecord.Enabled = true;
                btnStop.Enabled = false;
                waveIn.StopRecording();
            }
        }
    }
}
