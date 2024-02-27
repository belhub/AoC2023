using System.Runtime.InteropServices;

class Program3a
{
    class Numbers
    {
        public int MyNumber { get; set; }
        public required int PositionX { get; set; }
        public required List<int> PositionY { get; set; }
    }

    static List<List<char>> matrix = new();
    static List<Numbers> numbersWithCoo = new();
    static int sum = 0;
    static void ReadFile(string FileName)
    {
        string line;
        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                List<char> listChar = line.ToCharArray().ToList();
                matrix.Add(listChar);
            }
        }
    }

    static void TakeNumbers()
    {
        var list = matrix.SelectMany((row, i) =>
                row.Select((cha, j) => new { MyNumber = cha, PositionX = i, PositionY = j })
            .Where(item => char.IsNumber(item.MyNumber))
            .OrderBy(data => data.PositionX).ThenBy(data => data.PositionY)).ToList();

        var numbersData = list
        .Select((num, index) => new { First = num, Second = list.ElementAtOrDefault(index + 1), Third = list.ElementAtOrDefault(index + 2) }).ToList();

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
            numbersWithCoo.Add(new Numbers { MyNumber = number, PositionX = numbersData[i].First.PositionX, PositionY = ys });
        }
    }

    static void CheckChars()
    {
        var chars = matrix.SelectMany((row, i) =>
            row.Select((cha, j) => new { Char = cha, PosX = i, PosY = j })
                .Where(data => !char.IsNumber(data.Char) && data.Char != '.')).ToList();

        foreach (var cha in chars)
        {
            var sumListss = numbersWithCoo.Where(nums =>
            cha.PosX == nums.PositionX && (nums.PositionY.Contains(cha.PosY + 1) || nums.PositionY.Contains(cha.PosY - 1)) ||
            (cha.PosX - 1 == nums.PositionX || cha.PosX + 1 == nums.PositionX) && (nums.PositionY.Contains(cha.PosY + 1) || nums.PositionY.Contains(cha.PosY) || nums.PositionY.Contains(cha.PosY - 1)))
            .Select(nums => nums.MyNumber).Sum();
            sum += sumListss;
        }
    }
    static void Main3a()
    {
        ReadFile("input.txt");
        TakeNumbers();
        CheckChars();
        Console.WriteLine("OutputSum: " + sum);
    }
}