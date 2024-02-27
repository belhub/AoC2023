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

class Program8a
{
    static int outputSum = 0;
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

    static void PuzzleWay()
    {
        string keyToFind = "AAA";
        bool stopCondition = true;
        while (stopCondition)
        {
            for (int i = 0; i < way.Length; i++)
            {
                var valueToGo = puzzle[keyToFind];
                int goToStep = way[i] == 'R' ? 1 : 0;

                keyToFind = valueToGo[goToStep];
                outputSum += 1;
                if (keyToFind == "ZZZ")
                {
                    stopCondition = false;
                    break;
                }
            }
        }
    }

    static void Main8a()
    {
        ReadFile("input.txt");
        PuzzleWay();
        Console.WriteLine("OutputSum: " + outputSum);
    }
}