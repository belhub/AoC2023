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

class Program5b
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

    static void Filter(Dictionary<int, List<List<long>>> numsDict) //filter a seeds
    {
        for (int helper = 0; helper <= seeds.Length / 2; helper += 2)
        {
            for (long seed = seeds[helper]; seed < seeds[helper] + seeds[helper + 1]; seed++)
            {
                long number = seed;
                foreach (var mapNums in numsDict)
                {
                    number = Sieve(number, mapNums.Value);

                }
                sum = Math.Min(number, sum);
            }
            Console.WriteLine("Min: " + sum);
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

    static Dictionary<int, List<List<long>>> SieveInit() //save maps into Dictionary
    {
        Dictionary<int, List<List<long>>> dictNums = new();
        List<List<long>> mapNums = new();
        int id = 0;

        foreach (var line in inputStr)
        {
            if (line.Contains("map:"))
            {
                List<List<long>> mapAdd = new List<List<long>>(mapNums);
                dictNums.Add(id, mapAdd);
                id++;
                mapNums.Clear();
                continue;
            }

            List<long> mapRow = line.Split(' ').Select(l => long.Parse(l.Trim())).ToList();
            mapNums.Add(mapRow);

            if (line == inputStr.Last())
            {
                dictNums.Add(id, mapNums);
                break;
            }
        }
        dictNums.Remove(0);
        return dictNums;
    }
    static void Main5b()
    {
        ReadFile("input.txt");
        Dictionary<int, List<List<long>>> dictNums = SieveInit();
        Filter(dictNums);

        Console.WriteLine("OutputSum: " + sum);
    }
}