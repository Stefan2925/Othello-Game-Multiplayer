using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
   
        public enum culoareJucator{ Negru, Alb }

    public abstract class Piesa
    {
        private culoareJucator color;

        public culoareJucator Color
        {
            get { return color; }
            set { color = value; }
        }
       

        public Piesa(culoareJucator color)
        {
            Color = color;
        }
        public abstract void Deseneaza(Graphics g,int x,int y,int size);
    }

  
  
}
