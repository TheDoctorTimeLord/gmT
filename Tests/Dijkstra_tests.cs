﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;
using GameThief.GameModel.ServiceClasses;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class Dijkstra_tests
    {
        public HashSet<Noise>[,] GetMap(int width, int height)
        {
            var result = new HashSet<Noise>[width, height];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    result[x, y] = new HashSet<Noise>();
                }
            }
            return result;
        }

        public void CheckMap(HashSet<Noise>[,] mapResult, HashSet<Noise>[,] mapAnswer)
        {
            for (var y = 0; y < mapResult.GetLength(1); y++)
            {
                for (var x = 0; x < mapResult.GetLength(0); x++)
                {
                    CollectionAssert.AreEqual(mapResult[x, y], mapAnswer[x, y]);
                }
            }
        }

        public void ChangeMap(HashSet<Noise>[,] map, params Tuple<Point, Noise>[] noises)
        {
            foreach (var tuple in noises)
            {
                map[tuple.Item1.X, tuple.Item1.Y].Add(tuple.Item2);
            }
        }

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

        [Test]
        public void TestCreateNoiseSource()
        {
            var width = 3;
            var height = 3;
            SetMap(width, height, new List<Tuple<Point, IDecor>>());
            var map = GetMap(width, height);
            var sourse = new NoiseSource(NoiseType.Guard, 1, 2, new Point(1, 1), "");
            Dijkstra.DijkstraTraversal(map, sourse, (noises, noise) =>
                {
                    noises.Add(noise);
                });
            var mapAnswer = GetMap(width, height);
            ChangeMap(mapAnswer, 
                Tuple.Create(new Point(0, 0), new Noise(sourse, 1)),
                Tuple.Create(new Point(0, 1), new Noise(sourse, 1)),
                Tuple.Create(new Point(0, 2), new Noise(sourse, 1)),
                Tuple.Create(new Point(1, 0), new Noise(sourse, 1)),
                Tuple.Create(new Point(1, 1), new Noise(sourse, 2)),
                Tuple.Create(new Point(1, 2), new Noise(sourse, 1)),
                Tuple.Create(new Point(2, 0), new Noise(sourse, 1)),
                Tuple.Create(new Point(2, 1), new Noise(sourse, 1)),
                Tuple.Create(new Point(2, 2), new Noise(sourse, 1)));
            CheckMap(map, mapAnswer);
        }

        [Test]
        public void TestCreateNoiseSourceWithHighIntensity()
        {
            var width = 3;
            var height = 3;
            SetMap(width, height, new List<Tuple<Point, IDecor>>());
            var map = GetMap(width, height);
            var sourse = new NoiseSource(NoiseType.Guard, 1, 7, new Point(1, 1), "");
            Dijkstra.DijkstraTraversal(map, sourse, (noises, noise) =>
            {
                noises.Add(noise);
            });
            var mapAnswer = GetMap(width, height);
            ChangeMap(mapAnswer,
                Tuple.Create(new Point(0, 0), new Noise(sourse, 6)),
                Tuple.Create(new Point(0, 1), new Noise(sourse, 6)),
                Tuple.Create(new Point(0, 2), new Noise(sourse, 6)),
                Tuple.Create(new Point(1, 0), new Noise(sourse, 6)),
                Tuple.Create(new Point(1, 1), new Noise(sourse, 7)),
                Tuple.Create(new Point(1, 2), new Noise(sourse, 6)),
                Tuple.Create(new Point(2, 0), new Noise(sourse, 6)),
                Tuple.Create(new Point(2, 1), new Noise(sourse, 6)),
                Tuple.Create(new Point(2, 2), new Noise(sourse, 6)));
            CheckMap(map, mapAnswer);
        }

        [Test]
        public void TestCreateNoiseSourceWithLowIntensity()
        {
            var width = 5;
            var height = 5;
            SetMap(width, height, new List<Tuple<Point, IDecor>>());
            var map = GetMap(width, height);
            var sourse = new NoiseSource(NoiseType.Guard, 1, 2, new Point(2, 2), "");
            Dijkstra.DijkstraTraversal(map, sourse, (noises, noise) =>
            {
                noises.Add(noise);
            });
            var mapAnswer = GetMap(width, height);
            ChangeMap(mapAnswer,
                Tuple.Create(new Point(1, 1), new Noise(sourse, 1)),
                Tuple.Create(new Point(1, 2), new Noise(sourse, 1)),
                Tuple.Create(new Point(1, 3), new Noise(sourse, 1)),
                Tuple.Create(new Point(2, 1), new Noise(sourse, 1)),
                Tuple.Create(new Point(2, 2), new Noise(sourse, 2)),
                Tuple.Create(new Point(2, 3), new Noise(sourse, 1)),
                Tuple.Create(new Point(3, 1), new Noise(sourse, 1)),
                Tuple.Create(new Point(3, 2), new Noise(sourse, 1)),
                Tuple.Create(new Point(3, 3), new Noise(sourse, 1)));
            CheckMap(map, mapAnswer);
        }

        [Test]
        public void TestRemoveNoiseSourceWithLowIntensity()
        {
            var width = 5;
            var height = 5;
            SetMap(width, height, new List<Tuple<Point, IDecor>>());
            var map = GetMap(width, height);
            var sourse = new NoiseSource(NoiseType.Guard, 1, 2, new Point(2, 2), "");
            Dijkstra.DijkstraTraversal(map, sourse, (noises, noise) =>
            {
                noises.Add(noise);
            });
            Dijkstra.DijkstraTraversal(map, sourse, (noises, noise) =>
            {
                noises.Remove(noise);
            });
            var mapAnswer = GetMap(width, height);
            CheckMap(map, mapAnswer);
        }

        [Test]
        public void TestCreateTwoNoiseSource()
        {
            var width = 2;
            var height = 2;
            SetMap(width, height, new List<Tuple<Point, IDecor>>());
            var map = GetMap(width, height);
            var sourse1 = new NoiseSource(NoiseType.Guard, 1, 2, new Point(1, 1), "");
            var sourse2 = new NoiseSource(NoiseType.Guard, 1, 2, new Point(0, 0), "");
            Dijkstra.DijkstraTraversal(map, sourse1, (noises, noise) =>
            {
                noises.Add(noise);
            });
            Dijkstra.DijkstraTraversal(map, sourse2, (noises, noise) =>
            {
                noises.Add(noise);
            });
            var mapAnswer = GetMap(width, height);
            ChangeMap(mapAnswer,
                Tuple.Create(new Point(0, 0), new Noise(sourse1, 1)),
                Tuple.Create(new Point(0, 1), new Noise(sourse1, 1)),
                Tuple.Create(new Point(1, 0), new Noise(sourse1, 1)),
                Tuple.Create(new Point(1, 1), new Noise(sourse1, 2)));
            ChangeMap(mapAnswer,
                Tuple.Create(new Point(0, 0), new Noise(sourse2, 2)),
                Tuple.Create(new Point(0, 1), new Noise(sourse2, 1)),
                Tuple.Create(new Point(1, 0), new Noise(sourse2, 1)),
                Tuple.Create(new Point(1, 1), new Noise(sourse2, 1)));

            CheckMap(map, mapAnswer);
        }

        [Test]
        public void TestCreateTwoNoiseSourceAndRemoveOneOfThem()
        {
            var width = 2;
            var height = 2;
            SetMap(width, height, new List<Tuple<Point, IDecor>>());
            var map = GetMap(width, height);
            var sourse1 = new NoiseSource(NoiseType.Guard, 1, 2, new Point(1, 1), "");
            var sourse2 = new NoiseSource(NoiseType.Guard, 1, 2, new Point(0, 0), "");
            Dijkstra.DijkstraTraversal(map, sourse1, (noises, noise) =>
            {
                noises.Add(noise);
            });
            Dijkstra.DijkstraTraversal(map, sourse2, (noises, noise) =>
            {
                noises.Add(noise);
            });
            Dijkstra.DijkstraTraversal(map, sourse2, (noises, noise) =>
            {
                noises.Remove(noise);
            });
            var mapAnswer = GetMap(width, height);
            ChangeMap(mapAnswer,
                Tuple.Create(new Point(0, 0), new Noise(sourse1, 1)),
                Tuple.Create(new Point(0, 1), new Noise(sourse1, 1)),
                Tuple.Create(new Point(1, 0), new Noise(sourse1, 1)),
                Tuple.Create(new Point(1, 1), new Noise(sourse1, 2)));

            CheckMap(map, mapAnswer);
        }

        [Test]
        public void TestCreateNoiseSourceWhisWall()
        {
            var width = 3;
            var height = 3;
            SetMap(width, height, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(1, 1), (IDecor)new Wall()),
                Tuple.Create(new Point(1, 0), (IDecor)new Wall())
            });
            var map = GetMap(width, height);
            var sourse = new NoiseSource(NoiseType.Guard, 1, 4, new Point(2, 1), "");
            Dijkstra.DijkstraTraversal(map, sourse, (noises, noise) =>
            {
                noises.Add(noise);
            });
            var mapAnswer = GetMap(width, height);
            ChangeMap(mapAnswer,
                Tuple.Create(new Point(0, 0), new Noise(sourse, 1)),
                Tuple.Create(new Point(0, 1), new Noise(sourse, 2)),
                Tuple.Create(new Point(0, 2), new Noise(sourse, 2)),
                Tuple.Create(new Point(1, 2), new Noise(sourse, 3)),
                Tuple.Create(new Point(2, 0), new Noise(sourse, 3)),
                Tuple.Create(new Point(2, 1), new Noise(sourse, 4)),
                Tuple.Create(new Point(2, 2), new Noise(sourse, 3)));
            CheckMap(map, mapAnswer);
        }
    }
}