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
                if (matrix[i][j] == '*')
                {
                    if (j != 0 && char.IsNumber(matrix[i][j - 1])) //sprawdzanie liczba na górze i na dole 
                    {
                        List<int> number = new List<int>();

                        for (int index = 1; index <= 3; index++)
                        {
                            if (char.IsNumber(matrix[i][j - index]))
                            {
                                number.Add(int.Parse(matrix[i][j + index].ToString()));
                            }
                            else
                            {
                                break;
                            }
                        }
                        int numberInt = int.Parse(string.Join("", number));
                    }
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
