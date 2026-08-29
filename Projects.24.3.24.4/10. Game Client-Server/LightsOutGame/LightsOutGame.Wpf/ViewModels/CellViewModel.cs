using LightsOutGame.Common.Definitions;

namespace LightsOutGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Value { get; set; }

        public bool IsOn => Value == Constants.LightOn;

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
