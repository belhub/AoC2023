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
    static List<int> array1 = new();
    static List<int> array2 = new();
    static List<int> array3 = new();
    static List<int> lengthArray = new();



    static void ReadFile(string FileName)
    {
        string line;

        using (StreamReader file = new StreamReader(FileName))
        {
            int tmp = 0;

            while ((line = file.ReadLine()) != null)
            {
                string[] strArray = line.Split(" ", ':');
                ExtractNumbers(strArray);
                var common = array1.Intersect(array2);
                int length = common.Count();
                lengthArray.Add(length);
                array1.Clear();
                array2.Clear();
                tmp++;
            }
            CheckArray();
            for (int i = 0; i < array3.Count; i++)
            {
                outputSum += array3[i];
                Console.WriteLine(array3[i] + " | " + lengthArray[i]);
            }
        }
    }

    static void CheckArray()
    {
        for (int i = 0; i < lengthArray.Count; i++)
        {
            array3.Add(1);//uzupełnienie jedynkami inicjalizacja
        }

        for (int i = 0; i < lengthArray.Count; i++)
        {
            for (int index = 0; index < lengthArray[i]; index++)
            {

                array3[i + index + 1] += array3[i];

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
        Console.WriteLine("OutputSum: " + outputSum);
    }
}