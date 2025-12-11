using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial  class Menu : Form
    {
        protected override CreateParams CreateParams
        { 
        get
            {

                CreateParams handleparam = base.CreateParams;
                handleparam.ExStyle |= 0x02000000;
                return handleparam;

            }
        }
        public Menu()
        {

            InitializeComponent();
            

        

            this.Resize += Form1_Resize;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
            int spacing = 20; 
            int totalWidth = button1.Width + button2.Width + button3.Width + spacing * 2;

            
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int startY = (this.ClientSize.Height - button1.Height) / 2; 

            button1.Left = startX;
            button2.Left = startX + button1.Width + spacing;
            button3.Left = startX + button1.Width + spacing + button2.Width + spacing;

            button1.Top = startY;
            button2.Top = startY;
            button3.Top = startY;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Button_Offline(object sender, EventArgs e)
        {
            GameWindow joc = new GameWindow();
            joc.ShowDialog();
            this.Close(); 
        }
    }
}
