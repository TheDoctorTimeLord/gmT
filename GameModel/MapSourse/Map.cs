using System.Collections.Generic;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel.MapSourse
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
