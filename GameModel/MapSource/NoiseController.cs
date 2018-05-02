using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.Managers;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MapSource
{
    public class NoiseController
    {
        public readonly Map<HashSet<Noise>> Noises;

        public NoiseController(int width, int height)
        {
            Noises = new Map<HashSet<Noise>>(width, height);
        }

        public void AddNoiseSource(NoiseSource source)
        {
            TemporaryObjectsManager.AddTemporaryObject(source);
            Dijkstra.DijkstraTraversal(Noises.Cells, source, (noises, noise) =>
            {
                noises.Add(noise);
            });
        }

        public void RemoveSourceNoises(NoiseSource source)
        {
            Dijkstra.DijkstraTraversal(Noises.Cells, source, (noises, noise) =>
            {
                if (noises.Contains(noise))
                    noises.Remove(noise);
            });
        }
    }
}
