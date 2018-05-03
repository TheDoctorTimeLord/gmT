using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Jewel : ImmobileObject, IItem
    {
        public Jewel() : base(false, false, 0, 30, "jewel.png") { }
        private int price;

        public int GetPrice()
        {
            return price;
        }

        public override bool InteractWith(ICreature creature)
        {

        }
    }
}
