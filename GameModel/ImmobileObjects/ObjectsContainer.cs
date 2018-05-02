using System;
using System.Collections.Generic;
using System.Linq;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.ImmobileObjects.Items;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects
{
    public class ObjectsContainer
    {
        public bool IsSolid { get; set; }
        public bool IsOpaque { get; set; }
        public int TotalNoiseSuppression { get; set; }

        private readonly SortedSet<IDecor> Decors = new SortedSet<IDecor>();

        public void AddDecor(IDecor decor)
        {
            IsSolid = IsSolid || decor.IsSolid();
            IsOpaque = IsOpaque || decor.IsOpaque();
            TotalNoiseSuppression += decor.GetNoiseSuppression();
            Decors.Add(decor);
        }

        public static IDecor ParseDecor(string decorName)
        {
            switch (decorName)
            {
                case "broken_pieces":
                    return new BrokenPieces();
                case "button":
                    return new Button();
                case "carpet":
                    return new Carpet();
                case "chair":
                    return new Chair();
                case "closed_cupboard":
                    return new ClosedCupboard();
                case "closed_door":
                    return new ClosedDoor();
                case "jewel":
                    return new Jewel();
                case "lock":
                    return new Lock();
                case "opened_cupboard":
                    return new OpenedCupboard();
                case "opened_door":
                    return new OpenedDoor();
                case "painting":
                    return new Painting();
                case "treasure":
                    return new Treasure();
                case "vase":
                    return new Vase();
                case "wall":
                    return new Wall();
                default:
                    throw new Exception("Попытка создания несуществующего элемента декора: " + decorName);
            }
        }

        public void RemoveDecor(IDecor decor)
        {
            Decors.Remove(decor);
            TotalNoiseSuppression -= decor.GetNoiseSuppression();

            if (decor.IsSolid())
            {
                IsSolid = false;
                foreach (var dec in Decors)
                    IsSolid = IsSolid || dec.IsSolid();
            }

            if (!decor.IsOpaque())
            {
                IsOpaque = true;
                foreach (var dec in Decors)
                    IsOpaque = IsOpaque || dec.IsOpaque();
            }
        }

        public void Interact(ICreature creature)
        {
            var firstItem = Decors.FirstOrDefault();

            if (firstItem != null)
            {
                var toRemove = firstItem.InteractWith(creature);

                if (toRemove)
                    Decors.Remove(firstItem);
            }
        }

        public IDecor ShowDecor()
        {
            return Decors.FirstOrDefault();
        }

        public IEnumerable<IDecor> GetAllDecors()
        {
            return Decors.ToArray().Reverse();
        }
    }
}
