using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.MobileObjects.Creature;

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
                return creature.Inventory.AddItem(this);
            return false;
        }
    }
}
