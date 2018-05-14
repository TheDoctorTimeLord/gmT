using System;
using System.Collections.Generic;
using System.Drawing;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.ImmobileObjects.Items;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ServiceClasses
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
            SetMapAndFillWithDecors(26, 14, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(8, 0), (IDecor) new Window()),
                Tuple.Create(new Point(17, 0), (IDecor) new Window()),
                Tuple.Create(new Point(15, 13), (IDecor) new Window()),
                Tuple.Create(new Point(20, 13), (IDecor) new Window()),
                Tuple.Create(new Point(12, 0), (IDecor) new Mirror()),
                Tuple.Create(new Point(13, 0), (IDecor) new Mirror()),
                Tuple.Create(new Point(19, 3), (IDecor) new Chair()),
                Tuple.Create(new Point(21, 3), (IDecor) new Chair()),
                Tuple.Create(new Point(18, 3), (IDecor) new Plant()),
                Tuple.Create(new Point(22, 3), (IDecor) new Plant()),
                Tuple.Create(new Point(20, 4), (IDecor) new Table()),
                Tuple.Create(new Point(20, 4), (IDecor) new Jewel()),
                Tuple.Create(new Point(11, 12), (IDecor) new Jewel()),
                Tuple.Create(new Point(4, 0), (IDecor) new PaintingFlowers()),
                Tuple.Create(new Point(21, 0), (IDecor) new PaintingFlowers()),
                Tuple.Create(new Point(5, 7), (IDecor) new ClosedDoor()),
                Tuple.Create(new Point(2, 7), (IDecor) new PaintingHouse()),
                Tuple.Create(new Point(1, 10), (IDecor) new Barrel()),
                Tuple.Create(new Point(1, 11), (IDecor) new Barrel()),
                Tuple.Create(new Point(1, 12), (IDecor) new Barrel()),
                Tuple.Create(new Point(2, 11), (IDecor) new Barrel()),
                Tuple.Create(new Point(2, 12), (IDecor) new Barrel()),
                Tuple.Create(new Point(3, 12), (IDecor) new Barrel()),
                Tuple.Create(new Point(6, 12), (IDecor) new Treasure()),
                Tuple.Create(new Point(7, 12), (IDecor) new Treasure()),
                Tuple.Create(new Point(7, 8), (IDecor) new Barrel()),
                Tuple.Create(new Point(8, 8), (IDecor) new Barrel()),
                Tuple.Create(new Point(8, 9), (IDecor) new Barrel()),
                Tuple.Create(new Point(9, 8), (IDecor) new Barrel()),
                Tuple.Create(new Point(9, 9), (IDecor) new Barrel()),
                Tuple.Create(new Point(9, 10), (IDecor) new Barrel()),
                Tuple.Create(new Point(24, 9), (IDecor) new Plant()),
                Tuple.Create(new Point(24, 10), (IDecor) new Plant()),
                Tuple.Create(new Point(24, 11), (IDecor) new Plant()),
                Tuple.Create(new Point(24, 12), (IDecor) new Plant()),
                Tuple.Create(new Point(23, 11), (IDecor) new Plant()),
                Tuple.Create(new Point(23, 12), (IDecor) new Plant()),
                Tuple.Create(new Point(22, 12), (IDecor) new Plant()),
                Tuple.Create(new Point(21, 12), (IDecor) new Plant()),
            });

            for (var i = 0; i < 26; i++)
            {
                MapManager.Map[i, 0].ObjectContainer.AddDecor(new Wall());
                MapManager.Map[i, 13].ObjectContainer.AddDecor(new Wall());
            }

            for (var j = 0; j < 14; j++)
            {
                MapManager.Map[0, j].ObjectContainer.AddDecor(new Wall());
                MapManager.Map[25, j].ObjectContainer.AddDecor(new Wall());
            }

            for (var i = 1; i < 11; i++)
                if (i != 5)
                    MapManager.Map[i, 7].ObjectContainer.AddDecor(new Wall());

            for (var j = 8; j < 13; j++)
                MapManager.Map[10, j].ObjectContainer.AddDecor(new Wall());

            GameInformationManager.CreateTrackByName(new Dictionary<string, List<Instruction>>
            {
                {"path", new List<Instruction>
                {
                    new Instruction(new List<string> {"MoveTo", "18", "8"}),
                    new Instruction(new List<string> {"MoveTo", "3", "1"})
                }}
            });

            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                gameState.Player,
                MobileObjectsManager.GetCreatureByNameAndInitParams(CreatureTypes.Guard,
                    new MobileObjectInitialization(new Point(3, 1), 10, 10, Direction.Right, 1, 5, 2, 3,
                        new Inventory(new HashSet<IItem>(), 10), new List<Tuple<string, string>> {Tuple.Create("path", "path")}))
            });

            MapManager.LevelCost = CountLevelCost();
        }

        private static int CountLevelCost()
        {
            var cost = 0;
            for (var i = 0; i < MapManager.Map.Wigth; i++)
            for (var j = 0; j < MapManager.Map.Height; j++)
            {
                foreach (var decor in MapManager.Map[i, j].ObjectContainer.GetAllDecors())
                {
                    if (decor is IItem)
                        cost += ((IItem)decor).Price;
                }
            }

            return cost;
        }

        //public static void CreateLevel(GameState gameState)
        //{
        //    SetMapAndFillWithDecors(26, 14, new List<Tuple<Point, IDecor>>
        //    {
        //        Tuple.Create(new Point(3, 0), (IDecor) new Wall()),
        //        Tuple.Create(new Point(3, 1), (IDecor) new Wall()),
        //        Tuple.Create(new Point(3, 2), (IDecor) new Wall()),
        //        Tuple.Create(new Point(3, 3), (IDecor) new Wall()),
        //        Tuple.Create(new Point(3, 4), (IDecor) new ClosedDoor()),
        //        //Tuple.Create(new Point(3, 4), (IDecor)new Lock()),
        //        Tuple.Create(new Point(2, 3), (IDecor) new Jewel()),
        //        Tuple.Create(new Point(7, 4), (IDecor) new Wall()),
        //        Tuple.Create(new Point(8, 4), (IDecor) new Wall()),
        //        Tuple.Create(new Point(9, 4), (IDecor) new Wall()),
        //        Tuple.Create(new Point(7, 5), (IDecor) new Wall()),
        //        Tuple.Create(new Point(7, 5), (IDecor) new PaintingHouse()),
        //        Tuple.Create(new Point(8, 5), (IDecor) new Wall()),
        //        Tuple.Create(new Point(9, 5), (IDecor) new Wall()),
        //        Tuple.Create(new Point(9, 5), (IDecor) new PaintingFlowers()),
        //        Tuple.Create(new Point(7, 6), (IDecor) new Wall()),
        //        Tuple.Create(new Point(8, 6), (IDecor) new Treasure()),
        //        //Tuple.Create(new Point(9, 6), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(7, 7), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(8, 7), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(9, 7), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(7, 8), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(8, 8), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(9, 8), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(5, 9), (IDecor) new Wall()),
        //        Tuple.Create(new Point(6, 9), (IDecor) new Wall()),
        //        Tuple.Create(new Point(7, 9), (IDecor) new Table()),
        //        Tuple.Create(new Point(8, 9), (IDecor) new Wall()),
        //        Tuple.Create(new Point(9, 9), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(10, 9), (IDecor) new Wall()),
        //        Tuple.Create(new Point(11, 9), (IDecor) new Wall()),
        //        Tuple.Create(new Point(5, 10), (IDecor) new Wall()),
        //        Tuple.Create(new Point(6, 10), (IDecor) new OpenedDoor()),
        //        Tuple.Create(new Point(7, 10), (IDecor) new Wall()),
        //        Tuple.Create(new Point(7, 10), (IDecor) new Mirror()),
        //        //Tuple.Create(new Point(8, 10), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(9, 10), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(10, 10), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(11, 10), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(5, 11), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(6, 11), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(7, 11), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(9, 11), (IDecor) new Wall()),
        //        //Tuple.Create(new Point(10, 11), (IDecor) new Wall()),
        //        Tuple.Create(new Point(11, 11), (IDecor) new Barrel()),
        //        Tuple.Create(new Point(20, 11), (IDecor) new Wall()),
        //        Tuple.Create(new Point(20, 11), (IDecor) new Window()),
        //        Tuple.Create(new Point(17, 12), (IDecor) new Plant())
        //    });

        //    var guard = MobileObjectsManager.GetCreatureByNameAndInitParams(
        //        CreatureTypes.Guard,
        //        new MobileObjectInitialization(new Point(2, 0), 10, 10, Direction.Right, 1, 5, 2, 0, new Inventory(10),
        //            new List<Tuple<string, string>>()));
        //    //guard.Inventory.AddItem(new Key());
        //    MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature> {gameState.Player, guard});
        //    //MapManager.AddNoiseSourse(new NoiseSource(NoiseType.GuardVoice, 150, 1000,
        //    //    new Point(10, 10), ""));
        //}
    }
}