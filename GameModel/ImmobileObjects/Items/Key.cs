using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Key : ImmobileObject, IItem
    {
        public int Price { get; }

        public Key(int price) : base(DecorType.Key, 30, 0, false, false)
        {
            Price = price;
        }

        public override bool InteractWith(ICreature creature)
        {
            return creature.Inventory.AddItem(this);
        }
    }
}
