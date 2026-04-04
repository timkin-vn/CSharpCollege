using Drawing.Models;
using Drawing.ViewServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Services
{
    public enum PictureType
    {
        Train = 1,
        HouseScene = 2,
        SunAndClouds = 3,
        Abstract = 4
    }
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -5,
            Y = -1,
            Width = 19,
            Height = 12,
        };
        
        public PictureType CurrentPicture { get; set; } = PictureType.Train;

        public void DrawPicture(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 3);
            
            switch (CurrentPicture)
            {
                case PictureType.Train:
                    DrawTruck(painter, mainPen);
                    break;
                case PictureType.HouseScene:
                    DrawHouseScene(painter, mainPen);
                    break;
                case PictureType.SunAndClouds:
                    DrawSunAndCloudsScene(painter, mainPen);
                    break;
                case PictureType.Abstract:
                    DrawAbstractScene(painter);
                    break;
            }
        }
        
        private void DrawTruck(Painter painter, Pen mainPen)
        {
            var wheelBrush = Brushes.LightGray;

            // wheels
            var rect = new RectangleModel { X = -1, Y = 0, Width = 2, Height = 2, };
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            rect.X = 2;
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            rect.X = 5;
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            rect.X = 8;
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            // carcase
            var bodyBrush = Brushes.DarkGray;
            rect = new RectangleModel { X = 0, Y = 2, Width = 12, Height = 4, };
            painter.DrawRectangle(mainPen, bodyBrush, rect);

            // cabin
            rect = new RectangleModel { X = -4, Y = 2, Width = 4, Height = 8, };
            painter.DrawRectangle(mainPen, bodyBrush, rect);

            // window
            var windowBrush = Brushes.LightSkyBlue;
            rect = new RectangleModel { X = -2, Y = 6, Width = 1.5, Height = 3, };
            painter.DrawRectangle(mainPen, windowBrush, rect);
            
            rect = new RectangleModel { X = 11, Y = 2, Width = 2, Height = 4, };
            painter.DrawPie(mainPen, bodyBrush, rect, 270, 180);
            
            var sweeperBrush = Brushes.Red;
            var sweeperPoints = new[]
            {
                new PointModel { X = 11, Y = 2, },
                new PointModel { X = 12, Y = 2, },
                new PointModel { X = 13, Y = 0.5, },
                new PointModel { X = 11, Y = 0.5, },
            };
            painter.DrawPolygon(mainPen, sweeperBrush, sweeperPoints);
            
            var pipePoints = new[]
            {
                new PointModel { X = 8.5, Y = 9, },
                new PointModel { X = 10.5, Y = 9, },
                new PointModel { X = 10, Y = 6, },
                new PointModel { X = 9, Y = 6, },
            };
            painter.DrawPolygon(mainPen, bodyBrush, pipePoints);
        }
        
        private void DrawHouseScene(Painter painter, Pen mainPen)
        {
            // grass
            var grassRect = new RectangleModel { X = -5, Y = -1, Width = 19, Height = 2 };
            painter.DrawRectangle(null, Brushes.LimeGreen, grassRect);

            // walls
            var houseRect = new RectangleModel { X = 2, Y = 1, Width = 6, Height = 5 };
            painter.DrawRectangle(mainPen, Brushes.Peru, houseRect);

            // roof
            var roofPoints = new[]
            {
                new PointModel { X = 1.5, Y = 6 },
                new PointModel { X = 8.5, Y = 6 },
                new PointModel { X = 5, Y = 9 }
            };
            painter.DrawPolygon(mainPen, Brushes.IndianRed, roofPoints);

            // window
            var windowRect = new RectangleModel { X = 4, Y = 3, Width = 2, Height = 2 };
            painter.DrawRectangle(mainPen, Brushes.Yellow, windowRect);

            // tree
            var trunkRect = new RectangleModel { X = -3, Y = 1, Width = 1, Height = 2 };
            painter.DrawRectangle(mainPen, Brushes.SaddleBrown, trunkRect);

            // leafs
            var crownRect = new RectangleModel { X = -4.5, Y = 3, Width = 4, Height = 5 };
            painter.DrawEllipse(mainPen, Brushes.ForestGreen, crownRect);
        }
        
        private void DrawSunAndCloudsScene(Painter painter, Pen mainPen)
        {
            // sky
            var skyRect = new RectangleModel { X = -5, Y = -1, Width = 19, Height = 12 };
            painter.DrawRectangle(null, Brushes.LightSkyBlue, skyRect);

            // sun
            var sunRect = new RectangleModel { X = 10, Y = 8, Width = 3, Height = 3 };
            painter.DrawEllipse(new Pen(Color.Orange, 2), Brushes.Gold, sunRect);

            // clouds
            var cloudBrush = Brushes.White;
            var cloudPen = new Pen(Color.LightGray, 1);

            // cloud 1
            painter.DrawEllipse(cloudPen, cloudBrush, new RectangleModel { X = -2, Y = 7, Width = 4, Height = 2 });
            painter.DrawEllipse(cloudPen, cloudBrush, new RectangleModel { X = -1, Y = 8, Width = 3, Height = 2 });

            // cloud 2
            painter.DrawEllipse(cloudPen, cloudBrush, new RectangleModel { X = 4, Y = 6, Width = 5, Height = 2.5 });
            painter.DrawEllipse(cloudPen, cloudBrush, new RectangleModel { X = 5.5, Y = 7.5, Width = 3, Height = 2 });
        }
        
        private void DrawAbstractScene(Painter painter)
        {
            painter.DrawRectangle(null, Brushes.Purple, new RectangleModel { X = -3, Y = 2, Width = 5, Height = 7 });
            painter.DrawEllipse(null, Brushes.OrangeRed, new RectangleModel { X = 1, Y = 0, Width = 8, Height = 4 });
            
            var trianglePoints = new[]
            {
                new PointModel { X = 6, Y = 5 },
                new PointModel { X = 11, Y = 9 },
                new PointModel { X = 12, Y = 3 }
            };
            painter.DrawPolygon(null, Brushes.Teal, trianglePoints);
            
            painter.DrawRectangle(new Pen(Color.Black, 5), null, new RectangleModel { X = -4, Y = 8, Width = 3, Height = 2 });
        }
    }
}
