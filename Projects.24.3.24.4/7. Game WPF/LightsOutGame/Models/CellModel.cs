namespace LightsOutGame.Models
{
    public class CellModel
    {
        public bool IsOn { get; set; }

        public CellModel(bool isOn = false)
        {
            IsOn = isOn;
        }

        public void Toggle()
        {
            IsOn = !IsOn;
        }
    }
}
