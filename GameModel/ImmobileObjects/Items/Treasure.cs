using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Treasure : ImmobileObject, IItem
    {
        public Treasure() : base(false, false, 0, 30, "treasure.png") { }
        private int price;

        int IItem.GetPrice()
        {
            return price;
        }

        public override bool InteractWith(ICreature creature)
        {
            if (creature is Player)
                return creature.GetInventory().AddItem(this);
            return false;
        }
    }
}
