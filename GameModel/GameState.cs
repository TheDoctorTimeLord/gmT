using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.AnimatedObjects;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSourse;

namespace GameThief.GameModel
{
    public class GameState
    {
        public GameState(string nameBeginActState)
        {
            var infoByMap = FileManager.ReadingMapState(nameBeginActState);
            if (!infoByMap.IsSuccessfulReading)
                throw new Exception("Некорректное задание стартовых данных. Файл: " + nameBeginActState);
            MapManager.CreateMap(infoByMap.Width, infoByMap.Height, infoByMap.InfoAboutMap);
        }

        public void UpdateState()
        {
            foreach (var animate in AnimatesManager.Animates)
            {
                var query = animate.GetIntention();
                if (IsRequestVerified(query, animate))
                {
                    ActionIntention(query, animate);
                    animate.ActionTaken(query);
                }
                else
                    animate.ActionRejected(query);
            }

            TemporaryObjectsManager.UpdateTimers();
            AnimatesManager.UpdateAnimates();
        }

        private void ActionIntention(Query query, ICreature animate)
        {
            switch (query)
            {
                case Query.None:
                    break;
                case Query.Interaction:
                    var target = animate.GetPosition() + ConvertDirectionToSize[animate.GetDirection()];
                    if (MapManager.Map[target.X, target.Y].Creature != null)
                        MapManager.Map[target.X, target.Y].Creature.Interative(animate);
                    else
                        MapManager.Map[target.X, target.Y].Object.Interact(animate);
                    break;

                case Query.Move:
                    target = animate.GetPosition() + ConvertDirectionToSize[animate.GetDirection()];
                    MapManager.MoveCreature(animate, target);
                    break;

                case Query.RotateLeft:
                    animate.ChangeDirection(RotateFromTo(animate.GetDirection(), true));
                    break;

                case Query.RotateRight:
                    animate.ChangeDirection(RotateFromTo(animate.GetDirection(), false));
                    break;
                default:
                    throw new Exception("Не обработанное намерение: " + query.ToString() + ". " + animate);
            }
        }

        private bool IsRequestVerified(Query query, ICreature animate)
        {
            var delta = Size.Empty;
            if (query == Query.Move || query == Query.Interaction)
                delta = ConvertDirectionToSize[animate.GetDirection()];

            var target = animate.GetPosition() + delta;
            if (!MapManager.InBounds(target))
                return false;

            if (query == Query.Interaction)
                return true;

            if (MapManager.Map.Cells[target.X, target.Y].Creature != null)
                return false;
            if (MapManager.Map.Cells[target.X, target.Y].Object.IsSolid)
                return false;
            return true;
        }

        public static readonly Dictionary<Direction, Size> ConvertDirectionToSize = new Dictionary<Direction, Size>
        {
            { Direction.Up, new Size(0, -1) },
            { Direction.Left, new Size(-1, 0) },
            { Direction.Down, new Size(0, 1) },
            { Direction.Right, new Size(1, 0) }
        };

        public static Direction RotateFromTo(Direction from, bool isLeftRotate)
        {
            var direction = (int)from;
            if (isLeftRotate)
                direction = (direction + 3) % 4;
            else
                direction = (direction + 1) % 4;
            return (Direction) direction;
        }
    }
}