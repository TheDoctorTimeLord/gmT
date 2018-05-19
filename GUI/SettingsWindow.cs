using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameThief.GameModel.Enums;

namespace GameThief.GUI
{
    public class SettingsWindow : Form
    {
        private Color textColor = Color.Red;
        private Color boxColor = Color.Lavender;
        private Size boxSize = new Size(155, 140);
        private Size buttonSize = new Size(125, 30);
        private Font textFont = new Font(new FontFamily("Impact"), 15, FontStyle.Bold);
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();

        private readonly Dictionary<string, Point> controlElements = new Dictionary<string, Point>
        {
            {"back.png", new Point(0, 0)}
        };

        public SettingsWindow(DirectoryInfo imagesDirectory = null)
        {
            ClientSize = new Size(400, 400);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Menu");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);

            var forwardBox = CreateGroupBox(new Point(35, 30), "FORWARD", "W", "UP");
            Controls.Add(forwardBox);

            var leftBox = CreateGroupBox(new Point(forwardBox.Right + 20, forwardBox.Top), "LEFT TURN", "A", "LEFT");
            Controls.Add(leftBox);

            var rightBox = CreateGroupBox(new Point(forwardBox.Left, forwardBox.Bottom + 20), "RIGHT TURN", "D",
                "RIGHT");
            Controls.Add(rightBox);

            var interactBox = CreateGroupBox(new Point(leftBox.Left, leftBox.Bottom + 20), "INTERACT", "E", "ENTER");
            Controls.Add(interactBox);

            var button = new Button
            {
                Location = new Point(137, interactBox.Bottom + 20),
                Size = buttonSize,
                Text = "SAVE",
                Font = textFont,
                ForeColor = textColor,
                BackColor = boxColor
            };
            Controls.Add(button);

            var forward = forwardBox.Controls.OfType<RadioButton>();
            var left = leftBox.Controls.OfType<RadioButton>();
            var right = rightBox.Controls.OfType<RadioButton>();
            var interact = interactBox.Controls.OfType<RadioButton>();

            var buttons = forward.Concat(left).Concat(right).Concat(interact);

            button.Click += (sender, args) =>
            {
                foreach (var b in buttons)
                {
                    if (b.Checked)
                        switch (b.Text)
                        {
                            case "W":
                                PressedKeyConverter.ChangeMatching(Keys.W, Query.Move);
                                break;
                            case "A":
                                PressedKeyConverter.ChangeMatching(Keys.A, Query.RotateLeft);
                                break;
                            case "D":
                                PressedKeyConverter.ChangeMatching(Keys.W, Query.RotateRight);
                                break;
                            case "E":
                                PressedKeyConverter.ChangeMatching(Keys.E, Query.Interaction);
                                break;
                            case "UP":
                                PressedKeyConverter.ChangeMatching(Keys.Up, Query.Move);
                                break;
                            case "LEFT":
                                PressedKeyConverter.ChangeMatching(Keys.Left, Query.RotateLeft);
                                break;
                            case "RIGHT":
                                PressedKeyConverter.ChangeMatching(Keys.Right, Query.RotateRight);
                                break;
                            case "ENTER":
                                PressedKeyConverter.ChangeMatching(Keys.Enter, Query.Interaction);
                                break;
                        }
                }
                Close();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Burglar Game Settings";
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var contr in controlElements)
            {
                e.Graphics.DrawImage(bitmaps[contr.Key], contr.Value);
            }
        }

        private GroupBox CreateGroupBox(Point location, string label, string x, string y)
        {
            return new GroupBox
            {
                Location = location,
                Size = boxSize,
                BackColor = boxColor,
                Controls =
                {
                    new Label
                    {
                        Location = new Point(15, 15),
                        Size = buttonSize,
                        Text = label,
                        Font = textFont,
                        ForeColor = textColor
                    },
                    new RadioButton
                    {
                        Location = new Point(15, 60),
                        Size = buttonSize,
                        Text = x,
                        Font = textFont,
                        ForeColor = textColor
                    },
                    new RadioButton
                    {
                        Location = new Point(15, 100),
                        Size = buttonSize,
                        Text = y,
                        Font = textFont,
                        ForeColor = textColor
                    }
                }
            };
        }
    }
}
