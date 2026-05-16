using System.Drawing;
using Drawing.Models;

namespace Drawing.Services
{
    public class Painter
    {
        public void Paint(Graphics g, PictureBuilder picture)
        {
            if (picture.HouseBody == null) return;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            
            g.FillRectangle(Brushes.NavajoWhite, picture.HouseBody.Left, picture.HouseBody.Top, picture.HouseBody.Width, picture.HouseBody.Height);
            g.DrawRectangle(Pens.Black, picture.HouseBody.Left, picture.HouseBody.Top, picture.HouseBody.Width, picture.HouseBody.Height);

            
            Point[] roofPoints = {
                new Point(picture.Roof.Left, picture.Roof.Top + picture.Roof.Height),
                new Point(picture.Roof.Left + picture.Roof.Width, picture.Roof.Top + picture.Roof.Height),
                new Point(picture.Roof.Left + (picture.Roof.Width / 2), picture.Roof.Top)
            };
            g.FillPolygon(Brushes.Firebrick, roofPoints);
            g.DrawPolygon(Pens.Black, roofPoints);

            
            g.FillRectangle(Brushes.SkyBlue, picture.Window.Left, picture.Window.Top, picture.Window.Width, picture.Window.Height);
            g.DrawRectangle(Pens.Black, picture.Window.Left, picture.Window.Top, picture.Window.Width, picture.Window.Height);
        }
    }
}