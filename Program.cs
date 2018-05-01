using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
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
            //var pl = true;
            var player = new Player(new InitializationMobileObject(new Point(1, 1), Direction.Right));
            MobileObjectsManager.CreateCreature(player);

            while (true)
            {
                var noises = player.AudibleNoises == null
                    ? new List<Point>()
                    : player.AudibleNoises.Select(n => n.Source.Position).ToList();
                Console.WriteLine(Colol(noises));
                var a = Console.ReadKey();
                Console.WriteLine("");
                GameState.KeyPressed = Conv(a.KeyChar);
                st.UpdateState();
                Console.Clear();
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

        static string Colol(List<Point> noises)
        {
            var str = new StringBuilder();
            var c = " ";

            for (var i = 0; i < MapManager.Map.Height; i++)
            {
                for (var j = 0; j < MapManager.Map.Wigth; j++)
                {
                    c = MapManager.Map[j, i].BackgroundFilename;
                    c = noises.Contains(new Point(j, i)) ? "N" : c;
                    c = MapManager.Map[j, i].Creature is Player ? "P" : c;
                    c = MapManager.Map[j, i].Creature is Guard ? "G" : c;
                    str.Append(c);
                }

                str.Append("\n");
            }

            return str.ToString();
        }
    }
}
