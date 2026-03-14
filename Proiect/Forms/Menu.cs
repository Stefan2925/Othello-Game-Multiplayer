using System;
using System.Windows.Forms;
using Proiect.Forms;

namespace Proiect
{
    public partial class Menu : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x02000000;
                return createParams;
            }
        }

        public Menu()
        {
            InitializeComponent();
            Resize += MenuResize;
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuResize(object sender, EventArgs e)
        {
            int spatiu = 20;
            int latimeEcran = butonOnline.Width + buttonOffline.Width + butonDeExit.Width + spatiu * 2;
            int startX = (ClientSize.Width - latimeEcran) / 2;
            int startY = (ClientSize.Height - butonOnline.Height) / 2;

            butonOnline.Left = startX;
            buttonOffline.Left = startX + butonOnline.Width + spatiu;
            butonDeExit.Left = startX + butonOnline.Width + spatiu + buttonOffline.Width + spatiu;

            butonOnline.Top = startY;
            buttonOffline.Top = startY;
            butonDeExit.Top = startY;
        }

        private void ButtonOnlineClick(object sender, EventArgs e)
        {
            Retea retea = new Retea();
            retea.Show();
            Hide();
        }

        private void ButtonOfflineClick(object sender, EventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.ShowDialog();
            Close();
        }
    }
}
