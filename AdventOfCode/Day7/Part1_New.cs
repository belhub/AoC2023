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
//     pairs,
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
//                         'J' => 11,
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
//         var charCount = cards.ToCharArray().GroupBy(c => c)
//         .Select(g => new { Char = g.Key, Count = g.Count() })
//         .OrderByDescending(g => g.Count);

//         return charCount switch // ale vsCode zedytował mi ładnie switcha
//         {
//             var cha when cha.Where(c => c.Count == 5).Count() == 1 => Type.five,
//             var cha when cha.Where(c => c.Count == 4).Count() == 1 => Type.four,
//             var cha when cha.Where(c => c.Count == 3).Count() == 1 && cha.Where(c => c.Count == 2).Count() == 1 => Type.full,
//             var cha when cha.Where(c => c.Count == 3).Count() == 1 => Type.three,
//             var cha when cha.Where(c => c.Count == 2).Count() == 2 => Type.twoPairs,
//             var cha when cha.Where(c => c.Count == 2).Count() == 1 => Type.pairs,
//             _ => Type.highCard,
//         };
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