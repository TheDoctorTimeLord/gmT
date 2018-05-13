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
        private static readonly Dictionary<string, Func<IDecor>> DecorParser = new Dictionary<string, Func<IDecor>>
        {
            {"broken_pieces", () => new BrokenPieces()},
            {"button", () => new Button()},
            {"carpet", () => new Carpet()},
            {"chair", () => new Chair()},
            {"closed_cupboard", () => new ClosedCupboard()},
            {"closed_door", () => new ClosedDoor()},
            {"lock", () => new Lock()},
            {"opened_cupboard", () => new OpenedCupboard()},
            {"opened_door", () => new OpenedDoor()},
            {"wall", () => new Wall()},
            {"key", () => new Key()},
            {"painting", () => new PaintingFlowers()},
            {"treasure", () => new Treasure()},
            {"vase", () => new Vase()},
            {"jewel", () => new Jewel()}
        };

        private readonly SortedSet<IDecor> Decors = new SortedSet<IDecor>();
        public bool IsSolid { get; set; }
        public bool IsOpaque { get; set; }
        public int TotalNoiseSuppression { get; set; }

        public void AddDecor(IDecor decor)
        {
            IsSolid = IsSolid || decor.IsSolid;
            IsOpaque = IsOpaque || decor.IsOpaque;
            TotalNoiseSuppression += decor.NoiseSuppression;
            Decors.Add(decor);
        }

        public static IDecor ParseDecor(string decorName)
        {
            return DecorParser.ContainsKey(decorName)
                ? DecorParser[decorName]()
                : throw new Exception("Попытка создания несуществующего элемента декора \"" + decorName + "\"");
        }

        public void RemoveDecor(IDecor decor)
        {
            Decors.Remove(decor);
            TotalNoiseSuppression -= decor.NoiseSuppression;

            if (decor.IsSolid)
            {
                IsSolid = false;
                foreach (var dec in Decors)
                    IsSolid = IsSolid || dec.IsSolid;
            }

            if (decor.IsOpaque)
            {
                IsOpaque = false;
                foreach (var dec in Decors)
                    IsOpaque = IsOpaque || dec.IsOpaque;
            }
        }

        public void Interact(ICreature creature)
        {
            var firstItem = Decors.FirstOrDefault();

            if (firstItem != null)
            {
                var toRemove = firstItem.InteractWith(creature);

                if (toRemove)
                    RemoveDecor(firstItem);
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