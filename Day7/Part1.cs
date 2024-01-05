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

public class Card // dokończ - spróbowac z obiektem
{
    public int Value { get; set; }
    public int Weight { get; set; }
}

class Program
{
    static int outputSum = 0;
    static Dictionary<int, List<string>> handCards = new Dictionary<int, List<string>>();

    static void ReadFile(string FileName)
    {
        string line;


        using (StreamReader file = new StreamReader(FileName))
        {

            while ((line = file.ReadLine()) != null)
            {
                List<string> strList = new List<string>();

                string[] strArray = line.Split(" ");
                char[] charArr = strArray[0].ToCharArray();
                foreach (var item in charArr)
                {
                    int tmp = 0;
                    for (int i = 0; i < charArr.Length; i++)
                    {
                        if (item == charArr[i] && item != '.')
                        {
                            tmp++;
                            charArr[i] = '.';
                        }
                    }
                    if (item != '.')
                    {
                        if (int.TryParse(item.ToString(), out _))
                        {
                            strList.Add(item.ToString());
                        }
                        else
                        {
                            switch (item.ToString())
                            {
                                case "T":
                                    strList.Add("10");
                                    break;
                                case "J":
                                    strList.Add("11");
                                    break;
                                case "Q":
                                    strList.Add("12");
                                    break;
                                case "K":
                                    strList.Add("13");
                                    break;
                                case "A":
                                    strList.Add("14");
                                    break;
                            }
                        }
                        strList.Add(tmp.ToString());
                    }
                }
                handCards.Add(int.Parse(strArray[1]), strList);
            }
        }
    }

    static void SortedCard()
    {

        foreach (var items in handCards)
        {
            Console.Write("Key:  " + items.Key + " Values: ");//posortować to gówno !!!!

            for (int i = 1; i < items.Value.Count; i += 2)
            {
                //jakie tu kurwa dać warunki? chuj wie XDDDD

                Console.Write(items.Value[i] + " ");
            }

            Console.WriteLine();
        }
    }

    static void Main()
    {
        ReadFile("input.txt");
        SortedCard();
        // foreach (var items in handCards)
        // {
        //     Console.Write("Key:  " + items.Key + " Values: ");

        //     foreach (var iter in items.Value)
        //     {
        //         Console.Write(" " + iter);
        //     }
        //     Console.WriteLine();

        // }

        Console.WriteLine("OutputSum: " + outputSum);
    }
}