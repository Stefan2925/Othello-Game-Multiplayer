using Proiect.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proiect
{
    public partial class GameWindow : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        protected OthelloGame game;

        public GameWindow()
        {
            InitializeComponent();
            game = new OthelloGame(culoareJucator.Negru, Panou);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (game != null && Panou != null)
            {
                Panou.Paint -= game.Afiseaza;
                Panou.MouseClick -= game.Click;
            }

            base.OnFormClosing(e);
        }

        private void UpdateScore()
        {
            int scorN = 0;
            int scorA = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piesa piesa = game.Tabla.GetPiesa(i, j);
                    if (piesa != null)
                    {
                        if (piesa.Color == culoareJucator.Negru)
                        {
                            scorN++;
                        }
                        else if (piesa.Color == culoareJucator.Alb)
                        {
                            scorA++;
                        }
                    }
                }
            }

            scorNegru.Text = scorN.ToString();
            scorAlb.Text = scorA.ToString();
        }

        private void GameResize(object sender, EventArgs e)
        {
            
            butonDeIesire.Left = ClientSize.Width - butonDeIesire.Width - 20;
            butonDeIesire.Top = (ClientSize.Height - butonDeIesire.Height) / 2;

            
            int maxSize = (int)(Math.Min(ClientSize.Width, ClientSize.Height * 0.75) * 0.85);
            Panou.Width = maxSize;
            Panou.Height = maxSize;
            Panou.Left = (ClientSize.Width - maxSize) / 2;
            Panou.Top = 30;

          
            int scoreTop = ClientSize.Height - 95;
            int scoreCenterX = ClientSize.Width / 2;

          
            PanouNegru.Left = scoreCenterX - 130;
            PanouNegru.Top = scoreTop + 4;
            scorNegru.Left = PanouNegru.Right + 5;
            scorNegru.Top = scoreTop + 8;

           
            PanouAlb.Left = scoreCenterX + 55;
            PanouAlb.Top = scoreTop + 4;
            scorAlb.Left = PanouAlb.Right + 5;
            scorAlb.Top = scoreTop + 8;
        }

        private void BackMenu(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BoardPanel(object sender, EventArgs e)
        {
            // Metodă goală - event handler pentru panou
        }

        private void VerificaLaSecunda_Tick(object sender, EventArgs e)
        {
            UpdateScore();
        }
    }
}