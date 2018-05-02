namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Jewel : ImmobileObject, IItem
    {
        public Jewel() : base(false, false, 0, 30, "jewel.png") { }
        private int price;

        int IItem.GetPrice()
        {
            return price;
        }
    }
}
