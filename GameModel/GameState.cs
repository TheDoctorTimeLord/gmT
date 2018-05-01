using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel
{
    public class GameState
    {
        public static Keys KeyPressed;

        public static Query GetCurrentQuery() => ConverterPressedKey.Convert(KeyPressed);

        //public GameState(string gameConfigurationFilename)
        //{
        //    var mapInfo = FileManager.ReadMapState(gameConfigurationFilename);
        //    if (!mapInfo.WasSuccessful)
        //        throw new Exception("Некорректное задание стартовых данных. Файл: " + gameConfigurationFilename);
        //    MapManager.CreateMap(mapInfo.Width, mapInfo.Height, mapInfo.MapInfo);
        //}

        public GameState()
        {
            var lll = new List<List<string>>()
            {
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
                new List<string>{"."},
            };

            MapManager.CreateMap(5, 4, lll);
            MobileObjectsManager.CreateCreature("Player",
                new InitializationMobileObject(new Point(1, 1), Direction.Right));

            MobileObjectsManager.CreateCreature("Guard",
                new InitializationMobileObject(new Point(2, 2), Direction.Left));

            ConverterPressedKey.CreateConverter(new Dictionary<Keys, Query>
            {
                {Keys.D, Query.RotateRight},
                {Keys.W, Query.Move},
                {Keys.A, Query.RotateLeft},
                {Keys.E, Query.Interaction}
            });
        }

        public void UpdateState()
        {
            foreach (var creature in MobileObjectsManager.MobileObjects)
            {
                var query = creature.GetIntention();
                if (ValidateRequest(query, creature))
                {
                    ExecuteIntention(query, creature);
                    creature.ActionTaken(query);
                }
                else
                    creature.ActionRejected(query);
            }

            TemporaryObjectsManager.UpdateTemporaryObjects();
            MobileObjectsManager.UpdateAnimates();
        }

        private void ExecuteIntention(Query query, ICreature creature)
        {
            switch (query)
            {
                case Query.None:
                    break;
                case Query.Interaction:
                    var target = creature.GetPosition() + ConvertDirectionToSize[creature.GetDirection()];
                    if (MapManager.Map[target.X, target.Y].Creature != null)
                        MapManager.Map[target.X, target.Y].Creature.Interative(creature);
                    else
                        MapManager.Map[target.X, target.Y].ObjectContainer.Interact(creature);
                    break;

                case Query.Move:
                    target = creature.GetPosition() + ConvertDirectionToSize[creature.GetDirection()];
                    MapManager.MoveCreature(creature, target);
                    break;

                case Query.RotateLeft:
                    creature.ChangeDirection(RotateFromTo(creature.GetDirection(), true));
                    break;

                case Query.RotateRight:
                    creature.ChangeDirection(RotateFromTo(creature.GetDirection(), false));
                    break;
                default:
                    throw new Exception("Не обработанное намерение: " + query.ToString() + ". " + creature);
            }
        }

        private bool ValidateRequest(Query query, ICreature creature)
        {
            var delta = Size.Empty;
            if (query == Query.Move || query == Query.Interaction)
                delta = ConvertDirectionToSize[creature.GetDirection()];

            var target = creature.GetPosition() + delta;
            if (!MapManager.InBounds(target))
                return false;

            switch (query)
            {
                case Query.Interaction:
                    return true;
                case Query.Move when MapManager.Map.Cells[target.X, target.Y].Creature != null:
                    return false;
                case Query.Move when MapManager.Map.Cells[target.X, target.Y].ObjectContainer.IsSolid:
                    return false;
                default:
                    return true;
            }
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