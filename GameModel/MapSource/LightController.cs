using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.MapSource
{
    public class LightController
    {
        private readonly HashSet<LightSource>[,] lightCoverage;

        public LightController(int width, int height)
        {
            lightCoverage = new HashSet<LightSource>[width, height];

            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
            {
                lightCoverage[i, j] = new HashSet<LightSource>();
            }
        }

        public void AddLightSource(LightSource source)
        {
            foreach (var point in source.GetScope())
            {
                lightCoverage[point.X, point.Y].Add(source);
            }
        }

        public void RemoveLightSource(LightSource source)
        {
            foreach (var point in source.GetScope())
            {
                lightCoverage[point.X, point.Y].Remove(source);
            }
        }

        public bool this[int i, int j]
        {
            get { return lightCoverage[i, j].Count > 0; }
        }
    }
}
