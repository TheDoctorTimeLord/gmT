using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.Managers;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel
{
    public static class PathFinder
    {
        public static List<Query> GetPathFromTo(Point from, Point to, Direction currentDirectionCreature)
        {
            var queue = new Queue<LinkedNode>();
            var visitiedCell = new HashSet<LinkedNode>();

            queue.Enqueue(new LinkedNode(from, currentDirectionCreature, null));
            visitiedCell.Add(queue.Peek());

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();

                if (currentNode.Position == to)
                    return GetSetQuery(currentNode.GetPath().Reverse().ToList());

                var targetNode = new LinkedNode(
                    currentNode.Position + GameState.ConvertDirectionToSize[currentNode.Direction],
                    currentNode.Direction,
                    currentNode);
                if (CheckMove(targetNode.Position) && !visitiedCell.Contains(targetNode))
                {
                    visitiedCell.Add(targetNode);
                    queue.Enqueue(targetNode);
                }

                targetNode.Position = currentNode.Position;
                targetNode.Direction = GameState.RotateFromTo(currentNode.Direction, true);
                if (!visitiedCell.Contains(targetNode))
                {
                    visitiedCell.Add(targetNode);
                    queue.Enqueue(targetNode);
                }

                targetNode.Direction = GameState.RotateFromTo(currentNode.Direction, false);
                if (!visitiedCell.Contains(targetNode))
                {
                    visitiedCell.Add(targetNode);
                    queue.Enqueue(targetNode);
                }
            }
            return new List<Query>();
        }

        private static List<Query> GetSetQuery(List<LinkedNode> path)
        {
            if (path.Count == 0)
                return new List<Query>();

            var startNode = path.First();
            return path
                .Skip(1)
                .Select(node =>
                {
                    if (startNode.Position.X - node.Position.X == 1 || startNode.Position.Y - node.Position.Y == 1)
                    {
                        startNode = node;
                        return Query.Move;
                    }
                    if (((int) startNode.Direction + 1) % 4 == (int) node.Direction)
                    {
                        startNode = node;
                        return Query.RotateRight;
                    }
                    if (((int) startNode.Direction + 3) % 4 == (int) node.Direction)
                    {
                        startNode = node;
                        return Query.RotateLeft;
                    }
                    throw new Exception("Неверно построен путь");
                })
                .ToList();
        }

        private static bool CheckMove(Point target)
        {
            return MapManager.InBounds(target) &&
                   MapManager.Map[target.X, target.Y].Creature == null &&
                   (!MapManager.Map[target.X, target.Y].ObjectContainer.IsSolid ||
                    MapManager.Map[target.X, target.Y].ObjectContainer.ShowDecor() is ClosedDoor);
        }

        private class LinkedNode
        {
            public Direction Direction;
            public Point Position;
            public LinkedNode PreviousLinkedNode;

            public LinkedNode(Point position, Direction direction, LinkedNode previousLinkedNode)
            {
                Position = position;
                Direction = direction;
                PreviousLinkedNode = previousLinkedNode;
            }

            public IEnumerable<LinkedNode> GetPath()
            {
                var currentNode = this;
                while (currentNode.PreviousLinkedNode != null)
                {
                    yield return currentNode;
                    currentNode = currentNode.PreviousLinkedNode;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj is LinkedNode node)
                    return node.Direction == Direction && node.Position == Position;
                return false;
            }

            public override int GetHashCode()
            {
                return Position.GetHashCode() + Direction.GetHashCode();
            }
        }
    }
}
