namespace AdventOfCode2024
{
    public class Problem10Part2
    {
        #region Public Methods

        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var map = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrEmpty(line))
                    continue;

                map.Add(line);
            }

            var maxX = map[0].Length;
            var maxY = map.Count;

            var sum = 0;
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (map[y][x] == '0')
                    {
                        var score = CountRoutes('0', map, x, y);

                        sum += score;
                    }
                }
            }

            Console.WriteLine(sum);
        }

        #endregion Public Methods

        #region Private Methods

        private static int CountRoutes(char ch, List<string> map, int x, int y)
        {
            var isInBound = 0 <= x && x < map[0].Length
                                   && 0 <= y && y < map[0].Length;

            if (!isInBound)
                return 0;

            if (map[y][x] != ch)
                return 0;

            if (ch == '9')
                return 1;

            var nextChar = (char)(ch + 1);

            return CountRoutes(nextChar, map, x + 1, y)
                   + CountRoutes(nextChar, map, x - 1, y)
                   + CountRoutes(nextChar, map, x, y + 1)
                   + CountRoutes(nextChar, map, x, y - 1);
        }

        #endregion Private Methods
    }
}