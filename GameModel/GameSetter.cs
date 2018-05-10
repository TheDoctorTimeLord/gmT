using System;
using System.Collections.Generic;
using System.Drawing;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel
{
    public static class GameSetter
    {
        public static void SetSampleMap(int width, int height)
        {
            var content = new List<List<string>>();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    content.Add(new List<string> { "." });
                }
            }
            MapManager.CreateMap(width, height, content);
        }

        public static void SetMapAndFillWithDecors(int width, int height, List<Tuple<Point, IDecor>> decors)
        {
            SetSampleMap(width, height);

            foreach (var tuple in decors)
            {
                if (MapManager.InBounds(tuple.Item1))
                    MapManager.Map[tuple.Item1.X, tuple.Item1.Y].ObjectContainer.AddDecor(tuple.Item2);
            }
        }
    }
}
