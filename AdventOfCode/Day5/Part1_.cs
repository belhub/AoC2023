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

class Program5a_
{
    static long sum = 9999999999;
    static List<string> inputStr = new();
    static long[] seeds;

    static void ReadFile(string fileName)
    {
        bool firstLine = true;

        foreach (var line in File.ReadAllLines(fileName))
        {
            if (firstLine)
            {
                seeds = line.Split(' ').Skip(1).Select(l => long.Parse(l.ToString().Trim())).ToArray();
                firstLine = false;
                continue;
            }

            if (line.Length < 2)
            {
                continue;
            }
            inputStr.Add(line);
        }

        // cr_mn: kod tej petli mozna uproscic
        // - rozdzielajac ligike parsowania pierwszego piersza od pozostalych
        // - filtrujac puste wieksze
        // - wtedy petla sprowadza sie do prostego zapytania

        {
            var lines = File.ReadAllLines(fileName);
            seeds = lines[0].Split(' ').Skip(1).Select(l => long.Parse(l.ToString().Trim())).ToArray();
            inputStr = lines.Skip(1).Where(l => l.Length >= 2).ToList();
        }
    }

    static void Filter()
    {
        // cr_mn: moze juz tylko mi ale jak ciezko kod imperatywny/mutowalny zrozumiec :/
        // - jest zmienna "seed" ale po co powstaje zmienna "number" ??
        // - jest zmienna "mapNums" z kolekcja dnaych ... raz jest uzupelniona a raz czyszczona ??
        // -- moze zamiast Add+Clear wystarczy przy kazdej itarcji tworzyc nowa kolekcje
        // - kurcze, raz jest break a raz continue, czym one sie wlasciwie roznily 

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

                // cr_mn: ten warunek wykonuje sie dla kazdej linijki a ma sens tylko dla ostatniej, 
                // mozna to wyciagnac z petli

                if (line == inputStr.Last())
                {
                    number = Sieve(number, mapNums);
                    mapNums.Clear();
                    break;
                }
            }

            number = Sieve(number, mapNums);
            mapNums.Clear();

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

    internal static void Main5a()
    {
        ReadFile("Day5/input.txt");
        Filter();
        Console.WriteLine("OutputSum: " + sum);
    }
}