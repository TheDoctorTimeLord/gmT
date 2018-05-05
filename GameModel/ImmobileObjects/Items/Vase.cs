using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Vase : ImmobileObject, IItem
    {
        public int Price { get; }

        public Vase(int price, DecorType type, int priority, int noiseSuppression, bool isSolid, bool isOpaque) 
            : base(type, priority, noiseSuppression, isSolid, isOpaque)
        {
            Price = price;
        }

        public Vase(int price) : this(price, DecorType.Vase, 30, 0, false, false) { }

        public override bool InteractWith(ICreature creature)
        {
            if (creature is Player)
                return creature.GetInventory().AddItem(this);
            return false;
        }
    }
}
