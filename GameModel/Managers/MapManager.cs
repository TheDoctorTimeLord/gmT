using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.MapSourse;

namespace GameThief.GameModel.Managers
{
    public static class MapManager
    {
        public static Map Map;
        public static NoiseController NoiseController;

        public static void CreateMap(int width, int height, IEnumerable<string> content)
        {
            Map = new Map(width, height);
            FillMap(content);
        }

        public static void MoveCreature(Point oldPosition, Point newPosition)
        {
            if (Map[newPosition.X, newPosition.Y].Creature != null || 
                Map[newPosition.X, newPosition.Y].Object.IsSolid)
                throw new ArgumentException();

            var creature = Map[oldPosition.X, oldPosition.Y].Creature;
            Map[oldPosition.X, oldPosition.Y].Creature = null;
            Map[newPosition.X, newPosition.Y].Creature = creature;
        }

        public static void AddCreatureFromMap(ICreature creature)
        {
            var creaturePosition = creature.GetPosition();
            Map[creaturePosition.X, creaturePosition.Y].Creature = creature;
        }

        public static void RemoveCreatureFromMap(ICreature creature)
        {
            var creaturePosition = creature.GetPosition();
            Map[creaturePosition.X, creaturePosition.Y].Creature = null;
        }

        public static void UpdateNoises() => NoiseController.UpdateNoises(Map);

        public static void AddNoiseSourse(NoiseSource source) => NoiseController.AddNoiseSourse(Map, source);

        public static IEnumerable<Cell> GetVisibleCells(Point position, Direction sightDirection, int rangeVisibility)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Noise> GetAudibleNoises(Point position, int delicacyHearing, int thresholdAudibility)
        { 
            var result = new List<Noise>();
            var isFirst = true;
            Noise previous = null;

            foreach (var noise in NoiseController
                .Noises[position.X, position.Y]
                .OrderBy(n => n))
            {
                if (isFirst)
                {
                    result.Add(noise);
                    previous = noise;
                    isFirst = false;
                    continue;
                }
                
                if (noise.Intensity - previous.Intensity < delicacyHearing)
                    break;
                
                result.Add(noise);
                previous = noise;
            }

            return result.Where(n => n.Intensity >= thresholdAudibility);
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
