using AlarmClock.Models;
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
using NAudio.Wave;
namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
       
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        public SoundPlayer player;


        
        string extension = "";
        public int Music = 0;
        public string NameFile = "";
        public AlarmSettings Settings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Неверно задано время срабатывания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.AlarmTime = alarmTime;
            Settings.AlarmMessage = AlarmMessageTextBox.Text;
            Settings.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;
            Settings.Music = 1;

            DialogResult = DialogResult.OK;


        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AlarmTimeTextBox.Text = Settings.AlarmTime.ToString("H:mm");
            AlarmMessageTextBox.Text = Settings.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = Settings.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;
        }

        private void AlarmMessageTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show("Выберите песню mp3");

           
            if (Music == 0)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    NameFile = openFileDialog1.FileName;
                    extension = Path.GetExtension(NameFile);
                    if(extension != ".mp3")
                    {
                        MessageBox.Show("Файл должен иметь расширение mp3");
                        return;
                    }
                    button1.Text = NameFile.Substring(0, 14) + "...";

                    Settings.MusicUrl = NameFile;
                }
                
            }
            else
            {
                
                button1.Text = NameFile.Substring(0, 14) + "...";
                Music = 0;
            }
        }
        
        public void PlayMusic(string NameFile)
        {
            outputDevice = new WaveOutEvent();
            audioFile = new AudioFileReader(NameFile);
            outputDevice.Init(audioFile);
            outputDevice.Play();
            outputDevice.Stop();
        }
        public void StopMusic()
        {
            
        }
    }
}
