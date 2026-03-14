using System.Drawing;

namespace Proiect
{
    public class PiesaAlba : Piesa
    {
        public PiesaAlba() : base(CuloareJucator.Alb)
        {

        }

        public override void Deseneaza(Graphics g, int x, int y, int size)
        {
            Image img = Properties.Resources.white_piece;
            g.DrawImage(img, x, y, size, size);
        }
    }
}
