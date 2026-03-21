using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();

            //int[] array = new int[4 + rnd.Next(5)];
            //Console.WriteLine($"Array length = {array.Length}");

            //for (int i = 0; i < array.Length; i++)
            //{
            //    array[i] = rnd.Next(100);
            //}

            //Console.WriteLine("Array items:");
            ////for (int i = 0; i < array.Length; i++)
            ////{
            ////    Console.Write($"{array[i]} ");
            ////}
            //foreach (var item in array)
            //{
            //    Console.Write($"{item} ");
            //}

            //Console.WriteLine();

            //var sum = 0;
            //foreach (var item in array)
            //{
            //    sum += item;
            //}

            //Console.WriteLine($"Array sum = {sum}");

            //int[][] array2 = new int[3 + rnd.Next(3)][];
            //for (int i = 0; i < array2.Length; i++)
            //{
            //    array2[i] = new int[4 + rnd.Next(5)];
            //}

            //Console.WriteLine($"Array 2 has {array2.Length} rows");
            //Console.WriteLine("Row lengthes:");
            //foreach (var row in array2)
            //{
            //    Console.Write($"{row.Length} ");
            //}

            //Console.WriteLine();

            //foreach (var row in array2)
            //{
            //    for (int i = 0; i < row.Length; i++)
            //    {
            //        row[i] = rnd.Next(100);
            //    }
            //}

            //Console.WriteLine("Array 2 items:");

            //foreach (var row in array2)
            //{
            //    foreach (var item in row)
            //    {
            //        Console.Write($"{item} ");
            //    }

            //    Console.WriteLine();
            //}

            //var sum = 0;
            //foreach (var row in array2)
            //{
            //    foreach (var item in row)
            //    {
            //        sum += item;
            //    }
            //}

            //Console.WriteLine($"Array 2 sum = {sum}");

            //int[,] array3 = new int[3 + rnd.Next(3), 4 + rnd.Next(5)];
            //Console.WriteLine($"Array 3 has {array3.GetLength(0)} rows, {array3.GetLength(1)} columns, total {array3.Length} items");

            //for (int i = 0; i < array3.GetLength(0); i++)
            //{
            //    for (int j = 0; j < array3.GetLength(1); j++)
            //    {
            //        array3[i, j] = rnd.Next(100);
            //    }
            //}

            //Console.WriteLine("Array 3 items:");
            //for (int i = 0; i < array3.GetLength(0); i++)
            //{
            //    for (int j = 0; j < array3.GetLength(1); j++)
            //    {
            //        Console.Write($"{array3[i, j]} ");
            //    }

            //    Console.WriteLine();
            //}

            //var sum = 0;
            //foreach (var item in array3)
            //{
            //    sum += item;
            //}

            //Console.WriteLine($"Array 3 sum = {sum}");

            //int[][,] array4 = new int[2 + rnd.Next(3)][,];
            //for (int i = 0; i < array4.Length; i++)
            //{
            //    array4[i] = new int[3 + rnd.Next(3), 4 + rnd.Next(3)];
            //}

            //Console.WriteLine($"Array 4 has {array4.Length} layers");
            //Console.WriteLine("Layer sizes:");
            //foreach (var layer in array4)
            //{
            //    Console.WriteLine($"{layer.GetLength(0)} rows * {layer.GetLength(1)} columns");
            //}

            //foreach (var layer in array4)
            //{
            //    for (int i = 0; i < layer.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < layer.GetLength(1); j++)
            //        {
            //            layer[i, j] = rnd.Next(100);
            //        }
            //    }
            //}

            //for (int k = 0; k < array4.Length; k++)
            //{
            //    Console.WriteLine($"Layer {k}:");
            //    for (int i = 0; i < array4[k].GetLength(0); i++)
            //    {
            //        for (int j = 0; j < array4[k].GetLength(1); j++)
            //        {
            //            Console.Write($"{array4[k][i, j]} ");
            //        }

            //        Console.WriteLine();
            //    }
            //}

            //var list = new List<int>();
            //var count = 10 + rnd.Next(30);
            //for (int i = 0; i < count; i++)
            //{
            //    var value = rnd.Next(100);
            //    list.Add(value);
            //    Console.WriteLine($"Value {value} added to list, list count = {list.Count}, capacity = {list.Capacity}");
            //}

            //Console.WriteLine($"List has {list.Count} items");
            //foreach (var item in list)
            //{
            //    Console.Write($"{item} ");
            //}

            //Console.WriteLine();

            //for (int i = 0; i < list.Count; i++)
            //{
            //    list[i] = rnd.Next(100);
            //}

            //foreach (var item in list)
            //{
            //    Console.Write($"{item} ");
            //}

            //Console.WriteLine();

            var letters = new[] { "A", "B", "C", "D" };
            var dictionary = new Dictionary<string, int>();
            var count = 20 + rnd.Next(10);
            Console.WriteLine($"Adding {count} values to dictionary");

            for (int i = 0; i < count; i++)
            {
                var key = string.Empty;
                for (int j = 0; j < 3; j++)
                {
                    key += letters[rnd.Next(letters.Length)];
                }

                dictionary[key] = rnd.Next(100);
            }

            Console.WriteLine($"Dictionary has {dictionary.Count} values");
            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"Dictionary[{kvp.Key}] = {kvp.Value}");
            }
        }
    }
}
