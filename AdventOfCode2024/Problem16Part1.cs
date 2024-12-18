using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public static class Problem16Part1
    {
        #region Public Methods

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

            Vec2i direction = Vec2i.UnitX;

            var scoreTable = new int[map.SizeX, map.SizeY];
            for (int i = 0; i < map.SizeX; i++)
            {
                for (int j = 0; j < map.SizeY; j++)
                {
                    scoreTable[i, j] = int.MaxValue;
                }
            }

            scoreTable[start.X, start.Y] = 0;

            FindLowestPath(map, start, direction, scoreTable);

            Console.WriteLine(scoreTable[end.X, end.Y]);
        }

        #endregion Public Methods

        #region Private Methods

        private static int CountDirectionScore(Vec2i curDirection, Vec2i newDirection)
        {
            const int rotateScore = 1000;

            if (curDirection == newDirection)
                return 0;

            if (curDirection == -newDirection)
            {
                return 2 * rotateScore;
            }

            return rotateScore;
        }

        private static void FindLowestPath(CharMap map, Vec2i pos, Vec2i direction, int[,] scoreTable)
        {
            foreach (var step in new[] { Vec2i.UnitX, -Vec2i.UnitX, Vec2i.UnitY, -Vec2i.UnitY })
            {
                var nextPos = pos + step;

                if (map[nextPos] == '#')
                    continue;

                var score = scoreTable[pos.X, pos.Y] + CountDirectionScore(direction, step) + 1;

                if (score <= scoreTable[nextPos.X, nextPos.Y])
                {
                    scoreTable[nextPos.X, nextPos.Y] = score;
                    FindLowestPath(map, nextPos, step, scoreTable);
                }
            }
        }

        #endregion Private Methods
    }
}