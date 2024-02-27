class Program9a_
{
    static List<List<int>> matrix = new();
    static int sum = 0;
    static void ReadFile(string fileName)
    {
        // cr_mn: czytanie danych za pomoca LINQ
        matrix = File.ReadAllLines(fileName).Select(line => line.Split(" ").Select(int.Parse).ToList()).ToList();

        // string line;
        // using (StreamReader file = new StreamReader(FileName))
        // {
        //     while ((line = file.ReadLine()) != null)
        //     {
        //         string[] numbers = line.Split(" ");
        //         matrix.Add(new(numbers.Select(n => int.Parse(n))));
        //     }
        // }
    }

    static void History()
    {

        // cr_mn: uwagi do kodu "while (stopCondition) { ... stopCondition = StopCondition(second) }"
        // - akurat jak osobiscie nie przepadam "funkcjami jedno-linijkowcami", tzn jesli funkcja jest wolana w wielu miejscach lub
        // chcemy dla niej napisac test jednostkowy to jeszcze tak czasem robie, ale uncle bob w ksiazce "clean code" zdecydowanie zaleca
        // takie pisanie :) wiec kazdy ma swoja opinie        
        // - niezaleznie tutaj maja taka uwage ze nazwa tej funkcji nie bardzo mowi akurat co ta funkcja robi, wiec kazdy czytajac kod
        // i tak musi przejsc do jej definicji aby sprawdzic, byc powinna sie nazywa "AreAllValues0()"
        // - "stopCondition" zmienna moze nazwalbym "allValues0" i sama petla mialaby odwrotny warunek  "while(!allValues0) { ... }"

        // cr_mn: jesli chodzi ponizszy kod i petle
        // - zewnetrzna petla spokojnie moze byc zwyklem "foreach"
        // - wewnetrzna moze byc "while(true) {...break/return; }"
        // -- generalnie sie sugeruje tym aby bylo mniej zmiennych, aby kod lepiej mowil co robi tzn
        // jesli iterujemy po kolejnych elementach to "foreach" jakby to realizuje, wtedy nie trzba sie zastanawiac
        // czy moze z jakis jeszcze innych powodow byla zastosowana petla "for"

        List<int> numbersToAdd = new();

        foreach (var line in matrix)
        {
            var currentLine = line;
            var historyList = new List<List<int>>() { currentLine };

            while (true)
            {
                // cr_mn: jesli chcemy procesowac kolne 2 lub wiecej elementow w kolekcji to zazwyczaj do tego mamy funkcje
                // Pairwise/Windows/Buffer, w LINQ zostana takie metody dodane od kolejnej wersji, ale poki co mozna to 
                // prosto zrealizowac za pomoca Zip+Skip

                // var nexLine = currentLine.Select((f, index) => f - currentLine.ElementAtOrDefault(index - 1)).Skip(1).ToList();
                var nexLine = currentLine.Zip(currentLine.Skip(1), (prev, next) => next - prev).ToList();

                historyList.Add(nexLine);
                currentLine = nexLine;

                if (nexLine.All(l => l == 0))
                {
                    break;
                }
            }

            // cr_mn: w sumie jak przeczytalem ponizszy kod gdzie idziesz od ostatniego elementu do pierwszego zauwazylem ze w sumie
            // kolejnosc przechodzenie nie ma znaczenia i mozna przechodzic od poczatku do konca
            var resultNumber = historyList.Sum(line => line[^1]);

            // int resultNumber = 0;
            // for (int i = historyList.Count - 1; i >= 0; i--)
            // {
            //     resultNumber += historyList[i][^1];
            // }

            numbersToAdd.Add(resultNumber);
        }

        sum = numbersToAdd.Sum();
    }


    static bool StopCondition(List<int> list)
    {
        return !list.All(l => l == 0);
    }

    internal static void Main9a_()
    {
        ReadFile("Day9/input.txt");
        History();
        Console.WriteLine("OutputSum: " + sum);
    }
}