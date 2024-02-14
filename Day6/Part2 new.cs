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
    static int outputSum = 1;

    static long finalTime;
    static long finalDistance;

    static void ReadFile(string FileName)
    {
        string line;
        List<int> distance = new();
        List<int> time = new();

        using (StreamReader file = new StreamReader(FileName))
        {

            while ((line = file.ReadLine()) != null)
            {
                switch (line)
                {
                    case string l when l.Contains("Time"):
                        time = l.Split(' ', ':').Skip(1).Where(d => d.Length > 0).Select(t => int.Parse(t.Trim())).ToList();
                        break;
                    case string l when l.Contains("Distance"):
                        distance = l.Split(' ', ':').Skip(1).Where(d => d.Length > 0).Select(d => int.Parse(d.Trim())).ToList();
                        break;
                }
            }
            string finalT = string.Join("", time);
            string finalD = string.Join("", distance);
            finalTime = long.Parse(finalT);
            finalDistance = long.Parse(finalD);
        }
    }

    static void CalculateVariations()
    {
        int tmp = Enumerable.Range(1, (int)(finalTime - 1))
                    .Count(index => (finalTime - index) * index > finalDistance);
        Console.WriteLine("Sum: " + tmp);
    }

    static void Main()
    {
        ReadFile("input.txt");
        CalculateVariations();
    }
}