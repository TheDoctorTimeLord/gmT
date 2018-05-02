﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.Managers
{
    public static class MobileObjectsManager
    {
        public static HashSet<ICreature> MobileObjects { get; private set; } = new HashSet<ICreature>();
        private static readonly HashSet<ICreature> addedMobileObjects = new HashSet<ICreature>();

        public static void InitializationMobileOjects(HashSet<ICreature> creatures)
        {
            MobileObjects = new HashSet<ICreature>();
            foreach (var creature in creatures)
            {
                MapManager.AddCreatureToMap(creature);
                MobileObjects.Add(creature);
            }
        }

        public static void CreateCreature(string nameCreature, InitializationMobileObject init)
        {
            AddCreature(GetCreatureByNameAndInitParams(nameCreature, init));
        }

        public static void CreateCreature(ICreature creature)
        {
            AddCreature(creature);
        }

        private static void AddCreature(ICreature creature)
        {
            addedMobileObjects.Add(creature);
        }

        public static void UpdateAnimates()
        {
            foreach (var addedMobileObject in addedMobileObjects)
            {
                MobileObjects.Add(addedMobileObject);
                MapManager.AddCreatureToMap(addedMobileObject);
            }
            addedMobileObjects.Clear();
        }

        public static void DeleteCreature(ICreature creature)
        {
            if (!MobileObjects.Contains(creature))
                return;

            MobileObjects.Remove(creature);
            MapManager.RemoveCreatureFromMap(creature);
        }

        public static ICreature GetCreatureByNameAndInitParams(string nameCreature, InitializationMobileObject init)
        {
            switch (nameCreature)
            {
                case "Player":
                    return init.IsDefaultInitialization
                        ? new Player(new InitializationMobileObject(init.Position, init.Direction))
                        : new Player(init);
                case "Guard":
                    return init.IsDefaultInitialization
                        ? new Guard(new InitializationMobileObject(init.Position, init.Direction))
                        : new Guard(init);
                default:
                    throw new Exception("Попытка создания несуществующего Creature: " + nameCreature);
            }
        }
    }
}
