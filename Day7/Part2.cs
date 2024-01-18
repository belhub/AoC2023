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

public enum Type
{
    highCard,
    pairs,
    twoPairs,
    tree,
    full,
    four,
    five
}

public class InHands
{
    public int Points { get; set; }
    public Type type;
    public int[] HighCard { get; set; }
}

class Program
{
    static int outputSum = 0;
    static Dictionary<int, string> handCards = new Dictionary<int, string>();

    static void ReadFile(string FileName)
    {
        string line;
        using (StreamReader file = new StreamReader(FileName))
        {

            while ((line = file.ReadLine()) != null)
            {
                string[] strArray = line.Split(" ");
                handCards.Add(int.Parse(strArray[1]), strArray[0]);
            }
        }
    }

    static void SortedCard()
    {
        List<InHands> sortedC = new();

        foreach (var item in handCards)
        {
            char[] cards = item.Value.ToCharArray();
            bool wasPair = false;
            bool wasThree = false;
            bool added = false;
            int[] firstsCardInt = { 0, 0, 0, 0, 0 };
            for (int x = 0; x < 5; x++)
            {
                switch (cards[x])
                {
                    case 'T':
                        firstsCardInt[x] = 10;
                        break;
                    case 'J':
                        firstsCardInt[x] = 1;
                        break;
                    case 'Q':
                        firstsCardInt[x] = 12;
                        break;
                    case 'K':
                        firstsCardInt[x] = 13;
                        break;
                    case 'A':
                        firstsCardInt[x] = 14;
                        break;
                    default:
                        firstsCardInt[x] = int.Parse(cards[x].ToString());
                        break;
                }
            }

            int amountJ = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == 'J')
                {
                    amountJ++;
                }
            }
            foreach (var card in cards)
            {
                int tmp = 0;
                char last = cards.LastOrDefault();
                Type typeSwitch = Type.highCard;

                for (int i = 0; i < cards.Length; i++)
                {
                    if (card == cards[i] && card != '.' && card != 'J')
                    {
                        tmp++;
                        cards[i] = '.';
                    }
                }
                if (tmp > 1)
                {
                    switch (tmp + amountJ)
                    {
                        case 2:
                            if (wasPair)
                            {
                                typeSwitch = Type.twoPairs;
                                sortedC.RemoveAt(sortedC.Count - 1);//usuń ostani i na jego miejsce dodaj 
                            }
                            else if (wasThree)
                            {
                                typeSwitch = Type.full;
                                sortedC.RemoveAt(sortedC.Count - 1);//usuń ostani i na jego miejsce dodaj 
                            }
                            else
                            {
                                amountJ = 0;
                                wasPair = true;
                                typeSwitch = Type.pairs;
                            }
                            added = true;
                            break;
                        case 3:
                            if (wasPair)
                            {
                                typeSwitch = Type.full;
                                sortedC.RemoveAt(sortedC.Count - 1);//usuń ostani i na jego miejsce dodaj 
                            }
                            else
                            {
                                wasThree = true;
                                typeSwitch = Type.tree;
                                amountJ = 0;
                            }
                            added = true;
                            break;
                        case 4:
                            typeSwitch = Type.four;
                            added = true;
                            break;
                        case 5:
                            typeSwitch = Type.five;
                            added = true;
                            break;
                    }
                    sortedC.Add(new InHands { Points = item.Key, type = typeSwitch, HighCard = firstsCardInt });
                }
                if (!added && last == card)
                {
                    switch (amountJ)
                    {
                        case 0:
                            typeSwitch = Type.highCard;
                            break;
                        case 1:
                            typeSwitch = Type.pairs;
                            break;
                        case 2:
                            typeSwitch = Type.tree;
                            break;
                        case 3:
                            typeSwitch = Type.four;
                            break;
                        case 4:
                            typeSwitch = Type.five;
                            break;
                        case 5:
                            typeSwitch = Type.five;
                            break;
                    }
                    sortedC.Add(new InHands { Points = item.Key, type = typeSwitch, HighCard = firstsCardInt });
                    added=true;
                }
                if(sortedC.Count>2 &&sortedC[sortedC.Count - 2].Points==sortedC[sortedC.Count - 1].Points){//jakimś cudem niektóre się duplikują i trzeba było je usunąć
                    sortedC.RemoveAt(sortedC.Count - 2);
                }
            }
        }
        var fullSortedCards = sortedC.OrderBy(h => h.type)
        .ThenBy(h => h.HighCard[0])
        .ThenBy(h => h.HighCard[1])
        .ThenBy(h => h.HighCard[2])
        .ThenBy(h => h.HighCard[3])
        .ThenBy(h => h.HighCard[4]).ToList();
        for (int i = 1; i <= fullSortedCards.Count; i++)
        {
            outputSum += fullSortedCards[i - 1].Points * i;
        }
        foreach (var item in sortedC)
        {
            Console.WriteLine(item.Points + " | " + item.type);
        }
        Console.WriteLine(sortedC.Count);
    }
    static void Main()
    {
        ReadFile("input.txt");
        SortedCard();
        Console.WriteLine("OutputSum: " + outputSum);
    }
}