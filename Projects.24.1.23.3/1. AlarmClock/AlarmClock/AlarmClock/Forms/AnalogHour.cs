using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace AlarmClock.Forms
{
    public partial class AnalogHour : Form
    {




        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();


        int cx = 175, cy = 175;

        int secHAND = 100, minHAND = 90, hrHAND = 75;

        Timer t = new Timer();

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public AnalogHour()
        {


            InitializeComponent();
            this.BackColor = Color.AliceBlue;
            this.TransparencyKey = this.BackColor;


        }

        private void AnalogHour_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            const uint WM_SYSCOMMAND = 0x0112;
            const uint DOMOVE = 0xF012;

            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);

        }

        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6;

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }


        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        private void t_Tick(object sender, EventArgs e)
        {
            int s = DateTime.Now.Second;
            int m = DateTime.Now.Minute;
            int h = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            Graphics g = pictureBox1.CreateGraphics();
            handCoord = msCoord(s, secHAND + 4);
            g.DrawLine(new Pen(Color.White, 45f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = msCoord(m, minHAND + 4);
            g.DrawLine(new Pen(Color.White, 40f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = hrCoord(h % 12, m, hrHAND + 4);
            g.DrawLine(new Pen(Color.White, 20f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = hrCoord(h % 12, m, hrHAND);
            g.DrawLine(new Pen(Color.Gray, 4f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = msCoord(m, minHAND);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = msCoord(s, secHAND);
            g.DrawLine(new Pen(Color.DarkOrange, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

        }

    }
}
