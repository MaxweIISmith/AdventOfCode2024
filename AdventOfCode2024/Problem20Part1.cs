using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public static class Problem20Part1
    {
        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var lines = new List<string>();

            Vec2i start = new Vec2i();
            Vec2i end = new Vec2i();
            while (true)
            {
                var line = reader.ReadLine();

                if (String.IsNullOrEmpty(line))
                    break;

                lines.Add(line);

                var pos = line.IndexOf('S');
                if (pos != -1)
                {
                    start.X = pos;
                    start.Y = lines.Count - 1;
                }

                pos = line.IndexOf('E');
                if (pos != -1)
                {
                    end.X = pos;
                    end.Y = lines.Count - 1;
                }
            }

            var map = new CharMap(lines);

            var scoreTable = new int[map.SizeX, map.SizeY];
            for (int i = 0; i < map.SizeX; i++)
            {
                for (int j = 0; j < map.SizeY; j++)
                {
                    scoreTable[i, j] = int.MaxValue;
                }
            }

            scoreTable[start.X, start.Y] = 0;

            FindLowestPath(map, start, scoreTable);

            Console.WriteLine(scoreTable[end.X, end.Y]);

            const int cheatSize = 100;

            int count = 0;

            for (int y = 1; y < map.SizeY - 1; y++)
            {
                for (int x = 1; x < map.SizeX - 1; x++)
                {
                    if (map[x, y] == '#')
                    {
                        if (map[x - 1, y] != '#' && map[x + 1, y] != '#')
                        {
                            var diff = Math.Abs(scoreTable[x - 1, y] - scoreTable[x + 1, y]) - 2;
                            if (diff >= cheatSize)
                                count++;
                        }

                        if (map[x, y - 1] != '#' && map[x, y + 1] != '#')
                        {
                            var diff = Math.Abs(scoreTable[x, y - 1] - scoreTable[x, y + 1]) - 2;
                            if (diff >= cheatSize)
                                count++;
                        }
                    }
                }
            }

            Console.WriteLine(count);
        }

        private static void FindLowestPath(CharMap map, Vec2i pos, int[,] scoreTable)
        {
            var points = new Queue<Vec2i>([pos]);

            while (points.Count > 0)
            {
                var currentPos = points.Dequeue();

                foreach (var step in new[] { Vec2i.UnitX, -Vec2i.UnitX, Vec2i.UnitY, -Vec2i.UnitY })
                {
                    var nextPos = currentPos + step;

                    if (map[nextPos] == '#')
                        continue;

                    var score = scoreTable[currentPos.X, currentPos.Y] + 1;

                    if (score < scoreTable[nextPos.X, nextPos.Y])
                    {
                        scoreTable[nextPos.X, nextPos.Y] = score;
                        points.Enqueue(nextPos);
                    }
                }
            }
        }
    }
}