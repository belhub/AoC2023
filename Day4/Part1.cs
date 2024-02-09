class Program
{
    static int sum = 0;
    static void ReadFile(string FileName)
    {
        string line;
        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                string[] cardArr = line.Split('|', ':');
                string[] leftSide = cardArr[1].Split(" ");
                string[] rightSide = cardArr[2].Split(" ");
                int winningNum = leftSide.Select(item => item.Trim()).Intersect(rightSide.Select(data => data.Trim())).Count() - 1;

                if (winningNum > 0)
                {
                    sum += (int)Math.Pow(2, winningNum - 1);
                    Console.WriteLine(winningNum + " | " + Math.Pow(2, winningNum - 1));
                }
            }
        }
    }

    static void Main()
    {
        ReadFile("input.txt");
        Console.WriteLine("OutputSum: " + sum);
    }
}