using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AlarmClock.Forms
{
    public partial class SoundSettingsForm : Form
    {
        private WMPLib.WindowsMediaPlayer _previewPlayer;

        private readonly List<string> _defaultSounds = new List<string>
        {
            "C:\\Users\\Денис\\Desktop\\C#\\AlarmClock\\Sounds\\Aegean-Sea.wav"
        };

        public ClockSettings Settings { get; set; }

        public SoundSettingsForm()
        {
            InitializeComponent();
        }

        private void SoundSettingsForm_Load(object sender, EventArgs e)
        {
            comboBoxMusic.Items.Clear();

            foreach (var sound in _defaultSounds)
            {
                comboBoxMusic.Items.Add(sound);
            }

            foreach (var customSound in Settings.CustomSounds)
            {
                comboBoxMusic.Items.Add(customSound);
            }

            comboBoxMusic.SelectedItem = Settings.SelectedSoundPath;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Аудио файлы (*.wav;*.mp3)|*.wav;*.mp3";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = openFileDialog.FileName;
                    if (!Settings.CustomSounds.Contains(selectedPath))
                    {
                        Settings.CustomSounds.Add(selectedPath);
                        comboBoxMusic.Items.Add(selectedPath);
                    }
                    comboBoxMusic.SelectedItem = selectedPath;
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Settings.SelectedSoundPath = comboBoxMusic.SelectedItem?.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_previewPlayer != null)
                {
                    _previewPlayer.controls.stop();
                    _previewPlayer = null;
                }

                if (comboBoxMusic.SelectedItem == null)
                {
                    MessageBox.Show("Выберите звук для прослушивания", "Предупреждение",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedSound = comboBoxMusic.SelectedItem.ToString();


                if (!System.IO.File.Exists(selectedSound))
                {
                    MessageBox.Show("Файл не найден", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _previewPlayer = new WMPLib.WindowsMediaPlayer();
                _previewPlayer.URL = selectedSound;
                _previewPlayer.settings.volume = 100;
                _previewPlayer.controls.play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка воспроизведения: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_previewPlayer != null)
            {
                _previewPlayer.controls.stop();
                _previewPlayer = null;
            }
            base.OnFormClosing(e);
        }
    }
    
}
