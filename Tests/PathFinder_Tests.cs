using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.ServiceClasses;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class PathFinder_Tests
    {
        public void CheckPath(Point from, Point to, Direction direction, List<Query> answer, List<Query> result)
        {
            var walker = from;
            var walkerDirection = direction;
            if (answer.Count != 0)
            {
                foreach (var query in result)
                {
                    switch (query)
                    {
                        case Query.Move:
                            walker = walker + GameState.ConvertDirectionToSize[walkerDirection];
                            break;
                        case Query.RotateLeft:
                            walkerDirection = GameState.RotateFromTo(walkerDirection, true);
                            break;
                        case Query.RotateRight:
                            walkerDirection = GameState.RotateFromTo(walkerDirection, false);
                            break;
                    }
                }
                Assert.AreEqual(to, walker);
            }
            
            Assert.AreEqual(result.Count, answer.Count);
        }

        public void SetMap(int width, int height, List<Tuple<Point, IDecor>> decors)
        {
            var content = new List<List<string>>();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    content.Add(new List<string>{"."});
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
        public void ZeroPath()
        {
            SetMap(2, 2, new List<Tuple<Point, IDecor>>());

            var from = new Point(0, 0);
            var to = new Point(0, 0);
            var direction = Direction.Down;

            var answer = new List<Query>();
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }

        [Test]
        public void OneStep()
        {
            SetMap(2, 2, new List<Tuple<Point, IDecor>>());

            var from = new Point(0, 0);
            var to = new Point(1, 0);
            var direction = Direction.Right;

            var answer = new List<Query> {Query.Move};
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }

        [Test]
        public void RotateAndStep()
        {
            SetMap(2, 2, new List<Tuple<Point, IDecor>>());

            var from = new Point(0, 0);
            var to = new Point(1, 0);
            var direction = Direction.Down;

            var answer = new List<Query> { Query.RotateLeft, Query.Move };
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }

        [Test]
        public void ShortestPathSearch()
        {
            SetMap(3, 2, new List<Tuple<Point, IDecor>>());

            var from = new Point(0, 0);
            var to = new Point(2, 1);
            var direction = Direction.Down;

            var answer = new List<Query> { Query.Move, Query.RotateRight, Query.Move, Query.Move };
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }

        [Test]
        public void ShortestPathSearchWithSolidObject()
        {
            SetMap(4, 3, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(1, 2), (IDecor)new Wall()),
                Tuple.Create(new Point(3, 1), (IDecor)new Wall())
            });

            var from = new Point(0, 1);
            var to = new Point(3, 2);
            var direction = Direction.Down;

            var answer = new List<Query>
            {
                Query.RotateLeft,
                Query.Move,
                Query.Move,
                Query.RotateRight,
                Query.Move,
                Query.RotateLeft,
                Query.Move
            };
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }

        [Test]
        public void ShortestPathSearchWithMobileObject()
        {
            SetMap(3, 3, new List<Tuple<Point, IDecor>>());
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                MobileObjectsManager.GetCreatureByNameAndInitParams(
                    "Guard", new InitializationMobileObject(new Point(1, 1), Direction.Down)),
            });

        var from = new Point(1, 0);
            var to = new Point(1, 2);
            var direction = Direction.Down;

            var answer = new List<Query>
            {
                Query.RotateLeft,
                Query.Move,
                Query.RotateRight,
                Query.Move,
                Query.Move,
                Query.RotateRight,
                Query.Move
            };
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }

        [Test]
        public void NoPath()
        {
            SetMap(3, 3, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(1, 0), (IDecor)new Wall()),
                Tuple.Create(new Point(1, 1), (IDecor)new Wall()),
                Tuple.Create(new Point(1, 2), (IDecor)new Wall())
            });

            var from = new Point(0, 0);
            var to = new Point(2, 0);
            var direction = Direction.Down;

            var answer = new List<Query>();
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }

        [Test]
        public void ShortestPathSearchWithSolidObjectAndClosedDoor()
        {
            SetMap(3, 3, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(1, 0), (IDecor)new Wall()),
                Tuple.Create(new Point(1, 2), (IDecor)new Wall()),
                Tuple.Create(new Point(1, 1), (IDecor)new ClosedDoor())
            });

            var from = new Point(0, 1);
            var to = new Point(2, 1);
            var direction = Direction.Right;

            var answer = new List<Query>
            {
                Query.Move,
                Query.Move
            };
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }

        [Test]
        public void ShortestPathSearchWithSolidObjectAndOpenedDoor()
        {
            SetMap(3, 3, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(1, 0), (IDecor)new Wall()),
                Tuple.Create(new Point(1, 2), (IDecor)new Wall()),
                Tuple.Create(new Point(1, 1), (IDecor)new OpenedDoor())
            });

            var from = new Point(0, 1);
            var to = new Point(2, 1);
            var direction = Direction.Right;

            var answer = new List<Query>
            {
                Query.Move,
                Query.Move
            };
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }
    }
}
