using System.Drawing;

namespace Proiect
{
    public class PiesaNeagra : Piesa
    {
        public PiesaNeagra() : base(CuloareJucator.Negru)
        {

        }

        public override void Deseneaza(Graphics g, int x, int y, int size)
        {
            Image img = Properties.Resources.black_piece;
            g.DrawImage(img, x, y, size, size);
        }
    }
}
