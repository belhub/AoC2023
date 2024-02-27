using System.Data;

namespace Day7New
{
    public enum Type { HighCard, Pairs, TwoPairs, Three, Full, Four, Five }

    public record InHands(int Points, Type type, List<int> HighCard);

    class Program7anew_
    {
        static List<InHands> hands = new();

        static void ReadFile(string FileName)
        {
            string line;
            using (StreamReader file = new StreamReader(FileName))
            {

                while ((line = file.ReadLine()) != null)
                {
                    string[] strArray = line.Split(" ");
                    Type type;
                    char[] highCardArr = strArray[0].ToCharArray();
                    List<int> ints = new();
                    type = ClassifyCards(strArray[0]);
                    foreach (var highCard in highCardArr)
                    {
                        // cr_mn: faktycznie kozak (y)
                        int highCardint = highCard switch // ale kozak switch - KC VSCode
                        {
                            'T' => 10,
                            'J' => 11,
                            'Q' => 12,
                            'K' => 13,
                            'A' => 14,
                            _ => int.Parse(highCard.ToString()),
                        };
                        ints.Add(highCardint);
                    }
                    hands.Add(new InHands(int.Parse(strArray[1]), type, ints));
                }
            }
        }

        static Type ClassifyCards(string cards)
        {
            char firstsCard = cards.ToCharArray().First();

            var charCount = cards.ToCharArray().GroupBy(c => c)
            .Select(g => new { Char = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count);

            // cr_mn: to wyzej zapytanie LINQ lekko zmienione
            // - nie trzeba wolac .ToCharArray() bo typ string implementuje IEnumerable<char>
            // - no i moze lekko inne formatowanie samego kodu   
            // - my nizej wieke razy korzystamy z wyniku zapytania "charCount_" a wiec wiele razy wykonujemy cale zapytanie, brakuje 
            // wywolania ToList/ToArray

            var charCount_ = cards
                .GroupBy(c => c)
                .Select(g => new { Char = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToList();

            var result = charCount switch // ale vsCode zedytował mi ładnie switcha
            {
                var cha when cha.Where(c => c.Count == 5).Count() == 1 => Type.Five,
                var cha when cha.Where(c => c.Count == 4).Count() == 1 => Type.Four,
                var cha when cha.Where(c => c.Count == 3).Count() == 1 && cha.Where(c => c.Count == 2).Count() == 1 => Type.Full,
                var cha when cha.Where(c => c.Count == 3).Count() == 1 => Type.Three,
                var cha when cha.Where(c => c.Count == 2).Count() == 2 => Type.TwoPairs,
                var cha when cha.Where(c => c.Count == 2).Count() == 1 => Type.Pairs,
                _ => Type.HighCard,
            };

            // cr_mn: uwagi do kodu pattern matching wyzej
            // - zamiast wykonywac .Where(...).Count() mozna .Count(...)
            // - jesli chcemy sprawdzi czy istnieje w kolekcji element spelniajacy warunek to uzywamy .Any() bo on tylko przechodzi
            // elementy do momentu gdy warunek juz nie jest spelniony, gdy bazujemy na np .Count() >= 1 to niepotzrebnie przechodzmi po wszystkich
            // - Ty sobie posortowales po ilosc kart takich samych, wiec zamiast sprawdzac czy istnieje np ilosc 5 to mozna sobie 
            // sprawdzic jaki jest Count pierwszego elementu (bo posortowales)


            var firstCount = charCount.First().Count;
            var secondCount = charCount.Skip(1).FirstOrDefault()?.Count;

            var result_ = (firstCount, secondCount) switch
            {
                (5, _) => Type.Five,
                (4, _) => Type.Four,
                (3, 2) => Type.Full,
                (3, _) => Type.Three,
                (2, 2) => Type.TwoPairs,
                (2, _) => Type.Pairs,
                _ => Type.HighCard,
            };

            return result_;
        }

        static void SortedCard()
        {
            var newHands = hands.OrderBy(c => c.type)
                .ThenBy(c => c.HighCard[0])
                .ThenBy(c => c.HighCard[1])
                .ThenBy(c => c.HighCard[2])
                .ThenBy(c => c.HighCard[3])
                .ThenBy(c => c.HighCard[4]).ToList();

            var sum = newHands.Select((c, index) => c.Points * (index + 1)).Sum();
            Console.WriteLine("Sum: " + sum);
        }

        public static void Main7a()
        {
            ReadFile("Day7/input.txt");
            SortedCard();
        }
    }
}
