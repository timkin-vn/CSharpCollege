using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.PresenterServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel picture, bool interactiveMode)
        {
            if (picture == null)
            {
                return;
            }

            Pen pen;
            foreach (var rect in picture.Rectangles)
            {
                pen = new Pen(rect.DrawColor, 3);

                // Предполагаем, что у вас есть свойство Image в rect, которое содержит изображение
                if (rect.ImageRect != null)
                {
                    // Отрисовка изображения внутри прямоугольника
                    g.DrawImage(rect.ImageRect, rect.Rectangle);
                }
                else
                {
                    // Если изображения нет, можно заполнить цветом
                    using (Brush brush = new SolidBrush(rect.FillColor))
                    {
                        g.FillRectangle(brush, rect.Rectangle);
                    }
                }

                g.DrawRectangle(pen, rect.Rectangle);
            }

            if (interactiveMode)
            {
                // Нарисовать маркеры
                var activeBrush = Brushes.Black;
                var inactiveBrush = Brushes.White;
                pen = Pens.Black;

                foreach (var marker in picture.Markers)
                {
                    Brush brush = marker.IsActive ? activeBrush : inactiveBrush;
                    g.FillRectangle(brush, marker.Rectangle);
                    g.DrawRectangle(pen, marker.Rectangle);
                }
            }
        }
    }
}
    
