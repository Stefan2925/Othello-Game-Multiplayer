using System.Drawing;

namespace Proiect
{
   
        public enum CuloareJucator{ Negru, Alb }

    public abstract class Piesa
    {
        private CuloareJucator color;

        public CuloareJucator Color
        {
            get { return color; }
            set { color = value; }
        }
       

        public Piesa(CuloareJucator color)
        {
            Color = color;
        }
        public abstract void Deseneaza(Graphics g,int x,int y,int size);
    }

  
  
}
