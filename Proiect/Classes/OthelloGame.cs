using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    internal class OthelloGame
    {
        private int linieSelectata= -1;
        private int coloanaSelectata = -1;

        const int MARGINE = 30;
   

        private readonly (int x, int y)[] directions =
        {

                (-1,-1), (-1,0), (-1,1),
                (0,-1),         (0,1),
                (1,-1), (1,0), (1,1)


            };
        private Panel Panou;
        private Tabla tabla;
        private  culoareJucator culoareCurenta;
        Graphics g;

        public OthelloGame( culoareJucator currentPlayer,Panel Panou)
        {

            this.tabla = new Tabla ();
            this.culoareCurenta = currentPlayer;
            this.Panou = Panou;

            Panou.Paint += _Paint;
            Panou.MouseClick += Click;

        }



        public bool MutareValida(int x, int y)
        {
            if (!CuprindeTabla(x, y) || tabla.Grid[x, y] != null)
            {
              
                return false;
            }

            foreach (var (dx, dy) in directions)
            {
                int i = x + dx;
                int j = y + dy;
                bool foundOpponent = false;

                while (CuprindeTabla(i, j) && tabla.Grid[i, j] != null &&
                       tabla.Grid[i, j].Color != culoareCurenta)
                {
                    foundOpponent = true;
                    i += dx;
                    j += dy;
                }

                if (foundOpponent && CuprindeTabla(i, j) &&
                    tabla.Grid[i, j] != null &&
                    tabla.Grid[i, j].Color == culoareCurenta)
                    return true;
            }
            return false;
        }


        private bool CuprindeTabla(int x, int y)
        {
            return x >= 0 && x < tabla.Grid.GetLength(0) &&
                   y >= 0 && y < tabla.Grid.GetLength(1);

        }

        public bool VerificaEndJoc()
        {
            
            
            List < (int, int)> toateMiscarileCurente= ToateMutarile();
            if(toateMiscarileCurente.Count==0)
            {
                SchimbaCuloareCurenta();
                toateMiscarileCurente = ToateMutarile();
                if (toateMiscarileCurente.Count == 0)
                {
                    return true;
                }
                else { return false; }
               
            }
            return false;
            
            

        }

        public void PunePiesa(int x, int y)
        {


            if (culoareCurenta == culoareJucator.White)
                tabla.Grid[x, y] = new PiesaAlba();
            else tabla.Grid[x, y] = new PiesaNeagra();

            foreach (var (dx, dy) in directions)
                InverseazaDirectie(x, y, dx, dy);
           
            SchimbaCuloareCurenta();


            //bool end = VerificaEndJoc();
            //if (end == true)
            //    Application.Exit();
            

        }


        private void SchimbaCuloareCurenta()
        {
            if (culoareCurenta == culoareJucator.White)
            {
                culoareCurenta = culoareJucator.Black;

            }
            else
            {
                culoareCurenta = culoareJucator.White;
            }
        }
        public List<(int x, int y)>ToateMutarile()
        {
           List< (int, int )> moves = new List<(int, int)>();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (MutareValida(x, y))
                        moves.Add((x, y));
                }
            }

            return moves;
        }

        private void InverseazaDirectie(int x, int y, int dx, int dy)
        {
            int i = x + dx;
            int j = y + dy;


            while (CuprindeTabla(i, j) && tabla.Grid[i, j] != null &&
                   tabla.Grid[i, j].Color != culoareCurenta)
            {
                i += dx;
                j += dy;
            }


            if (!CuprindeTabla(i, j) || tabla.Grid[i, j] == null ||
                tabla.Grid[i, j].Color != culoareCurenta)
                return;


            i -= dx;
            j -= dy;

            while (!(i == x && j == y))
            {
               
                    if(culoareCurenta == culoareJucator.Black)
                {
                    tabla.Grid[i, j] = new PiesaNeagra();
                }
                else { tabla.Grid[i, j] = new PiesaAlba(); }
                    i -= dx;
                j -= dy;
            }




        }


        private void Click(object sender, MouseEventArgs e)
        {
            int usableSize = Panou.Width - 2 * MARGINE;
            int tileSize = usableSize / 8;

            int l = (e.Y - MARGINE) / tileSize; 
            int c = (e.X - MARGINE) / tileSize;  


            if (l >= 0 && l < 8 && c >= 0 && c < 8)
            {
                linieSelectata = l;
                coloanaSelectata = c;
                    

                
                if (MutareValida(l, c))
                    PunePiesa(l, c);
                Panou.Invalidate();

            }
        }

       


        private void _Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
           
  
            int usableSize = Panou.Width - 2 * MARGINE;
            int tileSize = usableSize / 8;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piesa piece = tabla.Grid[r, c];
                    int baseX = MARGINE;
                    int baseY = MARGINE;

                    if (piece != null)
                    {
                        

                        float scale = 0.8f;
                        int targetSize = (int)(tileSize * scale);

                        int offsetX = baseX + c * tileSize + (tileSize - targetSize) / 2;
                        int offsetY = baseY + r * tileSize + (tileSize - targetSize) / 2;

                        piece.Draw(g, offsetX, offsetY, targetSize);
                    }
                }
            }

            if (linieSelectata != -1 && coloanaSelectata != -1)
            {
                int x = MARGINE + coloanaSelectata * tileSize;
                int y = MARGINE + linieSelectata * tileSize;

                using (Pen p = new Pen(Color.Yellow, 3))
                {
                    g.DrawRectangle(p, x, y+5, tileSize, tileSize);
                }
            }

        }

    }
}


