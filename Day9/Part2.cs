class Program
{
    static List<List<int>> matrix = new List<List<int>>();
    static int sum = 0;
    static void ReadFile(string FileName)
    {
        string line;
        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                string[] numbers = line.Split(" ");
                List<int> rowInt = new();
                foreach (var number in numbers)
                {
                    rowInt.Add(int.Parse(number));
                }
                matrix.Add(rowInt);
            }
        }
    }

    static void History()
    {
        List<int> numbersToAdd = new();
        for (int index = 0; index < matrix.Count; index++)
        {
            bool stopCondition = true;
            List<List<int>> historyList = new();

            List<int> firstLine = matrix[index];
            int counter = 0;
            historyList.Add(firstLine);
            while (stopCondition)
            {
                List<int> secondLine = new List<int>();
                // for (int x = 0; x < counter; x++)
                // {
                //     secondLine.Add(0);
                // }
                for (int i = 1; i < firstLine.Count; i++)
                {
                    int number = firstLine[i] - firstLine[i - 1];
                    //Console.WriteLine(number);
                    secondLine.Add(number);
                }
                firstLine = secondLine;
                historyList.Add(firstLine);
                stopCondition = StopCondition(secondLine);
                counter++;
            }

            // foreach (var number in historyList)
            // {
            //     foreach (var num in number)
            //     {
            //         Console.Write(num + " |");
            //     }
            //     Console.WriteLine();
            // }

            int resultNumber = 0;
            for (int i = historyList.Count - 1; i >= 0; i--)
            {
                resultNumber = historyList[i][0] - resultNumber;
                //Console.WriteLine(resultNumber);
            }
            numbersToAdd.Add(resultNumber);
        }
        foreach (var num in numbersToAdd)
        {
            sum += num;
        }

    }
    static bool StopCondition(List<int> list)
    {
        foreach (var num in list)
        {
            if (num != 0)
            {
                return true;
            }
        }
        return false;
    }

    static void Main()
    {
        ReadFile("input.txt");
        History();
        Console.WriteLine("OutputSum: " + sum);
    }
}