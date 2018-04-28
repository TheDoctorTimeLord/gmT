﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.Managers
{
    public static class AnimatesManager
    {
        public static HashSet<ICreature> Animates { get; private set; } = new HashSet<ICreature>();
        private static readonly HashSet<ICreature> addedAnimations = new HashSet<ICreature>();

        public static void CreateCreature(string nameCreature, Point position)
        {
            if (nameCreature == "Player")
            {
                var pl = new Player();
                MapManager.AddCreatureFromMap(pl, position);
                addedAnimations.Add(pl);
            }
        }

        public static void UpdateAnimates()
        {
            Animates.UnionWith(addedAnimations);
            addedAnimations.Clear();
        }

        public static void DeleteCreature(ICreature creature)
        {
            if (!Animates.Contains(creature))
                return;

            Animates.Remove(creature);
            MapManager.AddCreatureFromMap(creature);
        }
    }
}
