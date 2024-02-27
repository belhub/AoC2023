class Program8a_
{
    static int outputSum = 0;

    // cr_mn: tutaj moze wartosc w slowniku powinna nie tablica tylko para (krotka), moga byc tylko 2 elementy,
    // to bardziej wyraza w kodzie co sie dzieje, ale z punktu widzenia obslugi pamieci, krotka jest bardzo prosta
    // struktura w porownaniu z tablica

    static Dictionary<string, string[]> puzzle = new();
    static char[] way = null;

    static void ReadFile(string FileName)
    {
        string line;
        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                if (way == null) { way = line.ToCharArray(); }
                else { if (line.Length > 0) { AddToDictionary(line); } }
            }
        }
    }

    static void AddToDictionary(string input)
    {
        string[] toDictionary = input.Replace("(", "").Replace(")", "").Replace(" ", "").Split('=', ',');
        puzzle.Add(toDictionary[0].Trim(), new string[] { toDictionary[1], toDictionary[2] });
    }

    static void PuzzleWay()
    {
        string keyToFind = "AAA";
        bool stopCondition = true;
        while (stopCondition)
        {
            for (int i = 0; i < way.Length; i++)
            {
                var valueToGo = puzzle[keyToFind];
                int goToStep = way[i] == 'R' ? 1 : 0;

                keyToFind = valueToGo[goToStep];
                outputSum += 1;
                if (keyToFind == "ZZZ")
                {
                    // cr_mn: tutaj zamiast zmiennej 'stopCondition' oraz 'break' mozna uzyc `return;`

                    stopCondition = false;
                    break;
                }
            }
        }
    }

    internal static void Main8a()
    {
        ReadFile("Day8/input.txt");
        PuzzleWay();
        Console.WriteLine("OutputSum: " + outputSum);
    }
}