using System.Drawing;
using System.Globalization;

class Program
{
    static List<long> seedToSoil = new();
    static List<long> soilToFertilizer = new();
    static List<long> fertilizerToWater = new();
    static List<long> waterToLight = new();
    static List<long> lightToTemperature = new();
    static List<long> temperatureToHumidity = new();
    static List<long> humidityToLocation = new();
    static List<long> seeds = new();
    static List<long> sum = new();
    static void ReadFile(string FileName)
    {
        string line;
        int lineCounter = 0;
        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                if (line.Contains("map"))
                {
                    lineCounter++;
                    continue;
                }
                string[] seedStr = line.Split(" ");
                switch (lineCounter)
                {
                    case 0:
                        for (int i = 1; i < seedStr.Length; i++)
                        {
                            seeds.Add(long.Parse(seedStr[i]));
                        }
                        break;
                    case 1:
                        foreach (var number in seedStr)
                        {
                            seedToSoil.Add(long.Parse(number));
                        }
                        break;
                    case 2:
                        foreach (var number in seedStr)
                        {
                            soilToFertilizer.Add(long.Parse(number));
                        }
                        break;
                    case 3:
                        foreach (var number in seedStr)
                        {
                            fertilizerToWater.Add(long.Parse(number));
                        }
                        break;
                    case 4:
                        foreach (var number in seedStr)
                        {
                            waterToLight.Add(long.Parse(number));
                        }
                        break;
                    case 5:
                        foreach (var number in seedStr)
                        {
                            lightToTemperature.Add(long.Parse(number));
                        }
                        break;

                    case 6:
                        foreach (var number in seedStr)
                        {
                            temperatureToHumidity.Add(long.Parse(number));
                        }
                        break;
                    case 7:
                        foreach (var number in seedStr)
                        {
                            humidityToLocation.Add(long.Parse(number));
                        }
                        break;
                }
            }
        }
    }
    static void Almanac() //optymalizacja
    {
        for (int i = 0; i < seeds.Count; i += 2)
        {

            long seed = seeds[i];
            long number = 0;
            Console.WriteLine("Seeds: " + seed);

            for (long num = seed; num < seed + seeds[i + 1]; num++)
            {
                List<long> operation = new();
                int iter = 0;

                while (iter <= 7)
                {
                    switch (iter)
                    {
                        case 0:
                            operation = seedToSoil;
                            number = num;
                            break;
                        case 1:
                            operation = soilToFertilizer;
                            break;
                        case 2:
                            operation = fertilizerToWater;
                            break;
                        case 3:
                            operation = waterToLight;
                            break;
                        case 4:
                            operation = lightToTemperature;
                            break;
                        case 5:
                            operation = temperatureToHumidity;
                            break;
                        case 6:
                            operation = humidityToLocation;
                            break;
                    }
                    number = Solve(number, operation);
                    iter++;
                }

                sum.Add(number);
            }
            long minNumber1 = 999999999999;
            foreach (var min in sum)
            {
                minNumber1 = Math.Min(minNumber1, min);
            }
            Console.WriteLine("Min: " + minNumber1);
        }
        long minNumber = 999999999999;
        foreach (var min in sum)
        {
            minNumber = Math.Min(minNumber, min);
        }
        Console.WriteLine("Min: " + minNumber);
    }

    static long Solve(long seed, List<long> almanac)
    {
        for (int index = 0; index < almanac.Count; index += 3)
        {
            long numberToCheck = almanac[index];
            long startNumber = almanac[index + 1];
            long almanaCounter = almanac[index + 2];
            if (seed >= startNumber && seed < almanaCounter + startNumber)
            {
                long tmp = seed - startNumber;
                return numberToCheck + tmp;
            }
        }
        return seed;
    }
    static void Main()
    {
        ReadFile("input.txt");
        Almanac();
    }
}
// 37806486 <--