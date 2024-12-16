using System.Text;
using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public static class Problem15Part1
    {
        #region Public Methods

        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var lines = new List<string>();

            Vec2i position = new Vec2i();
            while (true)
            {
                var line = reader.ReadLine();

                if (String.IsNullOrEmpty(line))
                    break;

                lines.Add(line);

                var x = line.IndexOf('@');

                if (x != -1)
                {
                    position.X = x;
                    position.Y = lines.Count - 1;
                }
            }

            var map = new CharMap(lines);

            var moves = new StringBuilder();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                moves.Append(line);
            }

            var moveVectors = new Dictionary<char, Vec2i>()
            {
                {'^', new Vec2i(0, -1)},
                {'v', new Vec2i(0, 1)},
                {'>', new Vec2i(1, 0)},
                {'<', new Vec2i(-1, 0)},
            };

            foreach (var moveChar in moves.ToString())
            {
                var move = moveVectors[moveChar];
                MakeMove(map, ref position, move);
            }

            var gpsValue = 0;

            for (int x = 0; x < map.SizeX; x++)
            {
                for (int y = 0; y < map.SizeY; y++)
                {
                    if (map[x, y] == 'O')
                    {
                        gpsValue += x + 100 * y;
                    }
                }
            }

            Console.WriteLine(gpsValue);
        }

        #endregion Public Methods

        #region Private Methods

        private static bool MakeMove(CharMap map, ref Vec2i position, Vec2i move)
        {
            var nextCell = position + move;

            if (map[nextCell] == '#')
            {
                return false;
            }

            if (map[nextCell] == '.')
            {
                map[nextCell] = map[position];
                map[position] = '.';
                position += move;

                return true;
            }

            if (MakeMove(map, ref nextCell, move))
            {
                return MakeMove(map, ref position, move);
            }

            return false;
        }

        #endregion Private Methods
    }
}