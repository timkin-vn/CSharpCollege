namespace GameDB.Common.Helpers;

public static class ArrayHelper
{
    public static int[][] DeepCopy(int[][] source)
    {
        if (source == null) return Array.Empty<int[]>();
        
        var result = new int[source.Length][];
        for (int i = 0; i < source.Length; i++)
        {
            result[i] = new int[source[i].Length];
            Array.Copy(source[i], result[i], source[i].Length);
        }
        return result;
    }

    public static bool AreEqual(int[][] first, int[][] second)
    {
        if (first == null || second == null) return false;
        if (first.Length != second.Length) return false;

        for (int i = 0; i < first.Length; i++)
        {
            if (first[i].Length != second[i].Length) return false;
            for (int j = 0; j < first[i].Length; j++)
            {
                if (first[i][j] != second[i][j]) return false;
            }
        }
        return true;
    }

    public static string ToMatrixString(int[][] matrix)
    {
        if (matrix == null) return "null";
        return string.Join("; ", matrix.Select(row => string.Join(", ", row)));
    }
}