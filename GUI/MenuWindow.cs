using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameThief.GUI
{
    public class MenuWindow : Form
    {
        private Color buttonTextColor = Color.Red;
        private Color buttonColor = Color.Lavender;
        private Size buttonSize = new Size(300, 60);
        private Font buttonFont = new Font(new FontFamily("Impact"), 30, FontStyle.Bold);
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();

        private readonly Dictionary<string, Point> controlElements = new Dictionary<string, Point>
        {
            {"back.png", new Point(0, 0)}
        };

        public MenuWindow(DirectoryInfo imagesDirectory = null)
        {
            var gameWindow = new GameWindow();
            gameWindow.Timer.Stop();
            var settingsWindow = new SettingsWindow();
            ClientSize = new Size(400, 400);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Menu");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);

            var startButton = new Button
            {
                Location = new Point(50, 60),
                Size = buttonSize,
                Text = "NEW GAME",
                BackColor = buttonColor,
                Font = buttonFont,
                ForeColor = buttonTextColor
            };

            Controls.Add(startButton);

            var saveButton = new Button
            {
                Location = new Point(50, startButton.Bottom + 50),
                Size = buttonSize,
                Text = "LOAD GAME",
                BackColor = buttonColor,
                Font = buttonFont,
                ForeColor = buttonTextColor
            };

            Controls.Add(saveButton);

            var settingsButton = new Button
            {
                Location = new Point(50, saveButton.Bottom + 50),
                Size = buttonSize,
                Text = "SETTINGS",
                BackColor = buttonColor,
                Font = buttonFont,
                ForeColor = buttonTextColor
            };

            Controls.Add(settingsButton);

            startButton.Click += (sender, args) =>
            {
                gameWindow.Timer.Start();
                gameWindow.Show();
                Hide();
            };

            saveButton.Click += (sender, args) =>
            {
                System.Media.SystemSounds.Exclamation.Play();
                MessageBox.Show("No saved games!", ":(", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            };

            settingsButton.Click += (sender, args) =>
            {
                settingsWindow.Show();
                Hide();
            };

            settingsWindow.FormClosed += (sender, args) => Show();
            gameWindow.FormClosed += (sender, args) =>
            {
                //System.Media.SoundPlayer;
                Show();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Burglar Game ver. 0.1";
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var contr in controlElements)
            {
                e.Graphics.DrawImage(bitmaps[contr.Key], contr.Value);
            }
        }
    }
}
