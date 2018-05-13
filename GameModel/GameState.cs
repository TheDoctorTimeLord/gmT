﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.MobileObjects.Creature;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel
{
    public class GameState
    {
        public static Random Random = new Random();

        public static Keys KeyPressed;

        public static readonly Dictionary<Direction, Size> ConvertDirectionToSize = new Dictionary<Direction, Size>
        {
            {Direction.Up, new Size(0, -1)},
            {Direction.Left, new Size(-1, 0)},
            {Direction.Down, new Size(0, 1)},
            {Direction.Right, new Size(1, 0)}
        };

        public readonly Player Player = new Player(new MobileObjectInitialization(new Point(0, 0), Direction.Right));

        //public GameState(string gameConfigurationFilename)
        //{
        //    var mapInfo = FileManager.ReadMapState(gameConfigurationFilename);
        //    if (!mapInfo.WasSuccessful)
        //        throw new Exception("Некорректное задание стартовых данных. Файл: " + gameConfigurationFilename);
        //    MapManager.CreateMap(mapInfo.Width, mapInfo.Height, mapInfo.MapInfo);
        //}

        public GameState()
        {
            GameSetter.CreateLevel(Player);

            PressedKeyConverter.CreateConverter(new Dictionary<Keys, Query>
            {
                {Keys.D, Query.RotateRight},
                {Keys.W, Query.Move},
                {Keys.A, Query.RotateLeft},
                {Keys.E, Query.Interaction}
            });
        }

        public static Query GetCurrentQuery()
        {
            return PressedKeyConverter.Convert(KeyPressed);
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
                {
                    creature.ActionRejected(query);
                }
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
                    var target = creature.Position + ConvertDirectionToSize[creature.Direction];
                    if (MapManager.Map[target.X, target.Y].Creature != null)
                        MapManager.Map[target.X, target.Y].Creature.InteractWith(creature);
                    else
                        MapManager.Map[target.X, target.Y].ObjectContainer.Interact(creature);
                    break;

                case Query.Move:
                    target = creature.Position + ConvertDirectionToSize[creature.Direction];
                    MapManager.MoveCreature(creature, target);
                    break;

                case Query.RotateLeft:
                    creature.Direction = RotateFromTo(creature.Direction, true);
                    break;

                case Query.RotateRight:
                    creature.Direction = RotateFromTo(creature.Direction, false);
                    break;
                default:
                    throw new Exception("Не обработанное намерение: " + query + ". " + creature);
            }
        }

        private bool ValidateRequest(Query query, ICreature creature)
        {
            if (creature.IsHidden() && query != Query.Interaction)
                return false;

            var delta = Size.Empty;
            if (query == Query.Move || query == Query.Interaction)
                delta = ConvertDirectionToSize[creature.Direction];

            var target = creature.Position + delta;
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

        public static Direction RotateFromTo(Direction from, bool isLeftRotate)
        {
            var direction = (int) from;
            if (isLeftRotate)
                direction = (direction + 3) % 4;
            else
                direction = (direction + 1) % 4;
            return (Direction) direction;
        }
    }
}