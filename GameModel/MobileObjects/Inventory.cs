using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameThief.GameModel.ImmobileObjects;

namespace GameThief.GameModel.MobileObjects
{
    public class Inventory
    {
        public HashSet<IItem> Items { get; private set; } = new HashSet<IItem>();
        public int MaxSize { get; private set; }

        public Inventory(HashSet<IItem> items, int maxSize)
        {
            MaxSize = maxSize;
            Items = new HashSet<IItem>(items.Take(maxSize));
        }

        public bool AddItem(IItem item)
        {
            if (Items.Count == MaxSize)
                return false;
            Items.Add(item);
            return true;
        }

        public void RemoveItem(IItem item)
        {
            if (!Items.Contains(item))
                return;
            Items.Remove(item);
        }
    }
}
