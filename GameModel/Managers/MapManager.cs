using System;
using System.Collections.Generic;
using System.Drawing;
using GameThief.GameModel.MapSourse;

namespace GameThief.GameModel.Managers
{
    public static class MapManager
    {
        public static Map Map;
        public static NoiseController NoiseController;

        public static void MoveCreature(Point oldPosition, Point newPosition)
        {
            if (Map[newPosition.X, newPosition.Y].Creature != null || 
                Map[newPosition.X, newPosition.Y].Object.IsSolid)
                throw new ArgumentException();

            var creature = Map[oldPosition.X, oldPosition.Y].Creature;
            Map[oldPosition.X, oldPosition.Y].Creature = null;
            Map[newPosition.X, newPosition.Y].Creature = creature;
        }

        public static void RemoveCreature(ICreature creature)
        {
            var creaturePosition = creature.GetPosition();
            Map[creaturePosition.X, creaturePosition.Y].Creature = null;
        }

        public static bool InBounds(Point position)
        {
            return
                position.X >= 0 &&
                position.Y >= 0 &&
                position.X < Map.Cells.GetLength(0) &&
                position.Y < Map.Cells.GetLength(1);
        }

        public static void FillMap(IEnumerable<string> content)
        {
            throw new NotImplementedException();
        }
    }
}
