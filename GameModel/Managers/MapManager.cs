using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.MapSource;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.Managers
{
    public static class MapManager
    {
        public static Map Map;
        public static NoiseController NoiseController;

        public static void CreateMap(int width, int height, List<List<string>> content)
        {
            Map = new Map(width, height);
            FillMap(content);
        }

        public static void MoveCreature(ICreature creature, Point newPosition)
        {
            if (Map[newPosition.X, newPosition.Y].Creature != null || 
                Map[newPosition.X, newPosition.Y].ObjectContainer.IsSolid)
                throw new ArgumentException();
            
            var oldPosition = creature.GetPosition();
            creature.ChangePosition(newPosition);
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

        public static void AddNoiseSourse(NoiseSource source) => NoiseController.AddNoiseSource(source);

        public static List<Cell> GetVisibleCells(Point position, Direction sightDirection, int rangeVisibility)
        {
            throw new NotImplementedException();
        }

        public static List<Noise> GetAudibleNoises(Point position, int delicacyHearing, int thresholdAudibility)
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

            return result
                .Where(n => n.Intensity >= thresholdAudibility)
                .ToList();
        }

        public static bool InBounds(Point position)
        {
            return
                position.X >= 0 &&
                position.Y >= 0 &&
                position.X < Map.Cells.GetLength(0) &&
                position.Y < Map.Cells.GetLength(1);
        }

        public static void FillMap(List<List<string>> content)
        {
            for (var i = 0; i < content.Count; i++)
            {
                var background = content[i][0];
                var newCell = new Cell(background);

                foreach (var dec in content[i].Skip(1))
                    newCell.ObjectContainer.AddDecor(ObjectsContainer.ParseDecor(dec));

                Map[i % Map.Wigth, i / Map.Height] = newCell;
            }
        }
    }
}
