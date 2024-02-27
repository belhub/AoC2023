// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Data;
// using System.Diagnostics;
// using System.Threading.Tasks;
// using System.Drawing;
// using System.Text.RegularExpressions;
// using System.Runtime.CompilerServices;
// using System.Linq;
// using System.Security.Cryptography;

// public enum Type
// {
//     highCard,
//     pair,
//     twoPairs,
//     three,
//     full,
//     four,
//     five
// }

// public class InHands
// {
//     public int Points { get; set; }
//     public Type type;
//     public required List<int> HighCard { get; set; }
// }

// class Program
// {
//     static List<InHands> hands = new();

//     static void ReadFile(string FileName)
//     {
//         string line;
//         using (StreamReader file = new StreamReader(FileName))
//         {

//             while ((line = file.ReadLine()) != null)
//             {
//                 string[] strArray = line.Split(" ");
//                 Type type;
//                 char[] highCardArr = strArray[0].ToCharArray();
//                 List<int> ints = new();
//                 type = ClassifyCards(strArray[0]);
//                 foreach (var highCard in highCardArr)
//                 {
//                     int highCardint = highCard switch // ale kozak switch - KC VSCode
//                     {
//                         'T' => 10,
//                         'J' => 1, //<- now weeker than 2
//                         'Q' => 12,
//                         'K' => 13,
//                         'A' => 14,
//                         _ => int.Parse(highCard.ToString()),
//                     };
//                     ints.Add(highCardint);
//                 }
//                 hands.Add(new InHands { Points = int.Parse(strArray[1]), type = type, HighCard = ints });
//             }
//         }
//     }

//     static Type ClassifyCards(string cards)
//     {
//         char firstsCard = cards.ToCharArray().First();
//         var charCount = cards.ToCharArray()
//             .GroupBy(c => c)
//             .Select(g => new { Char = g.Key, Count = g.Count() })
//             .OrderByDescending(g => g.Count);

//         var countJ = charCount.Select(c =>
//                 {
//                     return c.Char == 'J' ? c.Count : 0;
//                 }).Max();

//         var result = charCount switch // ale vsCode zedytował mi ładnie switcha
//         {
//             var cha when cha.Where(c => c.Count == 5).Count() == 1 => Type.five,
//             var cha when cha.Where(c => c.Count == 4 && c.Char != 'J').Count() == 1 => Type.four,
//             var cha when cha.Where(c => c.Count == 3 && c.Char != 'J').Count() == 1 && cha.Where(c => c.Count == 2 && c.Char != 'J').Count() == 1 => Type.full,
//             var cha when cha.Where(c => c.Count == 3 && c.Char != 'J').Count() == 1 => Type.three,
//             var cha when cha.Where(c => c.Count == 2 && c.Char != 'J').Count() == 2 => Type.twoPairs,
//             var cha when cha.Where(c => c.Count == 2 && c.Char != 'J').Count() == 1 => Type.pair,
//             _ => Type.highCard,
//         };

//         switch (countJ)
//         {
//             case 1:
//                 if (result is >= (Type)1 and <= (Type)3)
//                 {
//                     result += 2;
//                 }
//                 else if (result <= Type.four)
//                 {
//                     result++;
//                 }
//                 break;
//             case 2:
//                 if (result == Type.three || result == Type.highCard)
//                 {
//                     result += 3;
//                 }
//                 else if (result == Type.pair)
//                 {
//                     result += 4;

//                 }
//                 break;
//             case 3:
//                 if (result <= Type.pair)
//                     result += 5;
//                 break;
//             case 4:
//                 if (result == Type.highCard)
//                     result = Type.five;
//                 break;
//         }
//         return result;
//     }

//     static void SortedCard()
//     {
//         var newHands = hands.OrderBy(c => c.type)
//             .ThenBy(c => c.HighCard[0])
//             .ThenBy(c => c.HighCard[1])
//             .ThenBy(c => c.HighCard[2])
//             .ThenBy(c => c.HighCard[3])
//             .ThenBy(c => c.HighCard[4]).ToList();

//         var sum = newHands.Select((c, index) => c.Points * (index + 1)).Sum();
//         Console.WriteLine("Sum: " + sum);
//     }

//     static void Main()
//     {
//         ReadFile("input.txt");
//         SortedCard();
//     }
// }