using Proiect.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial class GameWindow : Form
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
        private OthelloGame game;
      
        public GameWindow()
        {
            InitializeComponent();
         
            
           
            game = new OthelloGame( culoareJucator.Black, Panou);

           
            Panou.Invalidate();

        }
      
        private void Game_Resize(object sender, EventArgs e)
        {
            button1.Left = this.ClientSize.Width - button1.Width ; 
            button1.Top = (this.ClientSize.Height - button1.Height) / 2;

            int maxSize = (int)(Math.Min(this.ClientSize.Width, this.ClientSize.Height) * 0.8);

            Panou.Width = maxSize;
            Panou.Height = maxSize;
            Panou.Left = (this.ClientSize.Width - maxSize) / 2;
            Panou.Top = (this.ClientSize.Height - maxSize) / 2;
        }






        private void Back_Menu(object sender, EventArgs e)
        {

            Application.Exit();
        }


      


        private void Board_Panel(object sender, EventArgs e)
        {

        }
    }
}