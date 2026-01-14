using System.Drawing;
using System.Windows.Forms;

namespace Proiect
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.butonDeExit = new Button();
            this.buttonOffline = new Button();
            this.butonOnline = new Button();
            SuspendLayout();

            butonDeExit.BackColor = Color.DarkGoldenrod;
            butonDeExit.FlatStyle = FlatStyle.Flat;
            butonDeExit.Font = new Font("Sylfaen", 16.2F, FontStyle.Bold);
            butonDeExit.ForeColor = SystemColors.InfoText;
            butonDeExit.Name = "button3";
            butonDeExit.Size = new Size(197, 80);
            butonDeExit.TabIndex = 2;
            butonDeExit.Text = "Exit";
            butonDeExit.UseVisualStyleBackColor = false;
            butonDeExit.Click += ButtonExitClick;
            
            buttonOffline.BackColor = Color.Gold;
            buttonOffline.FlatStyle = FlatStyle.Flat;
            buttonOffline.Font = new Font("Sylfaen", 16.2F, FontStyle.Bold);
            buttonOffline.ForeColor = SystemColors.InfoText;
            buttonOffline.Name = "button2";
            buttonOffline.Size = new Size(197, 83);
            buttonOffline.TabIndex = 1;
            buttonOffline.Text = "Play Offline";
            buttonOffline.UseVisualStyleBackColor = false;
            buttonOffline.Click += ButtonOfflineClick;

            butonOnline.BackColor = Color.Orange;
            butonOnline.FlatStyle = FlatStyle.Flat;
            butonOnline.Font = new Font("Sylfaen", 16.2F, FontStyle.Bold);
            butonOnline.ForeColor = SystemColors.InfoText;
            butonOnline.Name = "PlayOnline";
            butonOnline.Size = new Size(197, 84);
            butonOnline.TabIndex = 0;
            butonOnline.Text = "Play Online";
            butonOnline.UseVisualStyleBackColor = false;
            butonOnline.Click += ButtonOnlineClick;

            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.yes;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1472, 750);
            Controls.Add(butonDeExit);
            Controls.Add(buttonOffline);
            Controls.Add(butonOnline);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Menu";
            Text = "Othello Menu";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            Resize += MenuResize;

            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butonDeExit;
        private System.Windows.Forms.Button buttonOffline;
        private System.Windows.Forms.Button butonOnline;
    }
}

