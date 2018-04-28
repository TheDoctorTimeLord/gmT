using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel
{
    public static class MapManager
    {
        public static void MoveCreature(Map map, Point oldPosition, Point newPosition)
        {
            var creature = map.Cells[oldPosition.X, oldPosition.Y].Creature;
            map.Cells[oldPosition.X, oldPosition.Y].Creature = null;
            map.Cells[newPosition.X, newPosition.Y].Creature = creature;
        }

        public static bool InBounds(Map map, Point position)
        {
            return
                position.X >= 0 &&
                position.Y >= 0 &&
                position.X < map.Cells.GetLength(0) &&
                position.Y < map.Cells.GetLength(1);
        }

        public static void FillMap(Map map, IEnumerable<string> content)
        {
            throw new NotImplementedException();
        }
    }
}
