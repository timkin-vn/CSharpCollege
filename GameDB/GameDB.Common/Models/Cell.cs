namespace GameDB.Common.Models;

public class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public int Value { get; set; }

    public bool IsEmpty => Value == 0;
}