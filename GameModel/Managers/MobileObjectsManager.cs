using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.AnimatedObjects;

namespace GameThief.GameModel.Managers
{
    public static class MobileObjectsManager
    {
        public static HashSet<ICreature> MobileObjects { get; private set; } = new HashSet<ICreature>();
        private static readonly HashSet<ICreature> addedMobileObjects = new HashSet<ICreature>();

        public static void CreateCreature(string creatureName, Point position)
        {
            switch (creatureName)
            {
                case "Player":
                    AddCreature(new Player());
                    break;
                default:
                    throw new Exception("Попытка создания несуществующего Creature: " + creatureName);
            }
        }

        private static void AddCreature(ICreature creature)
        {
            addedMobileObjects.Add(creature);
            MapManager.AddCreatureFromMap(creature);
        }

        public static void UpdateAnimates()
        {
            MobileObjects.UnionWith(addedMobileObjects);
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
