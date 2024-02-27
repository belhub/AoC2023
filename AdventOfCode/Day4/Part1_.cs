class Program4a_
{
    static int sum = 0;

    static void ReadFile(string fileName)
    {
        foreach (var line in File.ReadAllLines(fileName))
        {
            string[] cardArr = line.Split('|', ':');
            string[] leftSide = cardArr[1].Split(" ");
            string[] rightSide = cardArr[2].Split(" ");

            int winningNum = leftSide.Select(n => n.Trim())
                .Intersect(rightSide.Select(n => n.Trim()))
                .Count() - 1;

            if (winningNum > 0)
            {
                sum += (int)Math.Pow(2, winningNum - 1);
                Console.WriteLine(winningNum + " | " + Math.Pow(2, winningNum - 1));
            }
        }

    }

    public static void Main4a()
    {
        ReadFile("Day4/input.txt");
        Console.WriteLine("OutputSum: " + sum);
    }
}