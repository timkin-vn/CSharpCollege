using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsApp1
{
    public partial class Vsavai : Form
    {
        private const string ImFolderName = "fons";

        private string[] ImFileName;

        private int indexer;

        private SoundPlayer player;

        public AlarmStatic AlarmStatic { get; set; }

        public Vsavai()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AlarmStatic != null)
            {
                AlarmStatic.IsAwekeActiva = false;
                AlarmStatic.IsAlarmActive = false;
            }

            this.Close();
        }

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {

        }

        private void Vsavai_Load(object sender, EventArgs e)
        {
            Text = AlarmStatic.AlarmMessege;
            IN();
            if (AlarmStatic.IsSoundActive)
            {
                player = new SoundPlayer("OOO.wav");
                player.PlayLooping();
            }
        }
        private void IN()
        {
            ImFileName = Directory.EnumerateFiles(ImFolderName).ToArray();
            indexer = 0;
            pictureBox1.Load(ImFileName[indexer]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            indexer++;
            if (indexer >= ImFileName.Length)
            {
                indexer = 0;
            }
            pictureBox1.Load(ImFileName[indexer]);
        }

        private void Vsavai_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
            }

            AlarmStatic.IsAwekeActiva = false;
        }

        private void Min5_Click(object sender, EventArgs e)
        {
            if (AlarmStatic != null)
            {
                AlarmStatic.AlarmTime = DateTime.Now.AddMinutes(5);
                AlarmStatic.IsAwekeActiva = false;
                this.Close();
            }
        }
    }
}
