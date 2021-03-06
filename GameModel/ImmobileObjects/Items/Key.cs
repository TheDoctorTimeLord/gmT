﻿using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Key : Item
    {
        public Key() : base(0, DecorType.Key)
        {
        }

        public override bool InteractWith(ICreature creature)
        {
            return creature.Inventory.AddItem(this);
        }
    }
}