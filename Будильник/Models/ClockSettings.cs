using System;
using System.IO;
using System.Xml.Serialization;

namespace AlarmClock.Models
{
    public class ClockSettings
    {
        [XmlIgnore]
        public TimeSpan AlarmTime { get; set; } = new TimeSpan(7, 0, 0);

        public string AlarmTimeString
        {
            get => AlarmTime.ToString();
            set => AlarmTime = TimeSpan.Parse(value);
        }

        public string AlarmMessage { get; set; } = "Просыпайся!";
        public bool IsAlarmActive { get; set; } = true;
        public bool IsSoundActive { get; set; } = true;
        public bool IsAwakeActivated { get; set; } = false;
        public bool DarkMode { get; set; } = false;

        public void SaveToFile(string filePath)
        {
            var serializer = new XmlSerializer(typeof(ClockSettings));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static ClockSettings LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new ClockSettings();

            try
            {
                var serializer = new XmlSerializer(typeof(ClockSettings));
                using (var reader = new StreamReader(filePath))
                {
                    return (ClockSettings)serializer.Deserialize(reader);
                }
            }
            catch
            {
                return new ClockSettings();
            }
        }
    }
}