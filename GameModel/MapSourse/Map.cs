using System.Collections.Generic;

namespace GameThief.GameModel.MapSourse
{
    public class Map
    {
        public Cell[,] Cells;

        public Map(int width, int height)
        {
            Cells = new Cell[width, height];
        }

        public Cell this[int i, int j] { get { return Cells[i, j]; } }
    }
}
