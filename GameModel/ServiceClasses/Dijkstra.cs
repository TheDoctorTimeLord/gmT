using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel.ServiceClasses
{
    //class DijkstraData
    //{
    //    public Point Position { get; set; }
    //    public int Value { get; set; }

    //    public DijkstraData(Point position, int value)
    //    {
    //        Position = position;
    //        Value = value;
    //    }
    //}

    public static class Dijkstra
    {
        public static void DijkstraTraversal(Point[] graph, Point start, Action<Point, int> action)
        {
            var notVisited = graph.ToList();
            //var track = new Dictionary<Point, DijkstraData> {[start] = new DijkstraData(new Point(-1, -1), 0)};
            var maxSupressions = new Dictionary<Point, int>
            {
                [start] = MapManager.Map[start.X, start.Y].ObjectContainer.TotalNoiseSuppression
            };

            while (true)
            {
                var toOpen = new Point(-1, -1);
                var bestSupr = int.MaxValue;
                foreach (var e in notVisited)
                {
                    if (maxSupressions.ContainsKey(e) && maxSupressions[e] > bestSupr)
                    {
                        bestSupr = maxSupressions[e];
                        toOpen = e;
                    }
                }

                if (toOpen == new Point(-1, -1))
                    break;
                //if (toOpen == end) break;

                foreach (var e in toOpen.FindNeighbours().Where(p => MapManager.InBounds(p)))
                {
                    var currentPrice = maxSupressions[toOpen]
                                       - MapManager.Map[toOpen.X, toOpen.Y].ObjectContainer.TotalNoiseSuppression
                                       + MapManager.Map[e.X, e.Y].ObjectContainer.TotalNoiseSuppression - 1;
                    //var nextNode = e.OtherNode(toOpen);
                    if (!maxSupressions.ContainsKey(e) || maxSupressions[e] > currentPrice)
                    {
                        maxSupressions[e] = currentPrice;
                    }
                }

                notVisited.Remove(toOpen);
            }

            //var result = new Dictionary<Point, int>();
            //result = track.Values.ToDictionary(d => d.)
            //result.Reverse();
            //return result;

            foreach (var pair in maxSupressions)
            {
                action(pair.Key, pair.Value);
            }
        }
    }
}
