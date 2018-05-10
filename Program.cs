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
            //Application.Run(new Form1());
            //var st = new GameState();
            var player = new Player(new InitializationMobileObject(new Point(0, 0), Direction.Left));

            Map1(player);
            Application.Run(new GameWindow());

            //while (true)
            //{
            //    var a = Console.ReadKey();
            //    GameState.KeyPressed = Conv(a.KeyChar);
            //    Console.WriteLine("");

            //    Console.Clear();

            //    st.UpdateState();

            //    var vis = MapManager.GetVisibleCells(player.Position, player.Direction, player.ViewDistanse,
            //        player.ViewWidth).ToList();
            //    var noises = MapManager.GetAudibleNoises(player.Position, player.MaxHearingDelta, player.MinHearingVolume)
            //        .ToDictionary(noise => noise.Source.Position);
            //    Console.WriteLine(Drawing(vis, noises, player.Inventory));
            //}
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

            MapManager.AddNoiseSourse(new NoiseSource(NoiseType.GuardVoice, 10, 4, new Point(0, 4), "N"));
            MapManager.AddNoiseSourse(new NoiseSource(NoiseType.GuardVoice, 100, 250, new Point(2, 2), "L"));
        }

        private static void Map2(Player player)
        {
            SetMap(5, 5, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(3, 2), (IDecor)new Wall()),
                Tuple.Create(new Point(3, 3), (IDecor)new Wall()),
                Tuple.Create(new Point(3, 4), (IDecor)new Wall()),
                Tuple.Create(new Point(4, 1), (IDecor)new Wall())
            });

            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                player,
                MobileObjectsManager.GetCreatureByNameAndInitParams(
                    CreatureTypes.Guard, new InitializationMobileObject(new Point(4, 3), Direction.Up))
            });
        }

        private static void Map3(Player player)
        {
            SetMap(5, 5, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(3, 2), (IDecor)new Wall()),
                Tuple.Create(new Point(3, 3), (IDecor)new Wall()),
                Tuple.Create(new Point(3, 4), (IDecor)new Wall()),
            });

            GameInformationManager.CreateTrackByName(new Dictionary<string, List<Instruction>>
            {
                { "track1", new List<Instruction>
                    {
                        new Instruction(new List<string>{"MoveTo", "2", "0"}),
                        new Instruction(new List<string>{"MoveTo", "4", "3"})
                    }
                }
            });

            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                player,
                MobileObjectsManager.GetCreatureByNameAndInitParams(
                    CreatureTypes.Guard, new InitializationMobileObject(
                        new Point(4, 3), 10, 10, Direction.Up, 1, 4, 4, 3, new Inventory(10),  new List<Tuple<string, string>>{Tuple.Create("path", "track1")}))
            });

            MapManager.NoiseController.AddNoiseSource(new NoiseSource(NoiseType.GuardVoice, 10, 10, new Point(0, 4), "N"));
        }

        private static void Map4(Player player)
        {
            SetMap(5, 5, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(1, 0), (IDecor)new Wall()),
                Tuple.Create(new Point(1, 1), (IDecor)new Wall()),
            });

            GameInformationManager.CreateTrackByName(new Dictionary<string, List<Instruction>>
            {
                { "track1", new List<Instruction>
                    {
                        new Instruction(new List<string>{"MoveTo", "2", "0"}),
                        new Instruction(new List<string>{"MoveTo", "4", "3"})
                    }
                }
            });

            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                player,
                MobileObjectsManager.GetCreatureByNameAndInitParams(
                    CreatureTypes.Guard, new InitializationMobileObject(
                        new Point(4, 3), 10, 10, Direction.Up, 1, 4, 2, 4, new Inventory(10),  new List<Tuple<string, string>>{Tuple.Create("path", "track1")}))
            });

            MapManager.NoiseController.AddNoiseSource(new NoiseSource(NoiseType.StepsOfThief, 10, 100, new Point(0, 4), "N"));
        }

        private static Keys Conv(char a)
        {
            switch (a)
            {
                case 'd':
                    return Keys.D;
                case 'e':
                    return Keys.E;
                case 'w':
                    return Keys.W;
                case 'a':
                    return Keys.A;
                default:
                    return Keys.None;
            }
        }

        static string Drawing(List<Point> vis, Dictionary<Point, Noise> noises, Inventory inventory)
        {
            var str = new StringBuilder();

            for (var i = 0; i < MapManager.Map.Height; i++)
            {
                for (var j = 0; j < MapManager.Map.Wigth; j++)
                {
                    var c = " ";

                    if (vis.Contains(new Point(j, i)))
                    {
                        c = MapManager.Map[j, i].Type.ToString();

                        //var point = new Point(j, i);
                        //c = noises.ContainsKey(point) ? noises[point].Source.Message : c;

                        foreach (var ch in MapManager.Map[j, i].ObjectContainer.GetAllDecors())
                        {
                            c = ch is Wall ? "W" : c;
                            c = ch is Painting ? "p" : c;
                            c = ch is Jewel ? "J" : c;
                            c = ch is ClosedDoor ? "D" : c;
                            c = ch is OpenedDoor ? "d" : c;
                            c = ch is Lock ? "o" : c;
                        }

                        c = MapManager.Map[j, i].Creature is Player ? "P" : c;
                        c = MapManager.Map[j, i].Creature is Guard ? "G" : c;
                    }
                    else
                        c = "x";

                    var point = new Point(j, i);
                    c = noises.ContainsKey(point) ? noises[point].Source.Message : c;

                    str.Append(c);
                }

                str.Append("\n");
            }
            str.Append("\n");
            foreach (var item in inventory.Items)
            {
                var c = "";
                c = item is Jewel ? "J" : c;
                c = item is Key ? "K" : c;
                str.Append(c);
            }
            

            return str.ToString();
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
