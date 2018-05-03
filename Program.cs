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
using GameThief.GameModel.ServiceClasses;

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
            var st = new GameState();
            var player = new Player(new InitializationMobileObject(new Point(2, 2), Direction.Left));

            Map3(player);

            while (true)
            {
                var a = Console.ReadKey();
                GameState.KeyPressed = Conv(a.KeyChar);
                Console.WriteLine("");

                Console.Clear();

                st.UpdateState();

                var vis = player.VisibleCells == null
                    ? new List<Point>()
                    : player.VisibleCells.Concat(new List<Point> { player.GetPosition() }).ToList();
                var noises = player.AudibleNoises.ToDictionary(noise => noise.Source.Position);
                Console.WriteLine(Drawing(vis, noises));
            }
        }

        private static void Map1(Player player)
        {
            SetMap(5, 5, new List<Tuple<Point, IDecor>>
            {
                Tuple.Create(new Point(3, 0), (IDecor)new Wall()),
                Tuple.Create(new Point(3, 1), (IDecor)new Wall()),
                Tuple.Create(new Point(3, 2), (IDecor)new Wall()),
                Tuple.Create(new Point(3, 3), (IDecor)new Wall()),
                Tuple.Create(new Point(3, 4), (IDecor)new Wall())
            });

            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                player,
                MobileObjectsManager.GetCreatureByNameAndInitParams(
                    "Guard", new InitializationMobileObject(new Point(1, 0), Direction.Up))
            });

            MapManager.AddNoiseSourse(new NoiseSource(NoiseType.Guard, 10, 4, new Point(0, 4), "N"));
            MapManager.AddNoiseSourse(new NoiseSource(NoiseType.Guard, 10, 25, new Point(4, 2), "L"));
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
                    "Guard", new InitializationMobileObject(new Point(4, 3), Direction.Up))
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
                    "Guard", new InitializationMobileObject(
                        new Point(4, 3), 10, 10, Direction.Up, 1, 4, 4, 3, new List<Tuple<string, string>>{Tuple.Create("path", "track1")}))
            });

            MapManager.NoiseController.AddNoiseSource(new NoiseSource(NoiseType.Guard, 10, 10, new Point(0, 4), "N"));
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

        static string Drawing(List<Point> vis, Dictionary<Point, Noise> noises)
        {
            var str = new StringBuilder();

            for (var i = 0; i < MapManager.Map.Height; i++)
            {
                for (var j = 0; j < MapManager.Map.Wigth; j++)
                {
                    var c = " ";

                    if (vis.Contains(new Point(j, i)))
                    {
                        c = MapManager.Map[j, i].BackgroundFilename;

                        //var point = new Point(j, i);
                        //c = noises.ContainsKey(point) ? noises[point].Source.Message : c;

                        foreach (var ch in MapManager.Map[j, i].ObjectContainer.GetAllDecors())
                        {
                            c = ch is Wall ? "W" : c;
                            c = ch is Painting ? "p" : c;
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
