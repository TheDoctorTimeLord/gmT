using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class MapManagerTests
    {
        void TestFieldOfView(Point position, Direction direction, int viewDistance, int viewWidth, List<Point> expectedResult)
        {
            var result = MapManager.GetVisibleCells(position, direction, viewDistance, viewWidth).ToList();
            CollectionAssert.AreEquivalent(expectedResult, result);
        }

        [Test]
        public void TestWithNoWalls()
        {
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
        }

        private readonly List<List<string>> mapSamle = new List<List<string>>
        {
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."},
            new List<string> {"."}
        };
    }
}
