﻿using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;
using NUnit.Framework;

namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Vase : Item
    {
        public Vase() : base(10, DecorType.Vase) { }

        public override bool InteractWith(ICreature creature)
        {
            if (creature is Player)
                return creature.GetInventory().AddItem(this);
            return false;
        }
    }
}