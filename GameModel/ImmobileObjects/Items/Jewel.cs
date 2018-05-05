using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Jewel : ImmobileObject, IItem
    {
        public int Price { get; }

        public Jewel(int price) : base(DecorType.Jewel, 30, 0, false, false)
        {
            Price = price;
        }

        public override bool InteractWith(ICreature creature)
        {
            if (creature is Player)
                return creature.GetInventory().AddItem(this);
            return false;
        }
    }
}
