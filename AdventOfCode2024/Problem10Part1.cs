using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public static class Problem10Part1
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
                        var topCoordinates = new HashSet<Vec2i>();

                        ExploreRoute('0', map, x, y, topCoordinates);

                        sum += topCoordinates.Count;
                    }
                }
            }

            Console.WriteLine(sum);
        }

        #endregion Public Methods

        #region Private Methods

        private static void ExploreRoute(char ch, List<string> map, int x, int y, HashSet<Vec2i> topCoordinates)
        {
            var isInBound = 0 <= x && x < map[0].Length
                && 0 <= y && y < map[0].Length;

            if (!isInBound)
                return;

            if (map[y][x] != ch)
                return;

            if (ch == '9')
                topCoordinates.Add(new Vec2i(x, y));

            var nextChar = (char)(ch + 1);

            ExploreRoute(nextChar, map, x + 1, y, topCoordinates);
            ExploreRoute(nextChar, map, x - 1, y, topCoordinates);
            ExploreRoute(nextChar, map, x, y + 1, topCoordinates);
            ExploreRoute(nextChar, map, x, y - 1, topCoordinates);
        }

        #endregion Private Methods
    }
}