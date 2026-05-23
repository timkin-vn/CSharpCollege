using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HouseDrawing
{
    public partial class HouseForm : Form
    {
        public HouseForm()
        {
            Text = "Домик";
            BackColor = Color.White;
            ClientSize = new Size(900, 600);
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.ResetTransform();

            float designW = 900f;
            float designH = 600f;

            float sx = (float)ClientSize.Width / designW;
            float sy = (float)ClientSize.Height / designH;
            float s = Math.Min(sx, sy);

            float ox = (ClientSize.Width - designW * s) / 2f;
            float oy = (ClientSize.Height - designH * s) / 2f;

            Matrix m = new Matrix();
            m.Scale(s, s);
            m.Translate(ox, oy, MatrixOrder.Append);
            g.Transform = m;

            Pen outline = new Pen(Color.FromArgb(40, 40, 40), 2);
            Pen roofPen = new Pen(Color.FromArgb(90, 40, 40), 2);

            Brush houseBrush = new SolidBrush(Color.FromArgb(242, 220, 180));
            Brush roofBrush = new SolidBrush(Color.FromArgb(170, 70, 60));
            Brush eavesBrush = new SolidBrush(Color.FromArgb(150, 60, 50));
            Brush doorBrush = new SolidBrush(Color.FromArgb(120, 75, 40));
            Brush glassBrush = new SolidBrush(Color.FromArgb(170, 220, 245));
            Brush frameBrush = new SolidBrush(Color.FromArgb(245, 245, 245));
            Brush grassBrush = new SolidBrush(Color.FromArgb(160, 210, 120));
            Brush sunBrush = new SolidBrush(Color.FromArgb(255, 210, 70));
            Brush cloudBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            Brush pipeBrush = new SolidBrush(Color.FromArgb(160, 110, 90));
            Brush pipeTopBrush = new SolidBrush(Color.FromArgb(140, 95, 80));
            Brush doorHandleBrush = new SolidBrush(Color.Gold);

            int baseX = 280;
            int baseY = 220;

            Rectangle ground = new Rectangle(0, 430, 900, 170);
            g.FillRectangle(grassBrush, ground);

            Rectangle sun = new Rectangle(60, 50, 80, 80);
            g.FillEllipse(sunBrush, sun);
            g.DrawEllipse(outline, sun);

            Rectangle c1 = new Rectangle(170, 80, 70, 45);
            Rectangle c2 = new Rectangle(210, 60, 90, 55);
            Rectangle c3 = new Rectangle(260, 80, 70, 45);
            g.FillEllipse(cloudBrush, c1);
            g.FillEllipse(cloudBrush, c3);
            g.DrawEllipse(outline, c1);
            g.DrawEllipse(outline, c3);
            g.FillEllipse(cloudBrush, c2);
            g.DrawEllipse(outline, c2);

            int houseW = 270;
            int houseH = 210;

            Rectangle walls = new Rectangle(baseX, baseY, houseW, houseH);

            g.FillRectangle(houseBrush, walls);
            g.DrawRectangle(outline, walls);

            Point[] roof = {
                new Point(baseX - 20, baseY),
                new Point(baseX + houseW / 2, baseY - 110),
                new Point(baseX + houseW + 20, baseY)
            };
            g.FillPolygon(roofBrush, roof);
            g.DrawPolygon(roofPen, roof);

            Point[] eaves = {
                new Point(baseX - 25, baseY),
                new Point(baseX + houseW + 25, baseY),
                new Point(baseX + houseW + 10, baseY + 18),
                new Point(baseX - 10, baseY + 18),
            };
            g.FillPolygon(eavesBrush, eaves);
            g.DrawPolygon(roofPen, eaves);

            Rectangle pipe = new Rectangle(baseX + (int)(houseW * 0.70), baseY - 95, 35, 65);
            g.FillRectangle(pipeBrush, pipe);
            g.DrawRectangle(outline, pipe);

            Rectangle pipeTop = new Rectangle(pipe.X - 5, baseY - 105, 45, 15);
            g.FillRectangle(pipeTopBrush, pipeTop);
            g.DrawRectangle(outline, pipeTop);

            int doorW = 60;
            int doorH = 115;
            Rectangle door = new Rectangle(baseX + (houseW - doorW) / 2, baseY + 95, doorW, doorH);
            g.FillRectangle(doorBrush, door);
            g.DrawRectangle(outline, door);

            Rectangle doorHandle = new Rectangle(door.Right - 15, door.Y + 55, 10, 10);
            g.FillEllipse(doorHandleBrush, doorHandle);
            g.DrawEllipse(outline, doorHandle);

            int windowFrameSize = 55;
            int sidePad = 30;
            int winY = baseY + 60;

            Rectangle window1Frame = new Rectangle(baseX + sidePad, winY, windowFrameSize, windowFrameSize);
            Rectangle window2Frame = new Rectangle(baseX + houseW - sidePad - windowFrameSize, winY, windowFrameSize, windowFrameSize);

            DrawWindow(g, outline, frameBrush, glassBrush, window1Frame);
            DrawWindow(g, outline, frameBrush, glassBrush, window2Frame);

            outline.Dispose();
            roofPen.Dispose();

            houseBrush.Dispose();
            roofBrush.Dispose();
            eavesBrush.Dispose();
            doorBrush.Dispose();
            glassBrush.Dispose();
            frameBrush.Dispose();
            grassBrush.Dispose();
            sunBrush.Dispose();
            cloudBrush.Dispose();
            pipeBrush.Dispose();
            pipeTopBrush.Dispose();
            doorHandleBrush.Dispose();
        }

        private void DrawWindow(Graphics g, Pen outline, Brush frameBrush, Brush glassBrush, Rectangle frame)
        {
            g.FillRectangle(frameBrush, frame);
            g.DrawRectangle(outline, frame);

            Rectangle glass = new Rectangle(frame.X + 5, frame.Y + 5, frame.Width - 10, frame.Height - 10);
            g.FillRectangle(glassBrush, glass);
            g.DrawRectangle(outline, glass);

            int cx = glass.Left + glass.Width / 2;
            int cy = glass.Top + glass.Height / 2;

            g.DrawLine(outline, cx, glass.Top, cx, glass.Bottom);
            g.DrawLine(outline, glass.Left, cy, glass.Right, cy);
        }

        static void Main()
        {
            Application.Run(new HouseForm());
        }
    }
}