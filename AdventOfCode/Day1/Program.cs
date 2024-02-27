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

class Program1
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
                char[] charArray = line.ToCharArray();
                List<string> numbers = new List<string>();
                int lengthArr = charArray.Length;
                for (int i = 0; i < lengthArr; i++)
                {
                    if (Char.IsDigit(charArray[i]))
                    {
                        numbers.Add(charArray[i].ToString());
                    }
                    else if (i + 1 < lengthArr && Char.IsDigit(charArray[i + 1]))
                    {
                        numbers.Add(charArray[i + 1].ToString());
                        continue;
                    }
                    else
                    {
                        string DoSwitch()
                        {
                            switch (lengthArr - i - 1)
                            {
                                case < 2: return "x";
                                case 2: return $"{charArray[i]}{charArray[i + 1]}{charArray[i + 2]}";
                                case 3: return $"{charArray[i]}{charArray[i + 1]}{charArray[i + 2]}{charArray[i + 3]}";
                                case >= 4: return $"{charArray[i]}{charArray[i + 1]}{charArray[i + 2]}{charArray[i + 3]}{charArray[i + 4]}";
                            };
                        }
                        string tmp = DoSwitch();
                        if (tmp.Length < 3)
                        {
                            continue;
                        }
                        List<int> tmpArray = CheckStr(tmp);
                        if (tmpArray.Count > 1)
                        {
                            i += tmpArray[0] - 2;
                            numbers.Add(tmpArray[1].ToString());
                        }
                    }
                }

                if (numbers.Count == 1)
                {
                    int num = int.Parse($"{numbers.First()}{numbers.First()}");
                    outputSum += num;
                    Console.WriteLine("|" + num + "|");
                }
                else
                {
                    int num = int.Parse($"{numbers.First()}{numbers.Last()}");
                    outputSum += num;
                    Console.WriteLine("|" + num + "|");
                }
            }
        }
    }

    static List<int> CheckStr(string check)
    {
        List<int> ite = new List<int>();  //liczba iteracji w przód; wartość liczbowa
        int i = 0;
        foreach (string item in strNumbers)
        {
            if (check.Contains(item))
            {
                ite.Add(item.Length);
                ite.Add(i + 1);
            }
            i++;
        }
        return ite;
    }
    internal static void Main1()
    {
        ReadFile("Day1/input.txt");
        Console.WriteLine("OutputSum: " + outputSum);
    }
}
