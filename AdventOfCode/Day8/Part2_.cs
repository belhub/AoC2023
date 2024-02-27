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
using System.Security.Cryptography;
using System.ComponentModel;
using System.Globalization;

class Program8b_
{
    static long outputSum = 1; //long
    static Dictionary<string, string[]> puzzle = new();
    static char[] way = null;

    static void ReadFile(string FileName)
    {
        string line;
        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                if (way == null) { way = line.ToCharArray(); }
                else { if (line.Length > 0) { AddToDictionary(line); } }
            }
        }
    }

    static void AddToDictionary(string input)
    {
        string[] toDictionary = input.Replace("(", "").Replace(")", "").Replace(" ", "").Split('=', ',');
        puzzle.Add(toDictionary[0].Trim(), new string[] { toDictionary[1], toDictionary[2] });
    }

    static List<string> PreparePuzzle()
    {
        List<string> aPuzzle = new();
        foreach (var item in puzzle)
        {
            if (item.Key.Contains('A'))
            {
                aPuzzle.Add(item.Key);
            }
        }
        return aPuzzle;
    }

    static long PuzzleWay(List<string> keysToFind)
    {
        // cr_mn: te mielimy jakby 3 petle w sobie i daje sie ze mozna to uproscic
        // - najbardziej zewnetrzna petla spokojnie moze by petla foreach
        // - petli ktora idzie po kolekcji "way" mozna sie calkowicie pozbyc
        // - wewnetrzna petla moze zostac zaimplementowana na zasadzie "while(true) { ... break; }"


        var loopNumber = new List<int>();

        foreach (var (keyToFind, keyIndex) in keysToFind.Select((keyToFind, keyIndex) => (keyToFind, keyIndex)))
        {
            var wayIndex = 0;
            var counter = 0;
            var currentKey = keyToFind;

            while (true)
            {
                currentKey = puzzle[currentKey][way[wayIndex] == 'R' ? 1 : 0];

                counter += 1;
                wayIndex = (wayIndex + 1) % way.Length;

                if (currentKey.Contains('Z'))
                {
                    break;
                }

            }

            loopNumber.Add(counter);
        }

        long returnResult = NwdForMany(loopNumber);
        foreach (var item in loopNumber)
        {
            Console.WriteLine(item);
            outputSum *= item;
        }
        return returnResult;
    }
    static long Nwd(long number1, long number2)
    {
        while (number2 != 0)
        {
            long temp = number2;
            number2 = number1 % number2;
            number1 = temp;
        }
        return number1;
    }
    static long NwdForOne(long number1, long number2)
    {
        number1 = Math.Abs(number1);
        number2 = Math.Abs(number2);
        return (number1 * number2) / Nwd(number1, number2);
    }

    static long NwdForMany(List<int> listOfCounters)
    {
        long result = listOfCounters[0];
        for (int i = 1; i < listOfCounters.Count; i++)
        {
            Console.Write(result + ", " + listOfCounters[i]);
            result = NwdForOne(result, listOfCounters[i]);
            Console.WriteLine(" = " + result);

        }
        return result;
    }

    internal static void Main8b_()
    {
        ReadFile("Day8/input.txt");
        List<string> keysList = PreparePuzzle();
        long output = PuzzleWay(keysList);
        Console.WriteLine("OutputSum: " + output);
    }
}