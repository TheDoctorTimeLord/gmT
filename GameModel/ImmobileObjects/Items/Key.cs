using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Items
{
    public class Key : ImmobileObject, IItem
    {
        public Key() : base(false, false, 0, 30, "key.png") { }
        private int price;

        int IItem.GetPrice()
        {
            return price;
        }
    }
}
