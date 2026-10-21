using FifteenGame.Common.BusinessModels;
using System;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Value { get; set; }

        public string CardColor
        {
            get
            {
                if (!IsOpened && !IsFound)
                    return "LightGray";

                if (Theme == "Animals" || Theme == "Fruits")
                    return "LightGray";

                switch (Value)
                {
                    case 1: return "Red";
                    case 2: return "Blue";
                    case 3: return "Green";
                    case 4: return "Yellow";
                    case 5: return "Purple";
                    case 6: return "Orange";
                    case 7: return "Pink";
                    case 8: return "Cyan";
                    default: return "Gray";
                }
            }
        }

        public string CardContent
        {
            get
            {
                if (!IsOpened && !IsFound)
                    return "";

                if (Theme == "Animals")
                {
                    switch (Value)
                    {
                        case 1: return "🐱";
                        case 2: return "🐶";
                        case 3: return "🐰";
                        case 4: return "🦊";
                        case 5: return "🐼";
                        case 6: return "🐸";
                        case 7: return "🐵";
                        case 8: return "🐨";
                        default: return "";
                    }
                }

                if (Theme == "Fruits")
                {
                    switch (Value)
                    {
                        case 1: return "🍎";
                        case 2: return "🍌";
                        case 3: return "🍊";
                        case 4: return "🍋";
                        case 5: return "🍇";
                        case 6: return "🍉";
                        case 7: return "🍓";
                        case 8: return "🥝";
                        default: return "";
                    }
                }

                return "";
            }
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public string Theme { get; set; }

        public MoveDirection Direction { get; set; }

        public bool IsOpened { get; set; }

        public bool IsFound { get; set; }
    }
}
