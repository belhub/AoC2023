// cr_mn: zawsze usuwalem nadmiarowe usingi bo bylo ich bardzo duzo


class Program1_
{
    static int outputSum = 0;
    static string[] strNumbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    // cr_mn: argumenty w C# nazywamy z malych, fileName zamiast FileName
    static void ReadFile(string fileName)
    {
        // cr_mn: w przypadku prostego czytania plikow tekstowych to klasy statyczne File/Directory/Path zazwyczaj wystarcza,
        // nie ma potrzeby korzystania ze strumieni

        // string line;
        // using (StreamReader file = new StreamReader(FileName))
        // {
        //     while ((line = file.ReadLine()) != null)


        foreach (var line in File.ReadAllLines(fileName))
        {
            // cr_mn: pozwalilem sobie zamienic na "var" :)
            var charArray = line.ToCharArray();
            var numbers = new List<string>();
            var lengthArr = charArray.Length;

            for (int i = 0; i < lengthArr; i++)
            {
                // cr_mn: zamiast Char/String/Int32/... zazwyczaj uzywa sia aliasow char/string/int

                if (char.IsDigit(charArray[i]))
                {
                    numbers.Add(charArray[i].ToString());
                }
                else if (i + 1 < lengthArr && char.IsDigit(charArray[i + 1]))
                {
                    numbers.Add(charArray[i + 1].ToString());
                    // cr_mn: ogolnie (chyba) kod uzywajacy break/continue uchodzi za "trudnu w zrozumieniu"
                    continue;
                }
                else
                {
                    // cr_mn: nizej zdefiniowana byla "local function"  (funkcja w funkcji) ktora korzystala z clsure
                    // oraz wolana byla tylko raz nizej, wydaje sie ze jest to "za bardzo skomplikowane" bo mozna to
                    // zamienic po prosty na cialo tej funkcji 
                    // - ciekawe ze sumie ze pattern marching jest "wyczerpana" (wszystkie warunki sa uwzglednione)

                    // string DoSwitch()
                    // {
                    //     switch (lengthArr - i - 1)
                    //     {
                    //         case < 2: return "x";
                    //         case 2: return $"{charArray[i]}{charArray[i + 1]}{charArray[i + 2]}";
                    //         case 3: return $"{charArray[i]}{charArray[i + 1]}{charArray[i + 2]}{charArray[i + 3]}";
                    //         case >= 4: return $"{charArray[i]}{charArray[i + 1]}{charArray[i + 2]}{charArray[i + 3]}{charArray[i + 4]}";
                    //     };
                    // }

                    //string tmp = DoSwitch();

                    string tmp = (lengthArr - i - 1) switch
                    {
                        < 2 => "x",
                        2 => $"{charArray[i]}{charArray[i + 1]}{charArray[i + 2]}",
                        3 => $"{charArray[i]}{charArray[i + 1]}{charArray[i + 2]}{charArray[i + 3]}",
                        >= 4 => $"{charArray[i]}{charArray[i + 1]}{charArray[i + 2]}{charArray[i + 3]}{charArray[i + 4]}"
                    };

                    // cr_mn: aby nie duplikowac wyzej kod mozna napisac cos takiego
                    var l = lengthArr - i - 1;
                    var tmp2 = l < 2 ? "x" : string.Concat(Enumerable.Range(i, (l >= 4 ? 4 : l) + 1).Select(j => charArray[j]));
                    // tmp = tmp2;

                    if (tmp.Length < 3)
                    {
                        continue;
                    }

                    List<int> tmpArray = CheckStr(tmp);
                    if (tmpArray.Count > 1)
                    {
                        // cr_mn: tutaj i po ktorym iterujemy w petli jest zmieniany, ciezko nadazac za takim kodem :(
                        i += tmpArray[0] - 2;
                        numbers.Add(tmpArray[1].ToString());
                    }
                }
            }
            if (numbers.Count == 1)
            {
                int num = int.Parse($"{numbers.First()}{numbers.First()}");
                outputSum += num;
                Console.WriteLine("|" + num + "|");
            }
            else
            {
                int num = int.Parse($"{numbers.First()}{numbers.Last()}");
                outputSum += num;
                Console.WriteLine("|" + num + "|");
            }
        }

    }

    static List<int> CheckStr(string check)
    {
        List<int> ite = new List<int>();  //liczba iteracji w przód; wartość liczbowa
        int i = 0;
        foreach (string item in strNumbers)
        {
            if (check.Contains(item))
            {
                ite.Add(item.Length);
                ite.Add(i + 1);
            }
            i++;
        }
        return ite;
    }
    internal static void Main1()
    {
        ReadFile("Day1/input.txt");
        Console.WriteLine("OutputSum: " + outputSum);
    }
}
