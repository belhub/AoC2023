using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.Linq;
class Program
{
    static int outputSum = 0;
    static List<int> array1 = new List<int>();
    static List<int> array2 = new List<int>();

    static void ReadFile(string FileName)
    {
        string line;

        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                string[] strArray = line.Split(" ", ':');
                ExtractNumbers(strArray);
                var common = array1.Intersect(array2);
                foreach (var number in common)
                {
                    Console.Write(number + " | ");
                }
                int length = common.Count();
                outputSum += (int)Math.Pow(2, length - 1);
                Console.Write("length: " + (int)Math.Pow(2, length - 1));
                Console.WriteLine();
                array1.Clear();
                array2.Clear();
            }
        }
    }

    static void ExtractNumbers(string[] arr)
    {
        bool second = false;
        foreach (var item in arr)
        {
            if (item == "|")
            {
                second = true;
            }
            if (int.TryParse(item.ToString(), out int number) && second == false)
            {
                array1.Add(int.Parse(item));
                //Console.Write(item + ".");

            }
            if (int.TryParse(item.ToString(), out int num) && second == true)
            {
                array2.Add(int.Parse(item));
                //Console.Write(item + ":");

            }
        }
    }
    static void Main()
    {
        ReadFile("input.txt");

        // foreach (var item in array1)
        // {
        //     Console.Write(item + ",");
        // }
        // foreach (var item in array2)
        // {
        //     Console.Write(item + ":");
        // }

        Console.WriteLine("OutputSum: " + outputSum);
    }
}