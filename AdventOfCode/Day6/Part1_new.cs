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
// class Program
// {
//     static int outputSum = 1;
//     static List<int> distance = new();
//     static List<int> time = new();
//     static void ReadFile(string FileName)
//     {
//         string line;

//         using (StreamReader file = new StreamReader(FileName))
//         {

//             while ((line = file.ReadLine()) != null)
//             {
//                 switch (line)
//                 {
//                     case string l when l.Contains("Time"):
//                         time = l.Split(' ', ':').Skip(1).Where(d => d.Length > 0).Select(t => int.Parse(t.Trim())).ToList();
//                         break;
//                     case string l when l.Contains("Distance"):
//                         distance = l.Split(' ', ':').Skip(1).Where(d => d.Length > 0).Select(d => int.Parse(d.Trim())).ToList();
//                         break;
//                 }
//             }
//         }
//     }

//     static void CalculateVariations()
//     {
//         for (int i = 0; i < distance.Count; i++)
//         {
//             int tmp = Enumerable.Range(1, time[i] - 1)
//                         .Count(index => (time[i] - index) * index > distance[i]);
//             Console.WriteLine(tmp);
//             outputSum *= tmp;
//         }
//     }

//     static void Main()
//     {
//         ReadFile("input.txt");
//         CalculateVariations();
//         Console.WriteLine("OutputSum: " + outputSum);
//     }
// }