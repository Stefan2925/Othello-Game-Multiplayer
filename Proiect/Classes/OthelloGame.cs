using Proiect.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proiect
{
    public class OthelloGame
    {
       
        private List<(int x, int y)> mutariValide = new List<(int x, int y)>();

        private const int margine = 30;
        private CuloareJucator culoareCurenta;

        private readonly (int x, int y)[] directii = new (int x, int y)[]
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1),          (0, 1),
            (1, -1), (1, 0),  (1, 1)
        };

        private readonly Panel panou;
        private readonly Tabla tabla;
        private Graphics grafici;

      

        public OthelloGame(CuloareJucator jucatorCurent, Panel panou)
        {
            tabla = new Tabla();
            culoareCurenta = jucatorCurent;
            this.panou = panou;

            panou.Paint += Afiseaza;
            panou.MouseClick += Click;
            AflaMiscariValide();
        }


        #region Scaneaza Tabla
        public void AflaMiscariValide()
        {
            Multiplayer multiplayer = FindMultiplayer();

            if (multiplayer == null || multiplayer.EsteTuraMea)
            {
                mutariValide = ToateMutarile();
            }
            else
            {
                mutariValide.Clear();
            }

            if (multiplayer == null || multiplayer.EsteTuraMea)
            {
                panou.Invalidate();
            }
        }



        public bool MutareValida(int x, int y)
        {
            if (!CuprindeTabla(x, y) || !tabla.EsteLiber(x, y))
            {
                return false;
            }

            foreach (var (dx, dy) in directii)
            {
                int i = x + dx;
                int j = y + dy;
                bool inamicGasit = false;

                while (CuprindeTabla(i, j) && tabla.GetPiesa(i, j) != null &&
                       tabla.GetPiesa(i, j).Color != culoareCurenta)
                {
                    inamicGasit = true;
                    i += dx;
                    j += dy;
                }

                if (inamicGasit && CuprindeTabla(i, j) &&
                    tabla.GetPiesa(i, j) != null &&
                    tabla.GetPiesa(i, j).Color == culoareCurenta)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion Scaneaza Tabla

        #region Multiplayer
        private Multiplayer FindMultiplayer()
        {
            Control parent = panou.Parent;
            while (parent != null)
            {
                if (parent is Multiplayer multiplayer)
                {
                    return multiplayer;
                }
                parent = parent.Parent;
            }
            return null;
        }
        #endregion Multiplayer

        #region Logica joc

        private bool CuprindeTabla(int x, int y)
        {
            return x >= 0 && x < 8 && y >= 0 && y < 8;
        }



        public void PunePiesa(int x, int y)
        {

            if (culoareCurenta == CuloareJucator.Alb)
            {
                tabla.SetPiesa(x, y, new PiesaAlba());
            }
            else
            {
                tabla.SetPiesa(x, y, new PiesaNeagra());
            }

            foreach (var (dx, dy) in directii)
            {
                InverseazaDirectie(x, y, dx, dy);
            }


            SchimbaCuloareCurenta();


            List<(int, int)> mutariAdversar = ToateMutarile();

            if (mutariAdversar.Count == 0)
            {
                SchimbaCuloareCurenta();
                List<(int, int)> mutariCurent = ToateMutarile();


                if (mutariCurent.Count == 0)
                {
                    panou.Invalidate();
                    MessageBox.Show("Joc terminat!");
                    return;
                }
            }

            AflaMiscariValide();
            panou.Invalidate();
        }




        private void SchimbaCuloareCurenta()
        {
            if (culoareCurenta == CuloareJucator.Alb)
            {
                culoareCurenta = CuloareJucator.Negru;
            }
            else
            {
                culoareCurenta = CuloareJucator.Alb;
            }
        }

        public List<(int x, int y)> ToateMutarile()
        {
            var mutari = new List<(int x, int y)>();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (MutareValida(x, y))
                    {
                        mutari.Add((x, y));
                    }
                }
            }
            return mutari;
        }

        private void InverseazaDirectie(int x, int y, int dx, int dy)
        {
            int i = x + dx;
            int j = y + dy;

            while (CuprindeTabla(i, j) && tabla.GetPiesa(i, j) != null &&
                   tabla.GetPiesa(i, j).Color != culoareCurenta)
            {
                i += dx;
                j += dy;
            }

            if (!CuprindeTabla(i, j) || tabla.GetPiesa(i, j) == null ||
                tabla.GetPiesa(i, j).Color != culoareCurenta)
            {
                return;
            }

            i -= dx;
            j -= dy;

            while (i != x || j != y)
            {
                if (culoareCurenta == CuloareJucator.Negru)
                {
                    tabla.SetPiesa(i, j, new PiesaNeagra());
                }
                else
                {
                    tabla.SetPiesa(i, j, new PiesaAlba());
                }
                i -= dx;
                j -= dy;
            }
        }
        #endregion Logica joc

        #region Events
        public void Click(object sender, MouseEventArgs e)
        {
            int tablaExacta = panou.Width - 2 * margine;
            int patratel = tablaExacta / 8;
            int linie = (e.Y - margine) / patratel;
            int coloana = (e.X - margine) / patratel;

            if (linie >= 0 && linie < 8 && coloana >= 0 && coloana < 8 && MutareValida(linie, coloana))
            {
                Multiplayer multiplayer = FindMultiplayer();
                if (multiplayer == null || multiplayer.EsteTuraMea)
                {
                    PunePiesa(linie, coloana);
                    multiplayer?.TrimiteMutare(linie, coloana);
                }
            }
        }

        public void Afiseaza(object sender, PaintEventArgs e)
        {
            grafici = e.Graphics;
            int tablaExacta = panou.Width - 2 * margine;
            int patratel = tablaExacta / 8;


            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piesa piesa = tabla.GetPiesa(r, c);
                    if (piesa != null)
                    {
                        double raportEcran = 0.8;
                        int tinta = (int)(patratel * raportEcran);
                        int offsetX = margine + c * patratel + (patratel - tinta) / 2 ;
                        int offsetY = margine + r * patratel + (patratel - tinta) / 2;

                        piesa.Deseneaza(grafici, offsetX, offsetY, tinta);
                    }
                }
            }



            if (mutariValide.Count > 0)
            {
                Multiplayer multiplayer = FindMultiplayer();
                bool showHighlights = multiplayer == null || multiplayer.EsteTuraMea;

                if (showHighlights)
                {
                    foreach (var (x, y) in mutariValide)
                    {
                        int px = margine + y * patratel;
                        int py = margine + x * patratel;

                        using (Brush brush = new SolidBrush(Color.FromArgb(120, 32, 178, 170)))
                        {
                            grafici.FillRectangle(brush, px + 3, py + 3, patratel - 6, patratel - 6);
                        }

                        using (Pen pen = new Pen(Color.FromArgb(180, 46, 125, 50), 2))
                        {
                            grafici.DrawRectangle(pen, px, py, patratel, patratel);
                        }
                    }
                }
            }
        }


        #endregion Events

        #region Gettere & Settere

        public CuloareJucator CuloareCurenta
        {
            get { return culoareCurenta; }
        }

        public Tabla Tabla
        {
            get { return tabla; }
        }

        public bool EsteTuraMea(CuloareJucator jucatorMeu)
        {
            return culoareCurenta == jucatorMeu;
        }

        #endregion Gettere & Settere
    }
}
