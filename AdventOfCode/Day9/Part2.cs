// class Program
// {
//     static List<List<int>> matrix = new List<List<int>>();
//     static int sum = 0;
//     static void ReadFile(string FileName)
//     {
//         string line;
//         using (StreamReader file = new StreamReader(FileName))
//         {
//             while ((line = file.ReadLine()) != null)
//             {
//                 string[] numbers = line.Split(" ");
//                 matrix.Add(new(numbers.Select(n => int.Parse(n))));
//             }
//         }
//     }

//     static void History()
//     {
//         List<int> numbersToAdd = new();
//         for (int index = 0; index < matrix.Count; index++)
//         {
//             bool stopCondition = true;
//             List<List<int>> historyList = new();

//             List<int> firstLine = matrix[index];
//             historyList.Add(firstLine);
//             while (stopCondition)
//             {
//                 var second = firstLine.Select((f, index) => f - firstLine.ElementAtOrDefault(index - 1)).Skip(1).ToList();
//                 firstLine = second;
//                 historyList.Add(firstLine);
//                 stopCondition = StopCondition(second);
//             }

//             int resultNumber = 0;
//             for (int i = historyList.Count - 1; i >= 0; i--)
//             {
//                 resultNumber = historyList[i][0] - resultNumber;
//             }
//             numbersToAdd.Add(resultNumber);
//         }
//         sum = numbersToAdd.Sum();
//     }
//     static bool StopCondition(List<int> list)
//     {
//         return !list.All(l => l == 0);
//     }

//     static void Main()
//     {
//         ReadFile("input.txt");
//         History();
//         Console.WriteLine("OutputSum: " + sum);
//     }
// }