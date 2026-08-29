using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture
{
    public partial class PictureForm : Form
    {
        public PictureForm()
        {
            InitializeComponent();
        }

        private void PictureForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            var bounds = ClientRectangle;
            var pen = new Pen(Color.DarkSlateGray, 3);
            var brushBody = Brushes.LightGray;
            var brushEye = Brushes.Black;
            var brushButton = Brushes.Blue;

            
            var headRect = new Rectangle
            {
                X = bounds.Width * 2 / 5,
                Y = bounds.Height / 10,
                Width = bounds.Width / 5,
                Height = bounds.Height / 6,
            };
            g.FillRectangle(brushBody, headRect);
            g.DrawRectangle(pen, headRect);

            
            var eyeSize = bounds.Width / 30;
            var leftEyeRect = new Rectangle
            {
                X = headRect.X + headRect.Width / 4 - eyeSize / 2,
                Y = headRect.Y + headRect.Height / 3 - eyeSize / 2,
                Width = eyeSize,
                Height = eyeSize,
            };
            var rightEyeRect = new Rectangle
            {
                X = headRect.X + headRect.Width * 3 / 4 - eyeSize / 2,
                Y = headRect.Y + headRect.Height / 3 - eyeSize / 2,
                Width = eyeSize,
                Height = eyeSize,
            };
            g.FillEllipse(brushEye, leftEyeRect);
            g.FillEllipse(brushEye, rightEyeRect);

            
            g.DrawLine(pen,
                headRect.Left + headRect.Width / 4,
                headRect.Bottom - headRect.Height / 4,
                headRect.Right - headRect.Width / 4,
                headRect.Bottom - headRect.Height / 4);

            
            var bodyRect = new Rectangle
            {
                X = bounds.Width * 2 / 5,
                Y = headRect.Bottom + bounds.Height / 20,
                Width = bounds.Width / 5,
                Height = bounds.Height / 3,
            };
            g.FillRectangle(brushBody, bodyRect);
            g.DrawRectangle(pen, bodyRect);

            
            var armWidth = bounds.Width / 12;
            var armHeight = bodyRect.Height / 3;
            var leftArmRect = new Rectangle
            {
                X = bodyRect.Left - armWidth - 10,
                Y = bodyRect.Top + bodyRect.Height / 6,
                Width = armWidth,
                Height = armHeight,
            };
            g.FillRectangle(brushBody, leftArmRect);
            g.DrawRectangle(pen, leftArmRect);

            
            var rightArmRect = new Rectangle
            {
                X = bodyRect.Right + 10,
                Y = bodyRect.Top + bodyRect.Height / 6,
                Width = armWidth,
                Height = armHeight,
            };
            g.FillRectangle(brushBody, rightArmRect);
            g.DrawRectangle(pen, rightArmRect);

            
            var legWidth = bodyRect.Width / 4;
            var legHeight = bounds.Height / 5;
            var leftLegRect = new Rectangle
            {
                X = bodyRect.Left + bodyRect.Width / 8 - legWidth / 2,
                Y = bodyRect.Bottom,
                Width = legWidth,
                Height = legHeight,
            };
            g.FillRectangle(brushBody, leftLegRect);
            g.DrawRectangle(pen, leftLegRect);

            
            var rightLegRect = new Rectangle
            {
                X = bodyRect.Right - bodyRect.Width / 8 - legWidth / 2,
                Y = bodyRect.Bottom,
                Width = legWidth,
                Height = legHeight,
            };
            g.FillRectangle(brushBody, rightLegRect);
            g.DrawRectangle(pen, rightLegRect);

            
            g.DrawLine(pen,
                headRect.X + headRect.Width / 2, headRect.Y,
                headRect.X + headRect.Width / 2, headRect.Y - bounds.Height / 10);

            var antennaCircleRect = new Rectangle
            {
                X = headRect.X + headRect.Width / 2 - eyeSize / 2,
                Y = headRect.Y - bounds.Height / 10 - eyeSize / 2,
                Width = eyeSize,
                Height = eyeSize,
            };
            g.FillEllipse(Brushes.Red, antennaCircleRect);

            
            var buttonWidth = bodyRect.Width / 6;
            var buttonHeight = bodyRect.Height / 10;
            var buttonMargin = bodyRect.Width / 10;

            for (int i = 0; i < 3; i++)
            {
                var buttonRect = new Rectangle
                {
                    X = bodyRect.X + buttonMargin,
                    Y = bodyRect.Y + buttonMargin + i * (buttonHeight + buttonMargin),
                    Width = buttonWidth,
                    Height = buttonHeight,
                };
                g.FillRectangle(brushButton, buttonRect);
                g.DrawRectangle(pen, buttonRect);
            }
        }


        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
