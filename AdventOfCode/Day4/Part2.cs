class Program4b
{
    static int sum = 0;
    static List<int> winingNumsList = new();
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
                winingNumsList.Add(winningNum);
            }
        }
    }

    static void CalculateScore()
    {
        List<int> copies = Enumerable.Repeat(1, winingNumsList.Count).ToList();
        for (int i = 0; i < winingNumsList.Count; i++)
        {
            for (int index = 0; index < winingNumsList[i]; index++)
            {
                copies[i + index + 1] += copies[i];
            }
        }
        sum = copies.Sum();
    }

    static void Main4b()
    {
        ReadFile("input.txt");
        CalculateScore();
        Console.WriteLine("OutputSum: " + sum);
    }
}