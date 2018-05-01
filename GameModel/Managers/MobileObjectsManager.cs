using System;
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

        public static void CreateCreature(string nameCreature, InitializationMobileObject init)
        {
            switch (nameCreature)
            {
                case "Player":
                    AddCreature(init.IsDefaultInitialization
                        ? new Player(new InitializationMobileObject(init.Position, init.Direction))
                        : new Player(init));
                    break;
                case "Guard":
                    AddCreature(init.IsDefaultInitialization
                        ? new Guard(new InitializationMobileObject(init.Position, init.Direction))
                        : new Guard(init));
                    break;
                default:
                    throw new Exception("Попытка создания несуществующего Creature: " + nameCreature);
            }
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
    }
}
