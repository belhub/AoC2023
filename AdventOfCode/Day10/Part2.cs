class Program
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    static List<List<char>> matrix = new List<List<char>>();
    static int[] positionStart = { 0, 0 };
    static List<int> nextPossibleMoveX = new();
    static List<int> nextPossibleMoveY = new();
    static List<Point> route = new(); //[x][y]
    static int sum = 0;
    static void ReadFile(string FileName)
    {
        string line;
        int positionX = 0;
        using (StreamReader file = new StreamReader(FileName))
        {
            while ((line = file.ReadLine()) != null)
            {
                List<char> row = new();
                char[] charArr = line.ToCharArray();
                for (int i = 0; i < charArr.Length; i++)
                {
                    row.Add(charArr[i]);
                    if (charArr[i] == 'S')
                    {
                        positionStart[0] = positionX;
                        positionStart[1] = i;
                    }
                }
                positionX++;
                matrix.Add(row);
            }
        }
    }

    static void CheckRoute()
    {
        int x = positionStart[0];
        int y = positionStart[1];
        while (true)
        {
            int xNext = ChooseWay(x, y).Item1;
            int yNext = ChooseWay(x, y).Item2;
            x = xNext;
            y = yNext;
            if (matrix[x][y] == '*')
            {
                break;
            }
            else
            {
                nextPossibleMoveX.Clear();
                nextPossibleMoveY.Clear();
                NextStepCheck(x, y);
                route.Add(new Point { X = x, Y = y });
                matrix[x][y] = '*';
            }
        }
    }

    static (int, int) ChooseWay(int currentX, int currentY)
    {
        if (currentX < matrix.Count - 1 && nextPossibleMoveX.Contains(currentX + 1) && matrix[currentX + 1][currentY] == '|')
        {
            currentX += 1;
        }
        else if (currentX != 0 && nextPossibleMoveX.Contains(currentX - 1) && matrix[currentX - 1][currentY] == '|')
        {
            currentX -= 1;
        }
        else if (currentX < matrix.Count - 1 && nextPossibleMoveX.Contains(currentX + 1) && (matrix[currentX + 1][currentY] == 'L' || matrix[currentX + 1][currentY] == 'J'))
        {
            currentX += 1;
        }
        else if (currentX != 0 && nextPossibleMoveX.Contains(currentX - 1) && (matrix[currentX - 1][currentY] == 'F' || matrix[currentX - 1][currentY] == '7'))
        {
            currentX -= 1;
        }
        else if (currentY < matrix[currentX].Count - 1 && nextPossibleMoveY.Contains(currentY + 1) && matrix[currentX][currentY + 1] == '-')
        {
            currentY += 1;
        }
        else if (currentY != 0 && nextPossibleMoveY.Contains(currentY - 1) && matrix[currentX][currentY - 1] == '-')
        {
            currentY -= 1;
        }
        else if (currentY < matrix[currentX].Count - 1 && nextPossibleMoveY.Contains(currentY + 1) && (matrix[currentX][currentY + 1] == '7' || matrix[currentX][currentY + 1] == 'J'))
        {
            currentY += 1;
        }
        else if (currentY != 0 && nextPossibleMoveY.Contains(currentY - 1) && (matrix[currentX][currentY - 1] == 'L' || matrix[currentX][currentY - 1] == 'F'))
        {
            currentY -= 1;
        }
        return (currentX, currentY);
    }

    static void NextStepCheck(int x, int y)
    {
        if (matrix[x][y] == 'S')
        {
            nextPossibleMoveX.Add(x - 1);
            nextPossibleMoveX.Add(x + 1);
            nextPossibleMoveY.Add(y - 1);
            nextPossibleMoveY.Add(y + 1);
        }
        if (matrix[x][y] == 'L')
        {
            nextPossibleMoveX.Add(x - 1);
            nextPossibleMoveY.Add(y + 1);
        }
        if (matrix[x][y] == 'F')
        {
            nextPossibleMoveX.Add(x + 1);
            nextPossibleMoveY.Add(y + 1);
        }
        if (matrix[x][y] == '7')
        {
            nextPossibleMoveX.Add(x + 1);
            nextPossibleMoveY.Add(y - 1);
        }
        if (matrix[x][y] == 'J')
        {
            nextPossibleMoveX.Add(x - 1);
            nextPossibleMoveY.Add(y - 1);
        }
        if (matrix[x][y] == '-')
        {
            nextPossibleMoveY.Add(y + 1);
            nextPossibleMoveY.Add(y - 1);
        }
        if (matrix[x][y] == '|')
        {
            nextPossibleMoveX.Add(x - 1);
            nextPossibleMoveX.Add(x + 1);
        }
    }

    static void CheckInside()
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                Point point = new Point { X = i, Y = j };
                if (IsInside(point) && matrix[point.X][point.Y] != '*')
                {
                    Console.WriteLine("X: " + point.X + " Y: " + point.Y + " - " + matrix[point.X][point.Y]);
                    sum++;
                }
            }
        }
    }

    static bool IsInside(Point point)
    {
        bool isInside = false;
        int n = route.Count;
        for (int i = 0, j = n - 1; i < n; j = i++)//Ray-Casting algorithm
        {
            if (((route[i].Y > point.Y) != (route[j].Y > point.Y)) &&
                (point.X < (route[j].X - route[i].X) * (point.Y - route[i].Y) / (route[j].Y - route[i].Y) + route[i].X))
            {
                isInside = !isInside;
            }
        }
        return isInside;
    }
    static void Main()
    {
        ReadFile("input.txt");
        CheckRoute();
        CheckInside();
        Console.WriteLine("OutputSum: " + sum);
    }
}