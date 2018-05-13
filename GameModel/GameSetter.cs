using System;
using System.Collections.Generic;
using System.Drawing;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.ImmobileObjects.Items;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.MobileObjects.Creature;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel
{
    public static class GameSetter
    {
        public static void SetSampleMap(int width, int height)
        {
            var content = new List<List<string>>();
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                content.Add(new List<string> {"wood"});
            MapManager.CreateMap(width, height, content);
        }

        public static void SetMapAndFillWithDecors(int width, int height, List<Tuple<Point, IDecor>> decors)
        {
            SetSampleMap(width, height);

            foreach (var tuple in decors)
                if (MapManager.InBounds(tuple.Item1))
                    MapManager.Map[tuple.Item1.X, tuple.Item1.Y].ObjectContainer.AddDecor(tuple.Item2);
        }

        public static void CreateLevel(GameState gameState)
        {
            SetMapAndFillWithDecors(24, 14, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(3, 0), (IDecor) new Wall()),
                Tuple.Create(new Point(3, 1), (IDecor) new Wall()),
                Tuple.Create(new Point(3, 2), (IDecor) new Wall()),
                Tuple.Create(new Point(3, 3), (IDecor) new Wall()),
                Tuple.Create(new Point(3, 4), (IDecor) new ClosedDoor()),
                //Tuple.Create(new Point(3, 4), (IDecor)new Lock()),
                Tuple.Create(new Point(2, 3), (IDecor) new Jewel()),
                Tuple.Create(new Point(7, 4), (IDecor) new Wall()),
                Tuple.Create(new Point(8, 4), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 4), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 5), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 5), (IDecor) new PaintingHouse()),
                Tuple.Create(new Point(8, 5), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 5), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 5), (IDecor) new PaintingFlowers()),
                Tuple.Create(new Point(7, 6), (IDecor) new Wall()),
                Tuple.Create(new Point(8, 6), (IDecor) new Treasure()),
                //Tuple.Create(new Point(9, 6), (IDecor) new Wall()),
                //Tuple.Create(new Point(7, 7), (IDecor) new Wall()),
                //Tuple.Create(new Point(8, 7), (IDecor) new Wall()),
                //Tuple.Create(new Point(9, 7), (IDecor) new Wall()),
                //Tuple.Create(new Point(7, 8), (IDecor) new Wall()),
                //Tuple.Create(new Point(8, 8), (IDecor) new Wall()),
                //Tuple.Create(new Point(9, 8), (IDecor) new Wall()),
                //Tuple.Create(new Point(5, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(6, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 9), (IDecor) new Table()),
                Tuple.Create(new Point(8, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 9), (IDecor) new Wall()),
                //Tuple.Create(new Point(10, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(11, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(5, 10), (IDecor) new Wall()),
                Tuple.Create(new Point(6, 10), (IDecor) new OpenedDoor()),
                Tuple.Create(new Point(7, 10), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 10), (IDecor) new Mirror()),
                //Tuple.Create(new Point(8, 10), (IDecor) new Wall()),
                //Tuple.Create(new Point(9, 10), (IDecor) new Wall()),
                //Tuple.Create(new Point(10, 10), (IDecor) new Wall()),
                //Tuple.Create(new Point(11, 10), (IDecor) new Wall()),
                //Tuple.Create(new Point(5, 11), (IDecor) new Wall()),
                //Tuple.Create(new Point(6, 11), (IDecor) new Wall()),
                //Tuple.Create(new Point(7, 11), (IDecor) new Wall()),
                //Tuple.Create(new Point(9, 11), (IDecor) new Wall()),
                //Tuple.Create(new Point(10, 11), (IDecor) new Wall()),
                Tuple.Create(new Point(11, 11), (IDecor) new Barrel()),
                Tuple.Create(new Point(20, 11), (IDecor) new Wall()),
                Tuple.Create(new Point(20, 11), (IDecor) new Window()),
                Tuple.Create(new Point(17, 12), (IDecor) new Plant())
            });

            var guard = MobileObjectsManager.GetCreatureByNameAndInitParams(
                CreatureTypes.Guard,
                new MobileObjectInitialization(new Point(2, 0), 10, 10, Direction.Right, 1, 5, 2, 0, new Inventory(10),
                    new List<Tuple<string, string>>()));
            //guard.Inventory.AddItem(new Key());
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature> {gameState.Player, guard});
            //MapManager.AddNoiseSourse(new NoiseSource(NoiseType.GuardVoice, 150, 1000,
            //    new Point(10, 10), ""));
        }
    }
}