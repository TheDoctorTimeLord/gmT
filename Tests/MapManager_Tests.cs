using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.ServiceClasses;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class MapManager_Tests
    {
        readonly List<List<string>> mapSamle = new List<List<string>>();

        void TestFieldOfView(Point position, Direction direction, int viewDistance, int viewWidth, List<Point> expectedResult)
        {
            var result = MapManager.GetVisibleCells(position, direction, viewDistance, viewWidth).ToList();
            CollectionAssert.AreEquivalent(expectedResult, result);
        }

        void FillMap(int width, int height)
        {
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    mapSamle.Add(new List<string> { "." });
                }
            }
        }

        [Test]
        public void TestNoWalls()
        {
            FillMap(5, 5);
            MapManager.CreateMap(5, 5, mapSamle);

            var expectedResult = new List<Point>
            {
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 1),
                new Point(0, 2),
                new Point(1, 2),
                new Point(2, 2),
                new Point(3, 2),
                new Point(4, 2),
                new Point(0, 3),
                new Point(1, 3),
                new Point(2, 3),
                new Point(3, 3),
                new Point(4, 3)
            };

            TestFieldOfView(new Point(2, 0), Direction.Down, 3, 2, expectedResult);
            mapSamle.Clear();
        }

        [Test]
        public void TestSingleWall()
        {
            FillMap(5, 5);
            MapManager.CreateMap(5, 5, mapSamle);
            MapManager.Map[2, 2].ObjectContainer.AddDecor(new Wall());

            var expectedResult = new List<Point>
            {
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 1),
                new Point(0, 2),
                new Point(1, 2),
                new Point(2, 2),
                new Point(3, 2),
                new Point(4, 2),
                new Point(0, 3),
                new Point(1, 3),
                new Point(3, 3),
                new Point(4, 3)
            };

            TestFieldOfView(new Point(2, 0), Direction.Down, 3, 2, expectedResult);
            mapSamle.Clear();
        }

        [Test]
        public void TestLongWall()
        {
            FillMap(5, 5);
            MapManager.CreateMap(5, 5, mapSamle);
            for (var i = 0; i < 5; i++)
                MapManager.Map[i, 2].ObjectContainer.AddDecor(new Wall());

            var expectedResult = new List<Point>
            {
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 1),
                new Point(0, 2),
                new Point(1, 2),
                new Point(2, 2),
                new Point(3, 2),
                new Point(4, 2)
            };

            TestFieldOfView(new Point(2, 0), Direction.Down, 3, 2, expectedResult);
            mapSamle.Clear();
        }

        [Test]
        public void TestTransparentObjects()
        {
            FillMap(5, 5);
            MapManager.CreateMap(5, 5, mapSamle);
            for (var i = 0; i < 5; i++)
                MapManager.Map[i, 2].ObjectContainer.AddDecor(new Table());

            var expectedResult = new List<Point>
            {
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 1),
                new Point(0, 2),
                new Point(1, 2),
                new Point(2, 2),
                new Point(3, 2),
                new Point(4, 2),
                new Point(0, 3),
                new Point(1, 3),
                new Point(2, 3),
                new Point(3, 3),
                new Point(4, 3)
            };

            TestFieldOfView(new Point(2, 0), Direction.Down, 3, 2, expectedResult);
            mapSamle.Clear();
        }

        [Test]
        public void TestMapEdgeRight()
        {
            FillMap(5, 5);
            MapManager.CreateMap(5, 5, mapSamle);

            var expectedResult = new List<Point>
            {
                new Point(3, 0),
                new Point(3, 1),
                new Point(4, 0),
                new Point(4, 1),
                new Point(4, 2)
            };

            TestFieldOfView(new Point(2, 0), Direction.Right, 3, 2, expectedResult);
            mapSamle.Clear();
        }

        [Test]
        public void TestMapEdgeLeft()
        {
            FillMap(5, 5);
            MapManager.CreateMap(5, 5, mapSamle);

            var expectedResult = new List<Point>
            {
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 0),
                new Point(0, 1),
                new Point(0, 2)
            };

            TestFieldOfView(new Point(2, 0), Direction.Left, 3, 2, expectedResult);
            mapSamle.Clear();
        }

        [Test]
        public void TestMapEdgeUp()
        {
            FillMap(5, 5);
            MapManager.CreateMap(5, 5, mapSamle);

            var expectedResult = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
                new Point(4, 0),
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 1)
            };

            TestFieldOfView(new Point(2, 2), Direction.Up, 3, 2, expectedResult);
            mapSamle.Clear();
        }

        [Test]
        public void TestCreature()
        {
            FillMap(5, 5);
            MapManager.CreateMap(5, 5, mapSamle);
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                new Guard(new InitializationMobileObject(new Point(2, 2), Direction.Up))
            });

            var expectedResult = new List<Point>
            {
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 1),
                new Point(0, 2),
                new Point(1, 2),
                new Point(2, 2),
                new Point(3, 2),
                new Point(4, 2),
                new Point(0, 3),
                new Point(1, 3),
                new Point(3, 3),
                new Point(4, 3)
            };

            TestFieldOfView(new Point(2, 0), Direction.Down, 3, 2, expectedResult);
            mapSamle.Clear();
        }
    }
}
