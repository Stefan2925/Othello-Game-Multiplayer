using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proiect
{
    partial class GameWindow
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
            this.components = new System.ComponentModel.Container();
            this.PanouNegru = new System.Windows.Forms.PictureBox();
            this.scorNegru = new System.Windows.Forms.Label();
            this.PanouAlb = new System.Windows.Forms.PictureBox();
            this.scorAlb = new System.Windows.Forms.Label();
            this.butonDeIesire = new System.Windows.Forms.Button();
            this.Panou = new System.Windows.Forms.Panel();
            this.VerificaLaSecunda = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PanouNegru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanouAlb)).BeginInit();
            this.SuspendLayout();
            // 
            // PanouNegru
            // 
            this.PanouNegru.BackColor = System.Drawing.Color.Transparent;
            this.PanouNegru.Image = global::Proiect.Properties.Resources.black_piece;
            this.PanouNegru.Location = new System.Drawing.Point(100, 600);
            this.PanouNegru.Name = "PanouNegru";
            this.PanouNegru.Size = new System.Drawing.Size(64, 64);
            this.PanouNegru.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PanouNegru.TabIndex = 2;
            this.PanouNegru.TabStop = false;
            // 
            // scorNegru
            // 
            this.scorNegru.Location = new System.Drawing.Point(0, 0);
            this.scorNegru.Name = "scorNegru";
            this.scorNegru.Size = new System.Drawing.Size(100, 23);
            this.scorNegru.TabIndex = 3;
            // 
            // PanouAlb
            // 
            this.PanouAlb.BackColor = System.Drawing.Color.Transparent;
            this.PanouAlb.Image = global::Proiect.Properties.Resources.white_piece;
            this.PanouAlb.Location = new System.Drawing.Point(100, 600);
            this.PanouAlb.Name = "PanouAlb";
            this.PanouAlb.Size = new System.Drawing.Size(64, 64);
            this.PanouAlb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PanouAlb.TabIndex = 4;
            this.PanouAlb.TabStop = false;
            // 
            // scorAlb
            // 
            this.scorAlb.Location = new System.Drawing.Point(0, 0);
            this.scorAlb.Name = "scorAlb";
            this.scorAlb.Size = new System.Drawing.Size(100, 23);
            this.scorAlb.TabIndex = 5;
            // 
            // butonDeIesire
            // 
            this.butonDeIesire.BackColor = System.Drawing.Color.DarkRed;
            this.butonDeIesire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butonDeIesire.Font = new System.Drawing.Font("Sylfaen", 12F);
            this.butonDeIesire.ForeColor = System.Drawing.SystemColors.InfoText;
            this.butonDeIesire.Location = new System.Drawing.Point(663, 173);
            this.butonDeIesire.Name = "butonDeIesire";
            this.butonDeIesire.Size = new System.Drawing.Size(125, 118);
            this.butonDeIesire.TabIndex = 0;
            this.butonDeIesire.Text = "Exit";
            this.butonDeIesire.UseVisualStyleBackColor = false;
            this.butonDeIesire.Click += new System.EventHandler(this.BackMenu);
            // 
            // Panou
            // 
            this.Panou.BackgroundImage = global::Proiect.Properties.Resources.Board_Panel;
            this.Panou.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panou.Location = new System.Drawing.Point(203, 82);
            this.Panou.Name = "Panou";
            this.Panou.Size = new System.Drawing.Size(615, 404);
            this.Panou.TabIndex = 1;
            // 
            // VerificaLaSecunda
            // 
            this.VerificaLaSecunda.Enabled = true;
            this.VerificaLaSecunda.Tick += new System.EventHandler(this.VerificaLaSecunda_Tick);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Proiect.Properties.Resources.Background_game;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1199, 785);
            this.Controls.Add(this.PanouNegru);
            this.Controls.Add(this.scorNegru);
            this.Controls.Add(this.PanouAlb);
            this.Controls.Add(this.scorAlb);
            this.Controls.Add(this.Panou);
            this.Controls.Add(this.butonDeIesire);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameWindow";
            this.Text = "Othello Game";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.GameResize);
            ((System.ComponentModel.ISupportInitialize)(this.PanouNegru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanouAlb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butonDeIesire;
        protected System.Windows.Forms.Panel Panou;
        private PictureBox PanouNegru;
        private Label scorNegru;
        private PictureBox PanouAlb;
        private Label scorAlb;
        private Timer VerificaLaSecunda;
    }
}