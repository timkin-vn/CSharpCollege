namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Num { get; set; }
        public string Text => Num == 0 ? "" : Num.ToString();
        public string BackgroundColor
        {
            get
            {
                if (Num == 2) return "#EEE4DA";
                if (Num == 4) return "#EDE0C8";
                if (Num == 8) return "#F2B179";
                if (Num == 16) return "#F59563";
                if (Num == 32) return "#F67C5F";
                if (Num == 64) return "#F65E3B";
                if (Num == 128) return "#EDCF72";
                if (Num == 256) return "#EDCC61";
                if (Num == 512) return "#EDC850";
                if (Num == 1024) return "#EDC53F";
                if (Num == 2048) return "#EDC22E";
                return "#CDC1B4";
            }
        }
    }
}