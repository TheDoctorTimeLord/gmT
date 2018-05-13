using System;
using System.Windows.Forms;
using GameThief.GUI;

namespace GameThief
{
    internal static class Program
    {
        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.Run(new GameWindow());
        }
    }
}