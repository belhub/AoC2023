using System.Data;

// cr_mn: wartosc enumow z duzej litery, pewnie tez bym napisal w jednej lini

public enum Type { HighCard, Pairs, TwoPairs, Tree, Full, Four, Five }

// public class InHands
// {
//     public int Points { get; set; }
//     public Type type;
//     public int[] HighCard { get; set; }
// }

// cr_mn: klasa idealnie nadaje sie pod record
record InHands(int Points, Type Type, int[] HighCard);

class Program7a_
{
    static int outputSum = 0;
    static Dictionary<int, string> handCards = new Dictionary<int, string>();

    static void ReadFile(string FileName)
    {
        // string line;
        // using (StreamReader file = new StreamReader(FileName))
        // {

        //     while ((line = file.ReadLine()) != null)
        //     {
        //         string[] strArray = line.Split(" ");
        //         handCards.Add(int.Parse(strArray[1]), strArray[0]);
        //     }
        // }

        // cr_mn: jednolinijkowiec za pomoca LINQ

        handCards = File.ReadLines(FileName)
           .Select(line => line.Split(" "))
           .ToDictionary(parts => int.Parse(parts[1]), parts => parts[0]);

        // cr_mn: tutaj wybrales typ danych Dictionary<int, string> do zapisania danycy, ale ogolnie to chyba "bid" moze sie powtarzac,
        // tak jak same karty, pewnie zamiast slownika trzeba uzyc "listy par"
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
                        firstsCardInt[x] = 11;
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

            foreach (var card in cards)
            {
                int tmp = 0;
                char last = cards.LastOrDefault();
                Type typeSwitch = Type.HighCard;

                for (int i = 0; i < cards.Length; i++)
                {
                    if (card == cards[i] && card != '.')
                    {
                        tmp++;
                        cards[i] = '.';
                    }
                }
                if (tmp > 1)
                {
                    switch (tmp)
                    {
                        case 2:
                            if (wasPair)
                            {
                                typeSwitch = Type.TwoPairs;
                                sortedC.RemoveAt(sortedC.Count - 1);//usuń ostani i na jego miejsce dodaj 
                            }
                            else if (wasThree)
                            {
                                typeSwitch = Type.Full;
                                sortedC.RemoveAt(sortedC.Count - 1);//usuń ostani i na jego miejsce dodaj 
                            }
                            else
                            {
                                typeSwitch = Type.Pairs;
                            }
                            added = true;
                            break;
                        case 3:
                            if (wasPair)
                            {
                                typeSwitch = Type.Full;
                                sortedC.RemoveAt(sortedC.Count - 1);//usuń ostani i na jego miejsce dodaj 
                            }
                            else
                            {
                                typeSwitch = Type.Tree;
                            }
                            added = true;
                            break;
                        case 4:
                            typeSwitch = Type.Four;
                            added = true;
                            break;
                        case 5:
                            typeSwitch = Type.Five;
                            added = true;
                            break;
                    }
                    string allcardsWeight = string.Join("", firstsCardInt);

                    // cr_mn: wywolanie konstruktora rekordu zamiast "object initializer"
                    //sortedC.Add(new InHands { Points = item.Key, type = typeSwitch, HighCard = firstsCardInt });
                    sortedC.Add(new InHands(item.Key, typeSwitch, firstsCardInt));
                }
                if (!added && last == card)
                {
                    string allcardsWeight = string.Join("", firstsCardInt);
                    // cr_mn: dla lepiej czytelnosci mozna uzyc "named parameters"
                    //sortedC.Add(new InHands { Points = item.Key, type = Type.HighCard, HighCard = firstsCardInt });
                    sortedC.Add(new InHands(Points: item.Key, Type: Type.HighCard, HighCard: firstsCardInt));
                }
                if (tmp == 3) { wasThree = true; }
                if (tmp == 2) { wasPair = true; }

            }
        }

        var fullSortedCards = sortedC.OrderBy(h => h.Type)
        .ThenBy(h => h.HighCard[0])
        .ThenBy(h => h.HighCard[1])
        .ThenBy(h => h.HighCard[2])
        .ThenBy(h => h.HighCard[3])
        .ThenBy(h => h.HighCard[4]).ToList();

        for (int i = 1; i <= fullSortedCards.Count; i++)
        {
            outputSum += fullSortedCards[i - 1].Points * i;
        }
        foreach (var item in fullSortedCards)
        {
            Console.WriteLine(item.Points + " | " + item.Type);
        }
        Console.WriteLine(sortedC.Count);
    }

    public static void Main7a()
    {
        ReadFile("Day7/input.txt");
        SortedCard();
        Console.WriteLine("OutputSum: " + outputSum);
    }
}