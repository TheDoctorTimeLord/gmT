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

        [Test]
        public void ZeroPath()
        {
            SampleMapSetter.SetMapAndFillWithDecors(2, 2, new List<Tuple<Point, IDecor>>());

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
            SampleMapSetter.SetMapAndFillWithDecors(2, 2, new List<Tuple<Point, IDecor>>());

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
            SampleMapSetter.SetMapAndFillWithDecors(2, 2, new List<Tuple<Point, IDecor>>());

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
            SampleMapSetter.SetMapAndFillWithDecors(3, 2, new List<Tuple<Point, IDecor>>());

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
            SampleMapSetter.SetMapAndFillWithDecors(4, 3, new List<Tuple<Point, IDecor>>
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
            SampleMapSetter.SetMapAndFillWithDecors(3, 3, new List<Tuple<Point, IDecor>>());
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
                Query.Move,
                Query.Move
            };
            var result = PathFinder.GetPathFromTo(from, to, direction);

            CheckPath(from, to, direction, answer, result);
        }

        [Test]
        public void NoPath()
        {
            SampleMapSetter.SetMapAndFillWithDecors(3, 3, new List<Tuple<Point, IDecor>>
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
            SampleMapSetter.SetMapAndFillWithDecors(3, 3, new List<Tuple<Point, IDecor>>
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
            SampleMapSetter.SetMapAndFillWithDecors(3, 3, new List<Tuple<Point, IDecor>>
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
