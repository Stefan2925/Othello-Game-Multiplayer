
namespace Proiect
{
    public class Tabla
    {
        private readonly Piesa[,] grid;

        public Tabla()
        {
            grid = new Piesa[8, 8];
            grid[3, 3] = new PiesaAlba();
            grid[4, 4] = new PiesaAlba();
            grid[3, 4] = new PiesaNeagra();
            grid[4, 3] = new PiesaNeagra();
        }

        public Piesa GetPiesa(int x, int y)
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                return grid[x, y];
            }
            return null;
        }

        public void SetPiesa(int x, int y, Piesa piesa)
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                grid[x, y] = piesa;
            }
        }

        public bool EsteLiber(int x, int y)
        {
            return GetPiesa(x, y) == null;
        }

       
    }
}
