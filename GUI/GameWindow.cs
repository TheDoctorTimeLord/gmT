using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.Managers;
using GameThief.GameModel.ServiceClasses;
using Timer = System.Windows.Forms.Timer;

namespace GameThief.GUI
{
    public class GameWindow : Form
    {
        private const int TimerInterval = 300;
        private const int ElementSize = 42;
        private Point messagePosionion = new Point(180, 180);
        public readonly Timer Timer;

        private readonly Dictionary<CellType, string> BackgroundFilenames = new Dictionary<CellType, string>
        {
            {CellType.Wood, "wood.png"}
        };

        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private static readonly Dictionary<string, SoundPlayer> Sounds = new Dictionary<string, SoundPlayer>();

        private readonly Dictionary<Tuple<CreatureTypes, Direction>, string> CreatureFilenames =
            new Dictionary<Tuple<CreatureTypes, Direction>, string>
            {
                {Tuple.Create(CreatureTypes.Guard, Direction.Up), "guard_back.png"},
                {Tuple.Create(CreatureTypes.Guard, Direction.Down), "guard_front.png"},
                {Tuple.Create(CreatureTypes.Guard, Direction.Left), "guard_left.png"},
                {Tuple.Create(CreatureTypes.Guard, Direction.Right), "guard_rigth.png"},
                {Tuple.Create(CreatureTypes.Player, Direction.Up), "player_back.png"},
                {Tuple.Create(CreatureTypes.Player, Direction.Down), "player_front.png"},
                {Tuple.Create(CreatureTypes.Player, Direction.Left), "player_left.png"},
                {Tuple.Create(CreatureTypes.Player, Direction.Right), "player_rigth.png"}
            };

        private readonly Dictionary<DecorType, string> DecorFilenames = new Dictionary<DecorType, string>
        {
            //{DecorType.BrokenPieces, "broken_pieces.png"},
            //{DecorType.Button, "button.png"},
            //{DecorType.Carpet, "carpet.png"},
            {DecorType.Chair, "chair.png"},
            {DecorType.ClosedDoor, "closed_door.png"},
            //{DecorType.ClosedCupboard, "closed_cupboard.png"},
            //{DecorType.Lock, "lock.png"},
            //{DecorType.OpenedCuboard, "opened_cupboard.png"},
            {DecorType.OpenedDoor, "opened_door.png"},
            {DecorType.Table, "table.png"},
            {DecorType.Wall, "wall.png"},
            //{DecorType.BurglaryToolkit, "burglary_toolkit.png"},
            {DecorType.Jewel, "jewel.png"},
            //{DecorType.Key, "key.png"},
            {DecorType.PaintingFlowers, "painting_flowers.png"},
            {DecorType.PaintingHouse, "painting_house.png"},
            {DecorType.Treasure, "treasure.png"},
            {DecorType.Vase, "vase.png"},
            {DecorType.Mirror, "mirror.png"},
            {DecorType.Barrel, "barrel.png"},
            {DecorType.Plant, "plant.png"},
            {DecorType.Window, "window.png"}
        };

        private static readonly Dictionary<NoiseType, string> Noises = new Dictionary<NoiseType, string>
        {
            {NoiseType.StepsOfGuard, "guard_steps.wav"},
            {NoiseType.StepsOfThief, "burglar_steps.wav"},
            {NoiseType.Pain, "pain.wav"},
            {NoiseType.Interact, "interact.wav"},
            {NoiseType.Win, "win.wav"}
        };

        private readonly GameState gameState;

        public GameWindow(DirectoryInfo imagesDirectory = null)
        {
            gameState = new GameState();
            GameSetter.CreateLevel(gameState);
            gameState.UpdateState();
            ClientSize = new Size(
                ElementSize * (MapManager.Map.Wigth + 1),
                ElementSize * MapManager.Map.Height);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);

            var soundsDirectory = new DirectoryInfo("Sounds");

            foreach (var e in soundsDirectory.GetFiles("*.wav"))
                Sounds[e.Name] = new SoundPlayer(e.FullName);

            Timer = new Timer { Interval = TimerInterval };
            Timer.Tick += TimerTick;
            Timer.Start();

            //FormClosed += (sender, args) =>
            //{
            //    if (gameState.PlayerLost)
            //    {
            //        Sounds[Noises[NoiseType.Pain]].Stop();
            //        //PlayLost.EndInvoke(PlayingSound);
            //    }

            //    if (gameState.PlayerWon)
            //    {
            //        Sounds[Noises[NoiseType.Win]].Stop(); ;
            //        //PlayWon.EndInvoke(PlayingSound);
            //    }
            //};
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
                Brushes.Lavender, 0, 0, ElementSize * MapManager.Map.Wigth,
                ElementSize * MapManager.Map.Height);

            DrawMap(e);

            foreach (var a in MobileObjectsManager.MobileObjects)
                if (gameState.Player.VisibleCells.Contains(new Point(a.Position.X, a.Position.Y)))
                    e.Graphics.DrawImage(bitmaps[CreatureFilenames[Tuple.Create(a.Type, a.Direction)]],
                        new Point((a.Position.X + 1) * ElementSize, a.Position.Y * ElementSize));

            CoverInvisible(e);

            foreach (var noise in gameState.Player.AudibleNoises)
            {
                e.Graphics.DrawImage(bitmaps["sound.png"],
                    new Point((noise.Source.Position.X + 1) * ElementSize, noise.Source.Position.Y * ElementSize));

                if (Noises.ContainsKey(noise.Source.Type))
                    Sounds[Noises[noise.Source.Type]].Play();
            }

            var k = 0;
            foreach (var item in gameState.Player.Inventory.Items)
            {
                e.Graphics.DrawImage(bitmaps[DecorFilenames[item.Type]], new Point(0, k * ElementSize));
                k++;
            }

            if (gameState.PlayerWon)
            {
                e.Graphics.DrawString($"WIN!!! SCORE: {gameState.Player.Inventory.Cost}",
                    new Font(new FontFamily("Impact"), 90), Brushes.LawnGreen, new Point(120, 180));
                //Action act = () => Sounds[Noises[NoiseType.Win]].PlayLooping();
                //act.BeginInvoke(null, null);
                PlayingSound = PlayWon.BeginInvoke(null, null);
            }
            if (gameState.PlayerLost)
            {
                e.Graphics.DrawString("YOU DEAD", new Font(new FontFamily("Impact"), 150), Brushes.Red, messagePosionion);
                //PlaySounds.BeginInvoke(null, null);
                //Action act = () => Sounds[Noises[NoiseType.Pain]].PlaySync();
                //act.BeginInvoke(null, null);
                //PlayingSound = PlayLost.BeginInvoke(null, null);
                //Sounds[Noises[NoiseType.Pain]].Play();
            }
        }

        private void CoverInvisible(PaintEventArgs e)
        {
            for (var i = 0; i < MapManager.Map.Wigth; i++)
                for (var j = 0; j < MapManager.Map.Height; j++)
                    if (!gameState.Player.VisibleCells.Contains(new Point(i, j)))
                        e.Graphics.DrawImage(bitmaps["darkness.png"],
                            new Point((i + 1) * ElementSize, j * ElementSize));
        }

        private void DrawMap(PaintEventArgs e)
        {
            for (var i = 0; i < MapManager.Map.Wigth; i++)
                for (var j = 0; j < MapManager.Map.Height; j++)
                {
                    e.Graphics.DrawImage(bitmaps[BackgroundFilenames[MapManager.Map[i, j].Type]],
                        new Point((i + 1) * ElementSize, j * ElementSize));

                    foreach (var decor in MapManager.Map[i, j].ObjectContainer.GetAllDecors())
                    {
                        if (decor is Item && !gameState.Player.VisibleCells.Contains(new Point(i, j)))
                            continue;

                        e.Graphics.DrawImage(bitmaps[DecorFilenames[decor.Type]],
                        new Point((i + 1) * ElementSize, j * ElementSize));
                    }
                }
        }

        private void TimerTick(object sender, EventArgs args)
        {
            gameState.UpdateState();
            //if (gameState.PlayerLost)
            //{
            //    PlaySounds.BeginInvoke(null, null);
            //    Sounds[Noises[NoiseType.Pain]].Play();
            //}
            Invalidate();
            GameState.KeyPressed = Keys.None;
        }

        private readonly Action PlayLost = () => Sounds[Noises[NoiseType.Pain]].PlaySync();
        private readonly Action PlayWon = () => Sounds[Noises[NoiseType.Win]].PlaySync();
        private IAsyncResult PlayingSound;
    }
}