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
        /// <summary>
        /// Поиск кротчайшего пути между точками from и to, описанный набором команд движения. Промежуточные взаиможейсвия
        /// (например, с закрытой дверью) не учитываются в возращаемом списке
        /// </summary>
        /// <param name="from">Позиция начала пути</param>
        /// <param name="to">Позиция конца пути</param>
        /// <param name="currentDirectionCreature">Направление взгляда персонажа</param>
        /// <returns></returns>
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

                AddTurnEdge(currentNode, visitiedCell, queue, true);
                AddTurnEdge(currentNode, visitiedCell, queue, false);
            }
            return new List<Query>();
        }

        private static void AddTurnEdge(LinkedNode currentNode, HashSet<LinkedNode> visitiedCell,
            Queue<LinkedNode> queue, bool isLeftRotate)
        {
            var targetNode = new LinkedNode(
                currentNode.Position,
                GameState.RotateFromTo(currentNode.Direction, isLeftRotate),
                currentNode);
            if (visitiedCell.Contains(targetNode))
                return;
            visitiedCell.Add(targetNode);
            queue.Enqueue(targetNode);
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
                    var query = ConvertActionToQuery(startNode, node);
                    startNode = node;
                    return query;
                })
                .ToList();
        }

        private static Query ConvertActionToQuery(LinkedNode startNode, LinkedNode currentNode)
        {
            if (Math.Abs(startNode.Position.X - currentNode.Position.X) +
                Math.Abs(startNode.Position.Y - currentNode.Position.Y) == 1)
            {
                return Query.Move;
            }
            if (GameState.RotateFromTo(startNode.Direction, false) == currentNode.Direction)
            {
                return Query.RotateRight;
            }
            if (GameState.RotateFromTo(startNode.Direction, true) == currentNode.Direction)
            {
                return Query.RotateLeft;
            }
            throw new Exception("Неверно соединены узлы пути");
        }

        private static bool CheckMove(Point target)
        {
            return MapManager.InBounds(target) &&
                   //MapManager.Map[target.X, target.Y].Creature == null &&
                   (!MapManager.Map[target.X, target.Y].ObjectContainer.IsSolid ||
                    MapManager.Map[target.X, target.Y].ObjectContainer.ShowDecor() is ClosedDoor);
        }

        private class LinkedNode
        {
            public readonly Direction Direction;
            public Point Position;
            private readonly LinkedNode previousLinkedNode;

            public LinkedNode(Point position, Direction direction, LinkedNode previousLinkedNode)
            {
                Position = position;
                Direction = direction;
                this.previousLinkedNode = previousLinkedNode;
            }

            public IEnumerable<LinkedNode> GetPath()
            {
                var currentNode = this;
                while (currentNode != null)
                {
                    yield return currentNode;
                    currentNode = currentNode.previousLinkedNode;
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

            public override string ToString()
            {
                return $"{Position.X} {Position.Y}. Dir = {Direction}";
            }
        }
    }
}
