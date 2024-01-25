class Program
{
    class Numbers
    {
        public int MyNumber { get; set; }
        public required List<int> PositionY { get; set; }
    }

    static List<List<char>> matrix = new List<List<char>>();
    static List<List<Numbers>> numbersList = new();
    static int sum = 0;
    static void ReadFile(string FileName)
    {
        string line;
        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                char[] charArr = line.ToCharArray();
                List<char> listChar = new List<char>(charArr);
                matrix.Add(listChar);
            }
        }
    }
    static void GetNumbers()
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            List<Numbers> numbersToList = new();
            for (int j = 0; j < matrix[i].Count; j++)
            {
                int length = 0;

                if (char.IsNumber(matrix[i][j]))
                {
                    List<int> number = new List<int>();
                    List<int> indexList = new();
                    for (int index = 0; index <= 2; index++)
                    {
                        if (char.IsNumber(matrix[i][j + index]))
                        {
                            number.Add(int.Parse(matrix[i][j + index].ToString()));
                            indexList.Add(j + index);
                        }
                        else
                        {
                            break;
                        }
                    }
                    int numberInt = int.Parse(string.Join("", number));
                    length = number.Count;
                    numbersToList.Add(new Numbers { MyNumber = numberInt, PositionY = indexList });
                }
                j += length;
            }
            numbersList.Add(numbersToList);

        }
    }
    static void CheckMatrix()
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (matrix[i][j] == '*')
                {
                    List<int> numbersToMultiply = new();
                    if (j != 0 && char.IsNumber(matrix[i][j - 1]) || (j < matrix[i].Count - 1 && char.IsNumber(matrix[i][j + 1]))) //boki 
                    {
                        List<int> number1 = new List<int>();
                        List<int> number2 = new List<int>();
                        bool checkLeft = true;
                        bool checkRight = true;
                        for (int index = 1; index <= 3; index++)
                        {
                            if (checkLeft && char.IsNumber(matrix[i][j - index]))
                            {
                                number1.Insert(0, int.Parse(matrix[i][j - index].ToString()));
                            }
                            else
                            {
                                checkLeft = false;
                            }

                            if (checkRight && char.IsNumber(matrix[i][j + index]))
                            {
                                number2.Add(int.Parse(matrix[i][j + index].ToString()));
                            }
                            else
                            {
                                checkRight = false;
                            }
                        }
                        if (number1.Count > 0) { numbersToMultiply.Add(int.Parse(string.Join("", number1))); }
                        if (number2.Count > 0) { numbersToMultiply.Add(int.Parse(string.Join("", number2))); }
                    }
                    for (int index = 0; index <= 2; index++)
                    {
                        if (i != 0 && numbersList[i - 1].Any(number => number.PositionY.Contains(j - 1 + index)))//góra
                        {
                            var matchingNum1 = numbersList[i - 1].FirstOrDefault(number => number.PositionY.Contains(j - 1 + index));
                            numbersToMultiply.Add(matchingNum1.MyNumber);
                        }
                        if (i < matrix[i].Count - 1 && numbersList[i + 1].Any(number => number.PositionY.Contains(j - 1 + index)))//dół
                        {
                            var matchingNum2 = numbersList[i + 1].FirstOrDefault(number => number.PositionY.Contains(j - 1 + index));
                            numbersToMultiply.Add(matchingNum2.MyNumber);
                        }
                    }
                    int numToAdd = 0;
                    if (numbersToMultiply.Distinct().Count() > 1)
                    {
                        numToAdd = 1;
                        foreach (var num in numbersToMultiply.Distinct())
                        {
                            numToAdd *= num;
                        }
                    }
                    sum += numToAdd;
                }
            }
        }
    }


    static void Main()
    {
        ReadFile("input.txt");
        GetNumbers();
        CheckMatrix();
        Console.WriteLine("OutputSum: " + sum);
    }
}
