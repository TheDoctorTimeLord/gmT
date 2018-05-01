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
            return new List<Query>();
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
        }
    }
}
