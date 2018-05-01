using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.Managers;
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
            var flag = true;
            var player = new Player(new InitializationMobileObject(new Point(1, 1), Direction.Right));
            MobileObjectsManager.CreateCreature(player);
            var wall = new Wall();
            var pt = new Painting();
            MapManager.Map[0, 0].ObjectContainer.AddDecor(wall);
            //MapManager.Map[0, 0].ObjectContainer.AddDecor(pt);

            while (true)
            {
                var a = Console.ReadKey();
                GameState.KeyPressed = Conv(a.KeyChar);
                Console.WriteLine("");

                Console.Clear();

                st.UpdateState();

                if (flag)
                {
                    flag = false;
                }
                else
                {
                    MapManager.Map[0, 0].ObjectContainer.RemoveDecor(wall);
                }

                var vis = player.VisibleCells == null
                    ? new List<Point>()
                    : player.VisibleCells.Concat(new List<Point> { player.GetPosition() }).ToList();
                var noises = player.AudibleNoises;
                Console.WriteLine(Colol(vis));
            }
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

        static string Colol(List<Point> vis)
        {
            var str = new StringBuilder();
            var c = " ";

            for (var i = 0; i < MapManager.Map.Height; i++)
            {
                for (var j = 0; j < MapManager.Map.Wigth; j++)
                {
                    if (vis.Contains(new Point(j, i)))
                    {
                        c = MapManager.Map[j, i].BackgroundFilename;

                        //c = noises.Contains(new Point(j, i)) ? "N" : c;

                        foreach (var ch in MapManager.Map[j, i].ObjectContainer.GetAllDecors())
                        {
                            c = ch is Wall ? "W" : c;
                            c = ch is Painting ? "g" : c;
                        }

                        c = MapManager.Map[j, i].Creature is Player ? "P" : c;
                        c = MapManager.Map[j, i].Creature is Guard ? "G" : c;
                    }
                    else
                        c = "x";

                    str.Append(c);
                }

                str.Append("\n");
            }

            return str.ToString();
        }
    }
}
