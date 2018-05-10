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
        private const int timerInterval = 300;
        private const int ElementSize = 32;

        public GameWindow(DirectoryInfo imagesDirectory = null)
        {
            gameState = new GameState();
            ClientSize = new Size(
                ElementSize * MapManager.Map.Wigth,
                ElementSize * MapManager.Map.Height);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);
            var timer = new Timer();
            timer.Interval = timerInterval;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Burglar Game ver. 0.1";
            DoubleBuffered = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            GameState.KeyPressed = e.KeyCode;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(
                Brushes.Black , 0, 0, ElementSize * MapManager.Map.Wigth,
                ElementSize * MapManager.Map.Height);
            
            for (var i = 0; i < MapManager.Map.Wigth; i++)
            for (var j = 0; j < MapManager.Map.Height; j++)
            {
                e.Graphics.DrawImage(bitmaps[BackgroundFilenames[MapManager.Map[i, j].Type]],
                    new Point(i * ElementSize, j * ElementSize));

                foreach (var decor in MapManager.Map[i, j].ObjectContainer.GetAllDecors())
                {
                    e.Graphics.DrawImage(bitmaps[DecorFilenames[decor.Type]],
                        new Point(i * ElementSize, j * ElementSize));
                }
            }

            foreach (var a in MobileObjectsManager.MobileObjects)
                e.Graphics.DrawImage(bitmaps[CreatureFilenames[Tuple.Create(a.Type, a.Direction)]],
                    new Point(a.Position.X * ElementSize, a.Position.Y * ElementSize));

            e.Graphics.ResetTransform();
        }

        private void TimerTick(object sender, EventArgs args)
        {
            gameState.UpdateState();
            Invalidate();

            GameState.KeyPressed = Keys.None;
        }

        private readonly Dictionary<Tuple<CreatureTypes, Direction>, string> CreatureFilenames = new Dictionary<Tuple<CreatureTypes, Direction>, string>
        {
            {Tuple.Create(CreatureTypes.Guard, Direction.Down), "guard.png"},
            {Tuple.Create(CreatureTypes.Player, Direction.Down), "player.png"}
        };

        private readonly Dictionary<CellType, string> BackgroundFilenames = new Dictionary<CellType, string>
        {
            {CellType.Wood, "wood.png"}
        };

        private readonly Dictionary<DecorType, string> DecorFilenames = new Dictionary<DecorType, string>
        {
            {DecorType.BrokenPieces, "broken_pieces.png"},
            {DecorType.Button, "button.png"},
            {DecorType.Carpet, "carpet.png"},
            {DecorType.Chair, "chair.png"},
            {DecorType.ClosedDoor, "closed_door.png"},
            {DecorType.ClosedCupboard, "closed_cupboard.png"},
            {DecorType.Lock, "lock.png"},
            {DecorType.OpenedCuboard, "opened_cupboard.png"},
            {DecorType.OpenedDoor, "opened_door.png"},
            {DecorType.Table, "table.png"},
            {DecorType.Wall, "wall.png"},
            {DecorType.BurglaryToolkit, "burglary_toolkit.png"},
            {DecorType.Jewel, "jewel.png"},
            {DecorType.Key, "key.png"},
            {DecorType.Painting, "painting.png"},
            {DecorType.Treasure, "treasure.png"},
            {DecorType.Vase, "vase.png"}
        };
    }
}
