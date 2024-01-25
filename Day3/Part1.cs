class Program
{
    static List<List<char>> matrix = new List<List<char>>();
    static int sum = 0;
    //static List<char> list = new List<char>();
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
    static void CheckMatrix()
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                int length = 0;
                if (char.IsNumber(matrix[i][j]))
                {
                    List<int> number = new List<int>();

                    for (int index = 0; index <= 2; index++)
                    {
                        if (char.IsNumber(matrix[i][j + index]))
                        {
                            number.Add(int.Parse(matrix[i][j + index].ToString()));
                        }
                        else
                        {
                            break;
                        }
                    }
                    int numberInt = int.Parse(string.Join("", number));
                    length = number.Count;
                    List<char> charList = new List<char>();
                    for (int index = -1; index <= length; index++)
                    {
                        if (j + index < 0 || j + index >= matrix[i].Count)
                        {
                            continue;
                        }
                        if (i != 0 && (!char.IsNumber(matrix[i - 1][j + index]) && matrix[i - 1][j + index] != '.') ||
                            (!char.IsNumber(matrix[i][j + index]) && matrix[i][j + index] != '.') ||
                            (i != matrix.Count - 1 && !char.IsNumber(matrix[i + 1][j + index]) && matrix[i + 1][j + index] != '.'))
                        {
                            sum += numberInt;
                            break;
                        }
                    }
                    j += length;
                }
            }
        }
    }


    static void Main()
    {
        ReadFile("input.txt");
        CheckMatrix();
        Console.WriteLine("OutputSum: " + sum);
    }
}
