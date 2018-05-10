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
using GameThief.GameModel.MobileObjects.Creature;
using GameThief.GameModel.ServiceClasses;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class MapManager_Tests
    {
        void TestFieldOfView(Point position, Direction direction, int viewDistance, int viewWidth, List<Point> expectedResult)
        {
            var result = MapManager.GetVisibleCells(position, direction, viewDistance, viewWidth).ToList();
            CollectionAssert.AreEquivalent(expectedResult, result);
        }

        [Test]
        public void TestAddingCreature()
        {
            GameSetter.SetSampleMap(3, 3);
            var player = new Player(new InitializationMobileObject(new Point(0, 0), Direction.Up));
            MapManager.AddCreatureToMap(player);
            Assert.IsTrue(MapManager.Map[0, 0].Creature is Player);
        }

        [Test]
        public void TestMovingCreature()
        {
            GameSetter.SetSampleMap(3, 3);
            var player = new Player(new InitializationMobileObject(new Point(0, 0), Direction.Up));
            MapManager.AddCreatureToMap(player);
            MapManager.MoveCreature(player, new Point(0, 1));
            Assert.IsTrue(MapManager.Map[0, 1].Creature is Player);
        }

        [Test]
        public void TestRemovingCreature()
        {
            GameSetter.SetSampleMap(3, 3);
            var player = new Player(new InitializationMobileObject(new Point(0, 0), Direction.Up));
            MapManager.AddCreatureToMap(player);
            MapManager.RemoveCreatureFromMap(player);
            Assert.IsTrue(MapManager.Map[0, 0].Creature == null);
        }

        [Test]
        public void TestMapFilling()
        {
            var width = 3;
            var height = 3;
            GameSetter.SetSampleMap(width, height);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    Assert.True(MapManager.Map[i, j].Creature == null);
                    Assert.False(MapManager.Map[i, j].ObjectContainer.IsOpaque);
                    Assert.False(MapManager.Map[i, j].ObjectContainer.IsSolid);
                }
            }
        }
        
        [Test]
        public void TestFieldOfViewWithNoWalls()
        {
            GameSetter.SetSampleMap(5, 5);

            var expectedResult = new List<Point>
            {
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
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
        }

        [Test]
        public void TestFieldOfViewWithSingleWall()
        {
            GameSetter.SetSampleMap(5, 5);
            MapManager.Map[2, 2].ObjectContainer.AddDecor(new Wall());

            var expectedResult = new List<Point>
            {
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
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
        }

        [Test]
        public void TestFieldOfViewWithLongWall()
        {
            GameSetter.SetSampleMap(5, 5);
            for (var i = 0; i < 5; i++)
                MapManager.Map[i, 2].ObjectContainer.AddDecor(new Wall());

            var expectedResult = new List<Point>
            {
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
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
        }

        [Test]
        public void TestFieldOfViewWithTransparentObjects()
        {
            GameSetter.SetSampleMap(5, 5);
            for (var i = 0; i < 5; i++)
                MapManager.Map[i, 2].ObjectContainer.AddDecor(new Table());

            var expectedResult = new List<Point>
            {
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
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
        }

        [Test]
        public void TesFieldOfViewWithtMapEdgeRight()
        {
            GameSetter.SetSampleMap(5, 5);

            var expectedResult = new List<Point>
            {
                new Point(2, 0),
                new Point(2, 1),
                new Point(3, 0),
                new Point(3, 1),
                new Point(4, 0),
                new Point(4, 1),
                new Point(4, 2)
            };

            TestFieldOfView(new Point(2, 0), Direction.Right, 3, 2, expectedResult);
        }

        [Test]
        public void TestFieldOfViewWithMapEdgeLeft()
        {
            GameSetter.SetSampleMap(5, 5);

            var expectedResult = new List<Point>
            {
                new Point(2, 0),
                new Point(2, 1),
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 0),
                new Point(0, 1),
                new Point(0, 2)
            };

            TestFieldOfView(new Point(2, 0), Direction.Left, 3, 2, expectedResult);
        }

        [Test]
        public void TestFieldOfViewWithMapEdgeUp()
        {
            GameSetter.SetSampleMap(5, 5);

            var expectedResult = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
                new Point(4, 0),
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 1),
                new Point(1, 2),
                new Point(2, 2),
                new Point(3, 2)
            };

            TestFieldOfView(new Point(2, 2), Direction.Up, 3, 2, expectedResult);
        }

        [Test]
        public void TestFieldOfViewWithCreature()
        {
            GameSetter.SetSampleMap(5, 5);
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                new Guard(new InitializationMobileObject(new Point(2, 2), Direction.Up))
            });

            var expectedResult = new List<Point>
            {
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
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
        }
    }
}
