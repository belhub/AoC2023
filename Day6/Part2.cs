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
    static long outputSum = 1;
    static long finalTime;
    static long finalDistance;
    static List<int> distance = new();
    static List<int> time = new();
    static List<int> finalValues = new();

    static void ReadFile(string FileName)
    {
        string line;

        using (StreamReader file = new StreamReader(FileName))
        {

            while ((line = file.ReadLine()) != null)
            {
                string[] strArray = line.Split(" ", ':');
                for (int i = 1; i < strArray.Length; i++)
                {
                    if (strArray[i].Length > 0 && strArray[0] == "Time:")
                    {
                        time.Add(int.Parse(strArray[i]));
                    }
                    if (strArray[i].Length > 0 && strArray[0] == "Distance:")
                    {
                        distance.Add(int.Parse(strArray[i]));
                    }
                }
            }
        }
        string finalT = string.Join("", time);
        string finalD = string.Join("", distance);
        finalTime = long.Parse(finalT);
        finalDistance = long.Parse(finalD);
    }


    static void CalculateVariations()
    {
        long tmp = 0;
        for (long index = 1; index < finalTime; index++)
        {
            if ((finalTime - index) * index > finalDistance)
            {
                tmp++;
            }
        }
        Console.WriteLine("Output: " + tmp);
        outputSum *= tmp;
    }

    static void Main()
    {
        ReadFile("input.txt");
        CalculateVariations();
        Console.WriteLine("Time: " + finalTime);

        Console.WriteLine();
        Console.WriteLine("Distance: " + finalDistance);

        Console.WriteLine("OutputSum: " + outputSum);
    }
}