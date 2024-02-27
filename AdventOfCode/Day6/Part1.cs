class Program6a
{
    static int outputSum = 1;
    static List<int> distance = new();
    static List<int> time = new();
    static List<int> finalValues = new();

    static void ReadFile(string FileName)
    {
        string line;

        using (StreamReader file = new StreamReader(FileName))
        {

            while ((line = file.ReadLine()) != null)
            {
                string[] strArray = line.Split(" ", ':');
                for (int i = 1; i < strArray.Length; i++)
                {
                    if (strArray[i].Length > 0 && strArray[0] == "Time:")
                    {
                        time.Add(int.Parse(strArray[i]));
                    }
                    if (strArray[i].Length > 0 && strArray[0] == "Distance:")
                    {
                        distance.Add(int.Parse(strArray[i]));
                    }
                }
            }
        }
    }

    static void CalculateVariations()
    {

        for (int i = 0; i < distance.Count; i++)
        {
            int tmp = 0;
            for (int index = 1; index < time[i]; index++)
            {
                if ((time[i] - index) * index > distance[i])
                {
                    tmp++;
                }
            }
            Console.WriteLine("Output: " + tmp);
            outputSum *= tmp;
        }
    }

    internal static void Main6a()
    {
        ReadFile("input.txt");
        CalculateVariations();
        Console.WriteLine("Time: ");

        foreach (var item in time)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
        Console.WriteLine("Distance: ");

        foreach (var item in distance)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine("OutputSum: " + outputSum);
    }
}