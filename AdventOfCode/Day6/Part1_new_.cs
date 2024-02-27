using System.Data;

class Program6anew_
{
    static int outputSum = 1;
    static List<int> distance = new();
    static List<int> time = new();

    static void ReadFile(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        foreach (var line in lines)
        {
            switch (line)
            {
                case string l when l.Contains("Time"):
                    time = l.Split(' ', ':').Skip(1).Where(d => d.Length > 0).Select(t => int.Parse(t.Trim())).ToList();
                    break;
                case string l when l.Contains("Distance"):
                    distance = l.Split(' ', ':').Skip(1).Where(d => d.Length > 0).Select(t => int.Parse(t.Trim())).ToList();
                    break;
            }
        }

        // cr_mn: kod niby fajny z pattern machhinf
        // - ale czesc kodu robi to samo, ale jest zduplikowana, tylko nazwa zmiennej (t vs d) 
        // - w pattern marching tutaj nie trzeba pisac "string l", wystarczy tutaj "l" 

        foreach (var line in File.ReadAllLines(fileName))
        {
            var values = line.Split(' ', ':').Skip(1).Where(d => d.Length > 0).Select(t => int.Parse(t.Trim())).ToList();

            if (line.Contains("Time"))
            {
                time = values;
            }
            else
            {
                distance = values;
            }
        }

        // cr_mn: lub zupelnie bez petli foreach bo ona "komplikuje"
        time = GetValues(lines[0]);
        distance = GetValues(lines[1]);

        // cr_mn: przyklad local function
        static List<int> GetValues(string line) =>
            line.Split(' ', ':').Skip(1).Where(d => d.Length > 0).Select(t => int.Parse(t.Trim())).ToList();
    }

    static void CalculateVariations()
    {
        for (int i = 0; i < distance.Count; i++)
        {
            int tmp = Enumerable.Range(1, time[i] - 1).Count(index => (time[i] - index) * index > distance[i]);
            Console.WriteLine(tmp);
            outputSum *= tmp;
        }

        // cr_mn: wyzej bardzo ladny .Range.Count() ale mozna zerknac na petle ze ona wlasciwie robi Zip
        outputSum = distance
            .Zip(time, (d, t) => Enumerable.Range(1, t - 1).Count(index => (t - index) * index > d))
            .Aggregate(1, (a, b) => a * b);
    }

    internal static void Main6a()
    {
        ReadFile("Day6/input.txt");
        CalculateVariations();
        Console.WriteLine("OutputSum: " + outputSum);
    }
}