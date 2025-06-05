using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmSettings _settings = new AlarmSettings();
                                              static  bool _isplayer    = false;
        private AwakeForm _awakeForm;
        private const string ImageFolderName = "Images";

        private List<string> _fileNames = new List<string>();

        private int _imageIndex = 0;
        public ClockForm()
        {
            InitializeComponent();
            
           
        }

        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            // Обновление индикатора времени
           

            // Проверка срабатывания
            if (_settings.IsAlarmActive && DateTime.Now.TimeOfDay >= _settings.AlarmTime.TimeOfDay)
            {
                if (_awakeForm == null || _awakeForm.IsDisposed)
                {
                    _awakeForm = new AwakeForm();
                }

                _awakeForm.Settings = _settings;
                _awakeForm.Show();
                string soundFilePath = @"C:Musing\bel.WAV";
                System.Media.SoundPlayer snd = new SoundPlayer(soundFilePath);
               
                if (_settings.IsSoundActive)
                {
                    if (_isplayer)
                    {
                        snd.Stop();
                        _isplayer = false;
                    }
                    else
                    {
                        snd.Play();
                        _isplayer = true;
                    }
                }
                else
                {
                    if (_isplayer)
                    {
                        snd.Play();
                        _isplayer = true;
                    }
                    else
                    {
                        snd.Stop();
                        _isplayer = false;
                    }
                };
                
              

            }
            Invalidate();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.Settings = _settings;
            form.ShowDialog();
        }
        private int[] hrCoord(int hval, int mval, int hlen,int cx,int cy)
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
        private int[] msCoord(int val, int hlen, int cx, int cy)
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



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
           const  int secHAND = 80, minHAND = 60, hrHAND =55;

            // Рисуем аналоговые часы
            Pen seconPen = new Pen(Color.Black,1);
            Pen hourPen = new Pen(Color.Red, 2);
            Pen minutesPen  =  new Pen (Color.Green, 2);
            ;
           
            float centerX = TimePikter1.Width+60;
            float centerY = TimePikter1.Height /2+10;

            

        

           
            int[] hourCoords = hrCoord(DateTime.Now.Hour, DateTime.Now.Minute, hrHAND + 4, ((int)centerX),(int)(centerY)) ;
            e.Graphics.DrawLine(hourPen, centerX, centerY, hourCoords[0], hourCoords[1]);
           

            int[] minuteCoords = msCoord(DateTime.Now.Minute, minHAND+4, ((int)centerX), (int)(centerY));
            e.Graphics.DrawLine(minutesPen, centerX, centerY, minuteCoords[0], minuteCoords[1]);

            int[] secondCoords = msCoord(DateTime.Now.Second, secHAND + 4, ((int)centerX), (int)(centerY));
            e.Graphics.DrawLine(seconPen, centerX, centerY, secondCoords[0], secondCoords[1]);
            e.Graphics.ResetTransform();


           
        }

        private void TimePikter1_Click(object sender, EventArgs e)
        {

        }
    }
}
    




