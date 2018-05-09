using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;

namespace GameThief.GUI
{
    public class GameWindow : Form
    {
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private readonly GameState gameState;
        private readonly Keys pressedKeys;
        //private int tickCount;
        private const int ElementSize = 32;

        public GameWindow(DirectoryInfo imagesDirectory = null)
        {
            gameState = new GameState();
            ClientSize = new Size(
                ElementSize * MapManager.Map.Wigth,
                ElementSize * MapManager.Map.Height + ElementSize);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);
            var timer = new Timer();
            timer.Interval = 15;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Digger";
            DoubleBuffered = true;
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    pressedKeys.Add(e.KeyCode);
        //    Game.KeyPressed = e.KeyCode;
        //}

        //protected override void OnKeyUp(KeyEventArgs e)
        //{
        //    pressedKeys.Remove(e.KeyCode);
        //    Game.KeyPressed = pressedKeys.Any() ? pressedKeys.Min() : Keys.None;
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, ElementSize);
            e.Graphics.FillRectangle(
                Brushes.Black, 0, 0, ElementSize * MapManager.Map.Wigth,
                ElementSize * MapManager.Map.Height);
            foreach (var a in MobileObjectsManager.MobileObjects)
                e.Graphics.DrawImage(bitmaps[CreatureFilenames[a.Type]], a.Position);
            e.Graphics.ResetTransform();
            //e.Graphics.DrawString(Game.Scores.ToString(), new Font("Arial", 16), Brushes.Green, 0, 0);
        }

        private void TimerTick(object sender, EventArgs args)
        {
            //if (tickCount == 0)
            gameState.UpdateState();
            //foreach (var e in gameState.Animations)
            //    e.Location = new Point(e.Location.X + 4 * e.Command.DeltaX, e.Location.Y + 4 * e.Command.DeltaY);
            //if (tickCount == 7)
            //    gameState.EndAct();
            //tickCount++;
            //if (tickCount == 8) tickCount = 0;
            Invalidate();
        }

        private readonly Dictionary<CreatureTypes, string> CreatureFilenames = new Dictionary<CreatureTypes, string>
        {
            {CreatureTypes.Guard, "guard.png"},
            {CreatureTypes.Player, "player.png"}
        };
    }
}
