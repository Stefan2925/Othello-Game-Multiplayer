using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proiect.Forms
{
    public partial class Retea : Form
    {
        public Retea()
        {
            InitializeComponent();
            this.FormClosing += Retea_FormClosing;
        }

        private void Retea_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }

        private void BtnConnectClick(object sender, EventArgs e)
        {
            string serverIp = textBoxServerIp.Text;
            if (!string.IsNullOrEmpty(serverIp))
            {
                Multiplayer multiplayer = new Multiplayer(false, serverIp);
                multiplayer.FormClosed += (s, args) => this.Close();
                multiplayer.Show();
                this.Hide();
            }
        }

        private void BtnHostClick(object sender, EventArgs e)
        {
            Multiplayer multiplayer = new Multiplayer(true, null);
            multiplayer.FormClosed += (s, args) => this.Close();
            multiplayer.Show();
            this.Hide();
        }

        private void InitializeComponent()
        {
            this.textBoxServerIp = new TextBox();
            this.buttonConnect = new Button();
            this.buttonHost = new Button();
            this.SuspendLayout();
            //
            // textBoxServerIp
            //
            this.textBoxServerIp.Location = new Point(57, 21);
            this.textBoxServerIp.Name = "textBoxServerIp";
            this.textBoxServerIp.Size = new Size(342, 22);
            this.textBoxServerIp.TabIndex = 0;
            //
            // buttonConnect
            //
            this.buttonConnect.BackColor = Color.LightBlue;
            this.buttonConnect.Location = new Point(57, 59);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new Size(160, 37);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "CONNECT";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += BtnConnectClick;
            //
            // buttonHost
            //
            this.buttonHost.BackColor = Color.LightGreen;
            this.buttonHost.Location = new Point(240, 59);
            this.buttonHost.Name = "buttonHost";
            this.buttonHost.Size = new Size(160, 37);
            this.buttonHost.TabIndex = 2;
            this.buttonHost.Text = "HOST GAME";
            this.buttonHost.UseVisualStyleBackColor = false;
            this.buttonHost.Click += BtnHostClick;
            //
            // Retea (this form)
            //
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(549, 149);
            this.Controls.Add(this.buttonHost);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxServerIp);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Retea";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Retea";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private TextBox textBoxServerIp;
        private Button buttonConnect;
        private Button buttonHost;

        
    }
}
