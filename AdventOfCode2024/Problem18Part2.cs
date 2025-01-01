using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public class Problem18Part2
    {
        public static void Solve()
        {
            var rowsCount = 71;
            var columnsCount = 71;
            var bytesCount = 1024;

            var lines = new List<string>();

            for (int i = 0; i < rowsCount; i++)
            {
                lines.Add(new string(Enumerable.Repeat('.', columnsCount).ToArray()));
            }

            var charMap = new CharMap(lines);

            var reader = File.OpenText("input.txt");

            var bytes = new List<Vec2i>();


            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var parts = line.Split(',');

                var x = Int32.Parse(parts[0]);
                var y = Int32.Parse(parts[1]);

                var b = new Vec2i(x, y);

                charMap[b] = '#';

                bytes.Add(b);
            }

            bytes.Reverse();

            int[,] scoreTable = new int[columnsCount, rowsCount];
            foreach (var b in bytes)
            {
                charMap[b] = '.';

                for (int i = 0; i < columnsCount; i++)
                {
                    for (int j = 0; j < rowsCount; j++)
                    {
                        scoreTable[i, j] = int.MaxValue;
                    }
                }

                var start = new Vec2i();

                scoreTable[start.X, start.Y] = 0;

                FindLowestPath(charMap, start, scoreTable);

                if (scoreTable[columnsCount - 1, rowsCount - 1] != Int32.MaxValue)
                {
                    Console.WriteLine(b);
                    break;
                }
            }
        }

        private static void FindLowestPath(CharMap map, Vec2i pos, int[,] scoreTable)
        {
            foreach (var step in new[] { Vec2i.UnitX, -Vec2i.UnitX, Vec2i.UnitY, -Vec2i.UnitY })
            {
                var nextPos = pos + step;

                if (nextPos.X < 0 || nextPos.X >= map.SizeX || nextPos.Y < 0 || nextPos.Y >= map.SizeY)
                    continue;

                if (map[nextPos] == '#')
                    continue;

                var score = scoreTable[pos.X, pos.Y] + 1;

                if (score < scoreTable[nextPos.X, nextPos.Y])
                {
                    scoreTable[nextPos.X, nextPos.Y] = score;
                    FindLowestPath(map, nextPos, scoreTable);
                }
            }
        }
    }
}