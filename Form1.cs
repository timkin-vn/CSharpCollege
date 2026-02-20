using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private AlarmStatic alarmStatic = new AlarmStatic();

        public AlarmStatic AlarmStatic { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var from = new Settings();
            from.AlarmStatic = alarmStatic;
            if (from.ShowDialog() == DialogResult.OK) {
                UpdateContril();
            }
        }
        private void UpdateContril()
        {
            if (alarmStatic.IsAlarmActive)
            {
                Text = $"Будильник (сработает в {alarmStatic.AlarmTime.ToShortTimeString()})";
            }
            else
            {
                Text = $"Будильник";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Text = "Будильник";

            if (alarmStatic.IsAlarmActive || alarmStatic.IsAwekeActiva){
                alarmStatic.IsAwekeActiva = false;
                alarmStatic.IsAlarmActive = false;
                MessageBox.Show("Будильник отключён","Отключение",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Будильник не был включён","Информация",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void Display_Click(object sender, EventArgs e)
        {

        }

        private void Option_Click(object sender, EventArgs e)
        {
            var form = new Opisalsa();
            form.ShowDialog();
        }

        private void ClockTime_Tick(object sender, EventArgs e)
        {
            Display.Text = DateTime.Now.ToLongTimeString();

            if (alarmStatic.IsAlarmActive &&
                !alarmStatic.IsAwekeActiva &&
                DateTime.Now >= alarmStatic.AlarmTime)
            {
                alarmStatic.IsAwekeActiva = true;
                alarmStatic.IsAlarmActive = false;
                var awekeForm = new Vsavai();
                awekeForm.AlarmStatic = alarmStatic;
                awekeForm.Show();
            }
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
