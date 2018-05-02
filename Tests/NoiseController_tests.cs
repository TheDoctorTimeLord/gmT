using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.Managers;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class NoiseController_tests
    {
        public void SetMap(int width, int height, List<Tuple<Point, IDecor>> decors)
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

            foreach (var tuple in decors)
            {
                if (MapManager.InBounds(tuple.Item1))
                    MapManager.Map[tuple.Item1.X, tuple.Item1.Y].ObjectContainer.AddDecor(tuple.Item2);
            }
        }
    }
}
