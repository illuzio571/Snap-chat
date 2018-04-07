namespace Snap_chat
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlOne = new System.Windows.Forms.Panel();
            this.pnlTwo = new System.Windows.Forms.Panel();
            this.pnlThree = new System.Windows.Forms.Panel();
            this.pnlFour = new System.Windows.Forms.Panel();
            this.pnlFive = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblChars = new System.Windows.Forms.Label();
            this.lblCharLimit = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnSpecialChars = new System.Windows.Forms.Button();
            this.btnTweet = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlOne
            // 
            this.pnlOne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOne.Location = new System.Drawing.Point(0, 0);
            this.pnlOne.Name = "pnlOne";
            this.pnlOne.Size = new System.Drawing.Size(150, 300);
            this.pnlOne.TabIndex = 0;
            // 
            // pnlTwo
            // 
            this.pnlTwo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTwo.Location = new System.Drawing.Point(150, 0);
            this.pnlTwo.Name = "pnlTwo";
            this.pnlTwo.Size = new System.Drawing.Size(150, 300);
            this.pnlTwo.TabIndex = 0;
            // 
            // pnlThree
            // 
            this.pnlThree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlThree.Location = new System.Drawing.Point(300, 0);
            this.pnlThree.Name = "pnlThree";
            this.pnlThree.Size = new System.Drawing.Size(150, 300);
            this.pnlThree.TabIndex = 0;
            // 
            // pnlFour
            // 
            this.pnlFour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFour.Location = new System.Drawing.Point(450, 0);
            this.pnlFour.Name = "pnlFour";
            this.pnlFour.Size = new System.Drawing.Size(150, 300);
            this.pnlFour.TabIndex = 0;
            // 
            // pnlFive
            // 
            this.pnlFive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFive.Location = new System.Drawing.Point(600, 0);
            this.pnlFive.Name = "pnlFive";
            this.pnlFive.Size = new System.Drawing.Size(150, 300);
            this.pnlFive.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 314);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Message:";
            // 
            // lblMessage
            // 
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Location = new System.Drawing.Point(16, 343);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(709, 68);
            this.lblMessage.TabIndex = 2;
            // 
            // lblChars
            // 
            this.lblChars.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChars.Location = new System.Drawing.Point(24, 427);
            this.lblChars.Name = "lblChars";
            this.lblChars.Size = new System.Drawing.Size(27, 23);
            this.lblChars.TabIndex = 3;
            this.lblChars.Text = "0";
            this.lblChars.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCharLimit
            // 
            this.lblCharLimit.AutoSize = true;
            this.lblCharLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharLimit.Location = new System.Drawing.Point(46, 428);
            this.lblCharLimit.Name = "lblCharLimit";
            this.lblCharLimit.Size = new System.Drawing.Size(40, 20);
            this.lblCharLimit.TabIndex = 3;
            this.lblCharLimit.Text = "/280";
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(571, 426);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 23);
            this.btnRestart.TabIndex = 4;
            this.btnRestart.Text = "&Reset";
            this.btnRestart.UseVisualStyleBackColor = true;
            // 
            // btnSpecialChars
            // 
            this.btnSpecialChars.Location = new System.Drawing.Point(450, 426);
            this.btnSpecialChars.Name = "btnSpecialChars";
            this.btnSpecialChars.Size = new System.Drawing.Size(115, 23);
            this.btnSpecialChars.TabIndex = 4;
            this.btnSpecialChars.Text = "&Special Characters";
            this.btnSpecialChars.UseVisualStyleBackColor = true;
            // 
            // btnTweet
            // 
            this.btnTweet.Location = new System.Drawing.Point(650, 426);
            this.btnTweet.Name = "btnTweet";
            this.btnTweet.Size = new System.Drawing.Size(75, 23);
            this.btnTweet.TabIndex = 4;
            this.btnTweet.Text = "&Tweet";
            this.btnTweet.UseVisualStyleBackColor = true;
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(103, 423);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(129, 32);
            this.btnRecord.TabIndex = 5;
            this.btnRecord.Text = "RECORD";
            this.btnRecord.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(250, 423);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(144, 32);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 461);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnSpecialChars);
            this.Controls.Add(this.btnTweet);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.lblCharLimit);
            this.Controls.Add(this.lblChars);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlFive);
            this.Controls.Add(this.pnlFour);
            this.Controls.Add(this.pnlTwo);
            this.Controls.Add(this.pnlThree);
            this.Controls.Add(this.pnlOne);
            this.Name = "frmMain";
            this.Text = "Snap-chat";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlOne;
        private System.Windows.Forms.Panel pnlTwo;
        private System.Windows.Forms.Panel pnlThree;
        private System.Windows.Forms.Panel pnlFour;
        private System.Windows.Forms.Panel pnlFive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblChars;
        private System.Windows.Forms.Label lblCharLimit;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnSpecialChars;
        private System.Windows.Forms.Button btnTweet;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStop;
    }
}

