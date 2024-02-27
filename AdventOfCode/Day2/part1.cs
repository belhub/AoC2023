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

class Program2a
{
    static int outputSum = 0;
    static string[] strNumbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    static void ReadFile(string FileName)
    {
        string line;

        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                List<string> numbers = new List<string>();
                string[] columns = line.Split(':', ';', ',');
                int maksR = 0;
                int maksG = 0;
                int maksB = 0;
                for (int i = 0; i < columns.Length; i++)//TODO: second part
                {
                    if (columns[i].Contains("green"))
                    {
                        maksG = ExcludeNumber(columns[i], maksG);
                    }
                    else if (columns[i].Contains("red"))
                    {
                        maksR = ExcludeNumber(columns[i], maksR);
                    }
                    else if (columns[i].Contains("blue"))
                    {
                        maksB = ExcludeNumber(columns[i], maksB);
                    }
                }
                if (maksR <= 12 && maksG <= 13 && maksB <= 14)
                {
                    outputSum += ExcludeNumber(columns[0], 0);
                    //Console.WriteLine(line + " | " + "MRed: " + maksR + " | MGreen: " + maksG + " | MBlue: " + maksB);
                }

            }
        }
    }

    static int ExcludeNumber(string str, int oldNum)
    {
        string[] tmpStr = str.Split(' ');
        int num = int.Parse(tmpStr[1].Trim());

        return Math.Max(num, oldNum);
    }

    static void Main2a()
    {
        ReadFile("input.txt");
        Console.WriteLine("OutputSum: " + outputSum);
    }
}
