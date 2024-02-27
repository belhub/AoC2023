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

class Program5a
{
    static long sum = 9999999999;
    static List<string> inputStr = new();
    static long[] seeds;
    static void ReadFile(string FileName)
    {
        string line;
        bool firstLine = true;
        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                if (firstLine)
                {
                    seeds = line.Split(' ').Skip(1).Select(l => long.Parse(l.ToString().Trim())).ToArray();
                    firstLine = false;
                    continue;
                }
                if (line.Length < 2) { continue; }
                inputStr.Add(line);
            }
        }
    }

    static void Filter()
    {
        List<List<long>> mapNums = new();
        foreach (var seed in seeds)
        {
            long number = seed;
            foreach (var line in inputStr)
            {
                if (line.Contains("map:"))
                {
                    number = Sieve(number, mapNums);
                    mapNums.Clear();
                    continue;
                }
                List<long> mpaRow = line.Split(' ').Select(l => long.Parse(l.ToString().Trim())).ToList();
                mapNums.Add(mpaRow);
                if (line == inputStr.Last())
                {
                    number = Sieve(number, mapNums);
                    mapNums.Clear();
                    break;
                }
            }
            sum = Math.Min(number, sum);
        }
    }

    static long Sieve(long seed, List<List<long>> map)//logika filtrowania 
    {
        foreach (var item in map)
        {
            long numberToCheck = item[0];
            long startNumber = item[1];
            long almanaCounter = item[2];
            if (seed >= startNumber && seed < almanaCounter + startNumber)
            {
                long tmp = seed - startNumber;
                return numberToCheck + tmp;
            }
        }

        return seed;
    }

    static void Main5a()
    {
        ReadFile("input.txt");
        Filter();
        Console.WriteLine("OutputSum: " + sum);
    }
}