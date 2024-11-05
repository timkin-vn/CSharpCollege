using System;
using System.Drawing;
using System.Windows.Forms;

namespace graphic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint_1;
            this.Resize += Form1_Resize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       

        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;

            //цвета
            var house1RoofColor = Color.Yellow;
            var house1WallColor = Color.SaddleBrown;
            var house1DoorColor = Color.Red;
            var house1WindowColor = Color.SkyBlue;

            var house2RoofColor = Color.Green;
            var house2WallColor = Color.RoyalBlue;
            var house2DoorColor = Color.LightSlateGray;
            var house2WindowColor = Color.SkyBlue;

            var groundColor = Color.SaddleBrown;
            var sunColor = Color.Gold;
            var treeTrunkColor = Color.Sienna;
            var treeLeafColor = Color.Green;
            var cloudColor = Color.LightGray;
            var fenceColor = Color.BurlyWood;

            //размеры и положения элементов
            float houseWidth = 15;
            float houseHeight = 25;
            float houseRoofHeight = 10;
            float doorWidth = 3;
            float doorHeight = 7;
            float windowWidth = 5;
            float windowHeight = 4;
            float groundHeight = 5;

            //дома
            float house1X = 10;
            float house1Y = 10;
            float house2X = 50;
            float house2Y = 10;

            //масштабирование
            float scaleX = ClientSize.Width / 100f;
            float scaleY = ClientSize.Height / 100f;

            //дом 1
            //крыша
            using (var roofBrush = new SolidBrush(house1RoofColor))
            {
                graphics.FillPolygon(roofBrush, new PointF[]
                {
                    new PointF((house1X + houseWidth / 2) * scaleX, house1Y * scaleY),
                    new PointF(house1X * scaleX, (house1Y + houseRoofHeight) * scaleY),
                    new PointF((house1X + houseWidth) * scaleX, (house1Y + houseRoofHeight) * scaleY)
                });
            }
            //стены
            using (var wallBrush = new SolidBrush(house1WallColor))
            {
                graphics.FillRectangle(wallBrush, new RectangleF(
                    house1X * scaleX,
                    (house1Y + houseRoofHeight) * scaleY,
                    houseWidth * scaleX,
                    houseHeight * scaleY));
            }
            //дверь
            using (var doorBrush = new SolidBrush(house1DoorColor))
            {
                graphics.FillRectangle(doorBrush, new RectangleF(
                    (house1X + houseWidth / 2 - doorWidth / 2) * scaleX,
                    (house1Y + houseRoofHeight + houseHeight - doorHeight) * scaleY,
                    doorWidth * scaleX,
                    doorHeight * scaleY));
            }
            //окно
            using (var windowBrush = new SolidBrush(house1WindowColor))
            {
                graphics.FillEllipse(windowBrush, new RectangleF(
                    (house1X + houseWidth / 2 - windowWidth / 2) * scaleX,
                    (house1Y + houseRoofHeight + houseHeight / 4) * scaleY,
                    windowWidth * scaleX,
                    windowHeight * scaleY));
            }

            //дом 2
            //крыша
            using (var roofBrush = new SolidBrush(house2RoofColor))
            {
                graphics.FillPolygon(roofBrush, new PointF[]
                {
                    new PointF((house2X + houseWidth / 2) * scaleX, house2Y * scaleY),
                    new PointF(house2X * scaleX, (house2Y + houseRoofHeight) * scaleY),
                    new PointF((house2X + houseWidth) * scaleX, (house2Y + houseRoofHeight) * scaleY)
                });
            }
            //стены
            using (var wallBrush = new SolidBrush(house2WallColor))
            {
                graphics.FillRectangle(wallBrush, new RectangleF(
                    house2X * scaleX,
                    (house2Y + houseRoofHeight) * scaleY,
                    houseWidth * scaleX,
                    houseHeight * scaleY));
            }

            //дверь
            using (var doorBrush = new SolidBrush(house2DoorColor))
            {
                graphics.FillRectangle(doorBrush, new RectangleF(
                    (house2X + houseWidth / 2 - doorWidth / 2) * scaleX,
                    (house2Y + houseRoofHeight + houseHeight - doorHeight) * scaleY,
                    doorWidth * scaleX,
                    doorHeight * scaleY));
            }

            //окно
            using (var windowBrush = new SolidBrush(house2WindowColor))
            {
                graphics.FillEllipse(windowBrush, new RectangleF(
                    (house2X + houseWidth / 2 - windowWidth / 2) * scaleX,
                    (house2Y + houseRoofHeight + houseHeight / 4) * scaleY,
                    windowWidth * scaleX,
                    windowHeight * scaleY));
            }

            //земля
            using (var groundBrush = new SolidBrush(groundColor))
            {
                graphics.FillRectangle(groundBrush, new RectangleF(
                    0,
                    (house1Y + houseHeight + houseRoofHeight) * scaleY,
                    ClientSize.Width,
                    groundHeight * scaleY));
            }
            //солнце
            float sunRadius = 8 * scaleX;
            using (var sunBrush = new SolidBrush(sunColor))
            {
                graphics.FillEllipse(sunBrush, ClientSize.Width - sunRadius * 2 - 10, 10, sunRadius * 2, sunRadius * 2);
            }

            //облако
            float cloudWidth = 20 * scaleX;
            float cloudHeight = 10 * scaleY;

            using (var cloudBrush = new SolidBrush(cloudColor))
            {
                graphics.FillEllipse(cloudBrush, 20 * scaleX, 5 * scaleY, cloudWidth, cloudHeight);
                graphics.FillEllipse(cloudBrush, 30 * scaleX, 5 * scaleY, cloudWidth, cloudHeight);
                graphics.FillEllipse(cloudBrush, 25 * scaleX, 2 * scaleY, cloudWidth, cloudHeight);
            }
        }
    }
}
