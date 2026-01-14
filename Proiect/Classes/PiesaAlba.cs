using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class PiesaAlba : Piesa
    {
        public PiesaAlba() : base(culoareJucator.Alb)
        {

        }

        public override void Deseneaza(Graphics g, int x, int y, int size)
        {
            Image img = Properties.Resources.white_piece;
            g.DrawImage(img, x, y, size, size);
        }
    }
}
