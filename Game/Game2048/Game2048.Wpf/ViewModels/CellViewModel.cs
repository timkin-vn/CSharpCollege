using Game2048.Business.Models;

namespace Game2048.Wpf.ViewModels
{
    public class CellViewModel
    {
        public int Value { get; set; }

        public string Text => Value == 0 ? "" : Value.ToString();

        public string Color => GetTileColor(Value);

        private string GetTileColor(int value)
        {
            return value switch
            {
                0 => "#CDC1B4",
                2 => "#EEE4DA",
                4 => "#EDE0C8",
                8 => "#F2B179",
                16 => "#F59563",
                32 => "#F67C5F",
                64 => "#F65E3B",
                128 => "#EDCF72",
                256 => "#EDCC61",
                512 => "#EDC850",
                1024 => "#EDC53F",
                2048 => "#EDC22E",
                _ => "#3C3A32"
            };
        }
    }
}
