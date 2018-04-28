using System.Collections.Generic;

namespace GameThief.GameModel.MapSourse
{
    public class Map
    {
        public Cell[,] Cells;

        public void CreateMap(int width, int height, IEnumerable<string> content)
        {
            Cells = new Cell[width, height];
        }

        public Cell this[int i, int j] { get { return Cells[i, j]; } }
    }
}
