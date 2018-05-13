using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects
{
    public abstract class Item : ImmobileObject, IItem
    {
        public int Price { get; }
        public DecorType Type { get; }

        protected Item(int price, DecorType type, int priority, int noiseSuppression, bool isSolid, bool isOpaque)
            : base(type, priority, noiseSuppression, isSolid, isOpaque)
        {
            Price = price;
            Type = type;
        }

        protected Item(int price, DecorType type) : this(price, type, 30, 0, false, false) { }
    }
}
