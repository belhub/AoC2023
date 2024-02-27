using System.Runtime.InteropServices;

class Program3a_
{
    // cr_mn: w umie dlaczego tutaj przy 2 wlasciwosciach jest "required"
    // class Numbers
    // {
    //     public int MyNumber { get; set; }
    //     public required int PositionX { get; set; }
    //     public required List<int> PositionY { get; set; }
    // }

    // cr_mn: jesli faktycznie klasa jest jedyniem pojemnikiem i nie zmieniamy zawartosci
    // to idealny wydaje sie record
    record Numbers(int MyNumber, int PositionX, List<int> PositionY);

    // cr_mn: tutaj "char[]" zamiast "List<char>" wystarczy, caly kod bez zmian dziala tak samo    
    //static List<List<char>> matrix = new();
    static List<char[]> matrix = new();

    static List<Numbers> numbersWithCoo = new();
    static int sum = 0;

    static void ReadFile(string fileName)
    {
        // string line;
        // using (StreamReader file = new StreamReader(FileName))
        // {
        //     while ((line = file.ReadLine()) != null)
        //     {
        //         List<char> listChar = line.ToCharArray().ToList();
        //         matrix.Add(listChar);
        //     }
        // }

        // cr_mn: uzycie File.ReadLines
        matrix = File.ReadLines(fileName).Select(line => line.ToCharArray()).ToList();
    }

    static void TakeNumbers()
    {
        // cr_mn: ogolnie to kto wie jak w tekscie formatowac zapytania LINQ :/ ale (chyba) mozna troche spojniej/czytelniej

        // cr_mn: tutaj nizej uzywany jest typ anonimowy o takich samych nazwach wlasciwosci jak klasa "Numbers" ale
        // "MyNumber" nie jest "int" tylko "char", to moze byc mylace
        // - byc moze cos mi sie myli ale w takim zapisie "PositionX = i" i jest numerem wiersza wiec pomyslalbym ze to 
        // bardziej Y, nie X jak jest tutaj nazwane
        var list = matrix
            .SelectMany((row, i) => row.Select((cha, j) => new { MyNumber = cha, PositionX = i, PositionY = j })
            .Where(item => char.IsNumber(item.MyNumber))
            .OrderBy(data => data.PositionX)
            .ThenBy(data => data.PositionY))
            .ToList();

        var numbersData = list
            .Select((num, index) => new { First = num, Second = list.ElementAtOrDefault(index + 1), Third = list.ElementAtOrDefault(index + 2) })
            .ToList();


        // cr_mn: uproszczony zapis gdzie:
        // - korzystajacy z tuple zamiast typow anonimowych
        // - nie ma tutaj sortowania        
        var list2 = matrix
            .SelectMany((row, rowIndex) => row.Select((digit, columnIndex) => (digit, rowIndex, columnIndex)))
            .Where(t => char.IsNumber(t.digit))
            .ToList();

        var numbersData2 = list2
            .Select((num, index) => (First: num, Second: list.ElementAtOrDefault(index + 1), Third: list.ElementAtOrDefault(index + 2)))
            .ToList();


        //for który będzie wyłaniać liczby z ich współrzędnymi
        for (int i = 0; i < numbersData.Count; i++)
        {
            int number = 0;
            string numStr = "";
            List<int> ys = new();

            if (numbersData[i].First.PositionX == numbersData[i]?.Second?.PositionX && numbersData[i].First.PositionX == numbersData[i]?.Third?.PositionX)
            {
                if (numbersData[i].First.PositionY == numbersData[i].Second.PositionY - 1 && numbersData[i].First.PositionY == numbersData[i].Third.PositionY - 2)
                {
                    numStr = numbersData[i].First.MyNumber.ToString() + numbersData[i].Second.MyNumber.ToString() + numbersData[i].Third.MyNumber.ToString();
                    ys.Add(numbersData[i].First.PositionY);
                    ys.Add(numbersData[i].Second.PositionY);
                    ys.Add(numbersData[i].Third.PositionY);
                    i += 2;
                }
                else if (numbersData[i].First.PositionY == numbersData[i].Second.PositionY - 1)
                {
                    numStr = new(numbersData[i].First.MyNumber.ToString() + numbersData[i].Second.MyNumber.ToString());
                    ys.Add(numbersData[i].First.PositionY);
                    ys.Add(numbersData[i].Second.PositionY);
                    i++;
                }
                else
                {
                    numStr = new(numbersData[i].First.MyNumber.ToString());
                    ys.Add(numbersData[i].First.PositionY);
                }
                number = int.Parse(numStr);
            }
            else if (numbersData[i].First.PositionX == numbersData[i]?.Second?.PositionX)
            {
                if (numbersData[i].First.PositionY == numbersData[i].Second.PositionY - 1)
                {
                    numStr = new(numbersData[i].First.MyNumber.ToString() + numbersData[i].Second.MyNumber.ToString());
                    ys.Add(numbersData[i].First.PositionY);
                    ys.Add(numbersData[i].Second.PositionY);
                    i++;
                }
                else
                {
                    numStr = new(numbersData[i].First.MyNumber.ToString());
                    ys.Add(numbersData[i].First.PositionY);
                }
                number = int.Parse(numStr);
            }
            else
            {
                numStr = new(numbersData[i].First.MyNumber.ToString());
                ys.Add(numbersData[i].First.PositionY);
                number = int.Parse(numStr);
            }
            // cr_mn: po zmianie z klasy na rekord musimy wykonywac konstruktor
            //numbersWithCoo.Add(new Numbers { MyNumber = number, PositionX = numbersData[i].First.PositionX, PositionY = ys });

            numbersWithCoo.Add(new Numbers(number, numbersData[i].First.PositionX, ys));
        }
    }

    static void CheckChars()
    {
        // cr_mn: tutaj takze formalowania zapyatn LINQ ma znaczenie
        var chars = matrix
            .SelectMany((row, i) => row.Select((cha, j) => new { Char = cha, PosX = i, PosY = j })
            .Where(data => !char.IsNumber(data.Char) && data.Char != '.'))
            .ToList();

        foreach (var cha in chars)
        {
            // cr_mn: ... && ... || (... && ...)
            // - przy takich warunkach moze warto pisac dodatkowe nawiasy ()... && ...) || (... && ...) i koniecznie formowaniem kodu podpowiadac co sie dzieje
            // - nizej zamiast Select( ... => .... ).Sum() mozna tylko ..Sum( ... => ...)
            var sumListss = numbersWithCoo
                .Where(nums =>
                    (cha.PosX == nums.PositionX) &&
                    (nums.PositionY.Contains(cha.PosY + 1) || nums.PositionY.Contains(cha.PosY - 1)) ||
                    (cha.PosX - 1 == nums.PositionX || cha.PosX + 1 == nums.PositionX) &&
                    (nums.PositionY.Contains(cha.PosY + 1) || nums.PositionY.Contains(cha.PosY) || nums.PositionY.Contains(cha.PosY - 1))
                    )
                .Sum(nums => nums.MyNumber);

            sum += sumListss;
        }
    }

    // cr_mn: ponizszy kod pieknie prezentuje problemy z kodem imperatywny/mutujacym/obiektowym
    // - kazda z funkcji zwraca void i cos zmienia na zewnatrz
    // - ciezko analizowac taki kod bo zamiast skupiac sie na jednej konretnej funkcji to musimy analizowac cos sie dzieje w calej klasie
    // machamy glowa w gore i w dol wieloktornie ("kod funkcyjny czyta sie jednorazowo od gory do dołu, od lewej do prawej")
    // - zakomentowanie jednej funkcji lub zmiana kolejnosci kompletnie wywraca program do gory nogami 
    // - kod funkcyjny wyglada tak:
    // const val1 = func1(123)
    // const val2 = func2(val1, 869)
    // const val3 = func3(val1, val2)

    public static void Main3a()
    {
        ReadFile("Day3/input.txt");
        TakeNumbers();
        CheckChars();
        Console.WriteLine("OutputSum: " + sum);
    }
}