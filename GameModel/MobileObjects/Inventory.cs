using System.Collections.Generic;
using System.Linq;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel.MobileObjects
{
    public class Inventory
    {
        public Inventory(int maxSize)
        {
            MaxSize = maxSize;
            Items = new HashSet<IItem>();
        }

        public Inventory(HashSet<IItem> items, int maxSize)
        {
            MaxSize = maxSize;
            Items = new HashSet<IItem>(items.Take(maxSize));
        }

        public HashSet<IItem> Items { get; }
        public int MaxSize { get; }

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

        public void DropItem(IItem item, ICreature creature)
        {
            var currentPosition = creature.Position;
            MapManager.Map[currentPosition.X, currentPosition.Y].ObjectContainer.AddDecor((IDecor) item);
        }
    }
}