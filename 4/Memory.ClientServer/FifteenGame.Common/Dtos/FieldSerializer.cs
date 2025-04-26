using System;
using System.Text;

namespace FifteenGame.Common.Dtos
{
    public static class FieldSerializer
    {
        public static string Serialize(int[,] field)
        {
            var sb = new StringBuilder();
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(field[i, j]);
                    if (j < cols - 1)
                        sb.Append(',');
                }
                if (i < rows - 1)
                    sb.Append(';');
            }
            return sb.ToString();
        }
        public static int[,] Deserialize(string str)
        {
            var rows = str.Split(';');
            int rowCount = rows.Length;
            int colCount = rows[0].Split(',').Length;
            int[,] field = new int[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                var cols = rows[i].Split(',');
                for (int j = 0; j < colCount; j++)
                {
                    field[i, j] = int.Parse(cols[j]);
                }
            }
            return field;
        }
    }
}
