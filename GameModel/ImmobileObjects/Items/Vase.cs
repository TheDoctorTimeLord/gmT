namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Vase : ImmobileObject, IItem
    {
        public Vase() : base(false, false, 0, 30, "vase.png") { }
        private int price;

        int IItem.GetPrice()
        {
            return price;
        }
    }
}
