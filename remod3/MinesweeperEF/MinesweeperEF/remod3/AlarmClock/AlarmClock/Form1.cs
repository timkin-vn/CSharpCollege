using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace AlarmClock {
    public partial class frmAlarmMain : Form {

        Graphics clockGraphic;

        private struct Alarm {
            public static List<Alarm> alarms = new List<Alarm>();
            public DateTime time { get; }
            public bool hasSound { get; }
            public static bool isRinging = false;
            private static SoundPlayer soundFile = new SoundPlayer("./files/ringtone.wav");

            public Alarm(DateTime time, bool hasSound) {
                this.time = time;
                this.hasSound = hasSound;
            }

            public static void startRinging(Alarm alarm) {
                isRinging = true;

                if (alarm.hasSound) {
                    soundFile.PlayLooping();
                }

                MessageBox.Show("Ваш будильник на время " + alarm.time.ToString("HH:mm:ss") + " сработал ! 🔔");
            }

            public static void stopRinging() {
                isRinging = false;
                soundFile.Stop();
            }
        }

        public frmAlarmMain() {
            InitializeComponent();
        }

        private void frmAlarmMain_Load(object sender, EventArgs e) {
            tmrAlarm.Start();
            clockGraphic = pnlClock.CreateGraphics();
        }

        private void tmrAlarm_Tick(object sender, EventArgs e) {
            foreach (Alarm alarm in Alarm.alarms) {
                DateTime now = DateTime.Now;
                if (now.ToString("HH:mm:ss") == alarm.time.ToString("HH:mm:ss")) {
                    btnStop.Enabled = true;
                    Alarm.startRinging(alarm);
                }
            }

            drawAnalogClock();
            lblDigitalClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        void drawAnalogClock() {
            Pen p;
            int xs = 27, ys = 5;
            int size = 170;

            clockGraphic.Clear(DefaultBackColor);

            clockGraphic.FillEllipse(Brushes.White, new Rectangle(new Point(xs, ys), new Size(size, size)));

            if (DateTime.Now.Second % 2 == 0 && Alarm.isRinging) {
                clockGraphic.FillEllipse(Brushes.Red, new Rectangle(new Point(xs, ys), new Size(size, size)));
            }
            else if (DateTime.Now.Second % 2 == 1 && Alarm.isRinging) {
                clockGraphic.FillEllipse(Brushes.DarkOrange, new Rectangle(new Point(xs, ys), new Size(size, size)));
            }

            p = new Pen(Color.Black, 5);
            clockGraphic.DrawEllipse(p, new Rectangle(new Point(xs, ys), new Size(size, size)));

            double radians;
            int x, y;

            int lengthHoursHand = (int)(0.3 * size);
            int hours = DateTime.Now.Hour;
            radians = (Math.PI / 2) - (hours * Math.PI / 6);
            x = (int)((size / 2) + lengthHoursHand * Math.Cos(radians));
            y = (int)((size / 2) - lengthHoursHand * Math.Sin(radians));

            p = new Pen(Color.Gray, 6);
            clockGraphic.DrawLine(p, new Point((size / 2) + xs, (size / 2) + ys), new Point(x + xs, y + ys));

            int lengthMinutesHand = (int)(0.4 * size);
            int minutes = DateTime.Now.Minute;
            radians = (Math.PI / 2) - (minutes * Math.PI / 30);
            x = (int)((size / 2) + lengthMinutesHand * Math.Cos(radians));
            y = (int)((size / 2) - lengthMinutesHand * Math.Sin(radians));

            p = new Pen(Color.Gray, 4);
            clockGraphic.DrawLine(p, new Point((size / 2) + xs, (size / 2) + ys), new Point(x + xs, y + ys));

            int lengthSecondsHand = (int)(0.45 * size);
            int seconds = DateTime.Now.Second;
            radians = (Math.PI / 2) - (seconds * Math.PI / 30);
            x = (int)((size / 2) + lengthSecondsHand * Math.Cos(radians));
            y = (int)((size / 2) - lengthSecondsHand * Math.Sin(radians));

            p = new Pen(Color.DarkRed, 2);
            clockGraphic.DrawLine(p, new Point((size / 2) + xs, (size / 2) + ys), new Point(x + xs, y + ys));


            int dotSize = 5;
            clockGraphic.FillEllipse(Brushes.DarkRed, new Rectangle(new Point((size / 2) - dotSize + xs, (size / 2) - dotSize + ys), new Size(2 * dotSize, 2 * dotSize)));


            int notchLength = (int)(0.05 * size);
            int notchRadius = (int)(0.45 * size);
            int x1, y1, x2, y2;
            for (int i = 0; i < 12; i++) {
                double notchAngle = (Math.PI / 180) * (30 * i - 90);

                x1 = (int)((size / 2) + notchRadius * Math.Cos(notchAngle));
                y1 = (int)((size / 2) + notchRadius * Math.Sin(notchAngle));

                x2 = (int)((size / 2) + (notchRadius - notchLength) * Math.Cos(notchAngle));
                y2 = (int)((size / 2) + (notchRadius - notchLength) * Math.Sin(notchAngle));

                clockGraphic.DrawLine(Pens.Black, x1 + xs, y1 + ys, x2 + xs, y2 + ys);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e) {
            DateTime time = dtpAlarm.Value;
            bool sound = cbxSound.Checked;
            Alarm alarm = new Alarm(time, sound);

            Alarm.alarms.Add(alarm);

            string formatedTime = alarm.time.ToString("HH:mm:ss");
            if (alarm.hasSound) { formatedTime += " 🔔"; }
            else { formatedTime += " 🔕"; }
            clbAlarms.Items.Add(formatedTime);

            clbAlarms_ItemsUpdated();
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            for (int i = Alarm.alarms.Count - 1; i >= 0; i--) {
                if (clbAlarms.GetItemChecked(i)) {
                    Alarm.alarms.RemoveAt(i);
                    clbAlarms.Items.RemoveAt(i);
                }
            }

            clbAlarms_ItemsUpdated();
        }

        protected void clbAlarms_ItemsUpdated() {
            cbxSelectAll.Enabled = Alarm.alarms.Count > 0;

            cbxSelectAll.Checked = false;

            btnRemove.Enabled = clbAlarms.CheckedItems.Count > 0;
        }

        private void btnStop_Click(object sender, EventArgs e) {
            Alarm.stopRinging();
            btnStop.Enabled = false;
        }

        private void clbAlarms_ItemCheck(object sender, ItemCheckEventArgs e) {
            int nbItemsSelected = clbAlarms.CheckedItems.Count;
            if (e.NewValue == CheckState.Checked) { nbItemsSelected++; }
            else { nbItemsSelected--; }

            btnRemove.Enabled = nbItemsSelected > 0;

            cbxSelectAll.Checked = nbItemsSelected == clbAlarms.Items.Count;
        }

        private void cbxSelectAll_Click(object sender, EventArgs e) {
            bool isChecked = cbxSelectAll.Checked;
            for (int i = 0; i < clbAlarms.Items.Count; i++) {
                clbAlarms.SetItemChecked(i, isChecked);
            }
        }
    }
}