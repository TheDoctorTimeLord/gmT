using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.ImmobileObjects.Items;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.MobileObjects.Creature;
using GameThief.GameModel.ServiceClasses;
using GameThief.GUI;

namespace GameThief
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            var player = new Player(new InitializationMobileObject(new Point(0, 0), Direction.Left));

            Map1(player);
            Application.Run(new GameWindow());
        }

        private static void Map1(Player player)
        {
            SetMap(15, 15, new List<Tuple<Point, IDecor>>
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
                Tuple.Create(new Point(8, 5), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 5), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 6), (IDecor) new Wall()),
                Tuple.Create(new Point(8, 6), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 6), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 7), (IDecor) new Wall()),
                Tuple.Create(new Point(8, 7), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 7), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 8), (IDecor) new Wall()),
                Tuple.Create(new Point(8, 8), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 8), (IDecor) new Wall()),
                Tuple.Create(new Point(5, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(6, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(8, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(10, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(11, 9), (IDecor) new Wall()),
                Tuple.Create(new Point(5, 10), (IDecor) new Wall()),
                Tuple.Create(new Point(6, 10), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 10), (IDecor) new Wall()),
                Tuple.Create(new Point(8, 10), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 10), (IDecor) new Wall()),
                Tuple.Create(new Point(10, 10), (IDecor) new Wall()),
                Tuple.Create(new Point(11, 10), (IDecor) new Wall()),
                Tuple.Create(new Point(5, 11), (IDecor) new Wall()),
                Tuple.Create(new Point(6, 11), (IDecor) new Wall()),
                Tuple.Create(new Point(7, 11), (IDecor) new Wall()),
                Tuple.Create(new Point(9, 11), (IDecor) new Wall()),
                Tuple.Create(new Point(10, 11), (IDecor) new Wall()),
                Tuple.Create(new Point(11, 11), (IDecor) new Wall())
            });
            var guard = MobileObjectsManager.GetCreatureByNameAndInitParams(
                CreatureTypes.Guard, new InitializationMobileObject(new Point(2, 0), 10, 10, Direction.Right, 1, 5, 2, 0, new Inventory(10), new List<Tuple<string, string>>()));
            guard.Inventory.AddItem(new Key());
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                player,
                guard
            });

            //MapManager.AddNoiseSourse(new NoiseSource(NoiseType.GuardVoice, 10, 4, new Point(0, 4), "N"));
            //MapManager.AddNoiseSourse(new NoiseSource(NoiseType.GuardVoice, 100, 250, new Point(2, 2), "L"));
        }
        
        public static void SetMap(int width, int height, List<Tuple<Point, IDecor>> decors)
        {
            var content = new List<List<string>>();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    content.Add(new List<string> { "." });
                }
            }
            MapManager.CreateMap(width, height, content);

            foreach (var tuple in decors)
            {
                if (MapManager.InBounds(tuple.Item1))
                    MapManager.Map[tuple.Item1.X, tuple.Item1.Y].ObjectContainer.AddDecor(tuple.Item2);
            }
        }
    }
}
