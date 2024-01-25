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
class Program
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
                else
                {
                    string[] puzzleString = line.Split("=");
                    if (puzzleString.Length > 1)
                    {
                        string stringWithoutBrackets = puzzleString[1].Replace("(", "").Replace(")", "").Replace(" ", "");
                        string[] puzzleValue = stringWithoutBrackets.Split(",");
                        puzzle.Add(puzzleString[0].Trim(), puzzleValue);
                    }
                }
            }
        }
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
    static void Main()
    {
        ReadFile("input.txt");
        PuzzleWay();
        Console.WriteLine("OutputSum: " + outputSum);
    }
}