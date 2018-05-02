namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Painting : ImmobileObject, IItem
    {
        public Painting() : base(false, false, 0, 30, "painting.png") { }
        private int price;

        int IItem.GetPrice()
        {
            return price;
        }
    }
}
