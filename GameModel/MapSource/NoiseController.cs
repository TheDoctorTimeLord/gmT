using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.Managers;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MapSource
{
    public class NoiseController
    {
        public readonly HashSet<Noise>[,] Noises;

        public NoiseController(int width, int height)
        {
            Noises = new HashSet<Noise>[width, height];

            for (var i = 0; i < width; i++) 
            for (var j = 0; j < height; j++)
            {
                Noises[i, j] = new HashSet<Noise>();
            }
        }

        public void AddNoiseSource(NoiseSource source)
        {
            TemporaryObjectsManager.AddTemporaryObject(source);
            Dijkstra.DijkstraTraversal(Noises, source, (noises, noise) =>
            {
                noises.Add(noise);
            });
        }

        public void RemoveSourceNoises(NoiseSource source)
        {
            Dijkstra.DijkstraTraversal(Noises, source, (noises, noise) =>
            {
                if (noises.Contains(noise))
                    noises.Remove(noise);
            });
        }
    }
}
