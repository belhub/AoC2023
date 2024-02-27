class Program2a_
{
    static int outputSum = 0;

    static void ReadFile(string fileName)
    {
        foreach (var line in File.ReadAllLines(fileName)) // cr_mn: nowa petla
        {
            // cr_mn: nieuzywana zmienna  numbers
            //List<string> numbers = new List<string>();

            string[] columns = line.Split(':', ';', ',');
            int maksR = 0;
            int maksG = 0;
            int maksB = 0;

            for (int i = 0; i < columns.Length; i++)//TODO: second part
            {
                if (columns[i].Contains("green"))
                {
                    maksG = ExcludeMaxNumber(columns[i], maksG);
                }
                else if (columns[i].Contains("red"))
                {
                    maksR = ExcludeMaxNumber(columns[i], maksR);
                }
                else if (columns[i].Contains("blue"))
                {
                    maksB = ExcludeMaxNumber(columns[i], maksB);
                }
            }
            if (maksR <= 12 && maksG <= 13 && maksB <= 14)
            {
                outputSum += ExcludeMaxNumber(columns[0], 0); // cr_mn: cwany zabieg tutaj wycinajacy z tekstu numer gry

                //Console.WriteLine(line + " | " + "MRed: " + maksR + " | MGreen: " + maksG + " | MBlue: " + maksB);
            }
        }
    }

    static int ExcludeNumber(string str, int oldNum)
    {
        string[] tmpStr = str.Split(' ');
        int num = int.Parse(tmpStr[1].Trim());

        return Math.Max(num, oldNum);
    }

    // cr_mn: moze warto zmienic nazwe metody oraz argumentow aby byly bardziej czytelne 
    static int ExcludeMaxNumber(string namedNumber, int maxSoFar)
    {
        string[] tmpStr = namedNumber.Split(' ');
        int num = int.Parse(tmpStr[1].Trim());

        return Math.Max(num, maxSoFar);
    }

    internal static void Main2a()
    {
        ReadFile("Day2/input.txt");
        Console.WriteLine("OutputSum: " + outputSum);
    }
}
