using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel
{
    public class Map
    {
        
        public Cell[,] Cells;
        public List<Noise>[,] Noises;

        public void CreateMap(int width, int height, IEnumerable<string> content)
        {
            Noises = new List<Noise>[width, height];
            Cells = new Cell[width, height];
            MapManager.FillMap(this, content);
        }
    }
}
