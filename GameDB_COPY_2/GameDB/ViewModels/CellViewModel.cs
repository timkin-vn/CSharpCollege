using System.Windows.Media;

namespace GameDB.ViewModels;

public class CellViewModel : ViewModelBase
{
    public int Value { get; set; }
    public string Text => Value == 0 ? string.Empty : Value.ToString();
    public int Row { get; set; }
    public int Column { get; set; }
    public Brush TileBrush { get; set; } = Brushes.Transparent;
    public Brush TextBrush { get; set; } = Brushes.Black;

    public static CellViewModel Create(int row, int column, int value)
    {
        return new CellViewModel
        {
            Row = row,
            Column = column,
            Value = value,
            TileBrush = GetTileBrush(value),
            TextBrush = GetTextBrush(value),
        };
    }

    private static Brush GetTileBrush(int value)
    {
        string color = value switch
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

        return (Brush)new BrushConverter().ConvertFromString(color)!;
    }

    private static Brush GetTextBrush(int value)
    {
        string color = value <= 4 && value != 0 ? "#776E65" : "#F9F6F2";
        return (Brush)new BrushConverter().ConvertFromString(color)!;
    }
}