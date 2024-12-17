using System.Text;
using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public static class Problem15Part2
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

                line = line.Replace("#", "##")
                    .Replace(".", "..")
                    .Replace("O", "[]")
                    .Replace("@", "@.");

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
                TryMakeMove(map, ref position, move);
            }

            var gpsValue = 0;

            for (int x = 0; x < map.SizeX; x++)
            {
                for (int y = 0; y < map.SizeY; y++)
                {
                    if (map[x, y] == '[')
                    {
                        gpsValue += x + 100 * y;
                    }
                }
            }

            Console.WriteLine(gpsValue);
        }

        #endregion Public Methods

        #region Private Methods

        private static bool CanMoveHorizontal(CharMap map, Vec2i position, Vec2i move)
        {
            var nextPosition = position + move;

            var nextObj = map[nextPosition];

            if (nextObj == '.')
                return true;

            if (nextObj == '#')
                return false;

            if (nextObj == '[' || nextObj == ']')
                return CanMoveHorizontal(map, nextPosition, move);

            return false;
        }

        private static bool CanMoveVertical(CharMap map, Vec2i position, Vec2i move)
        {
            var nextPosition = position + move;

            var nextObj = map[nextPosition];

            if (nextObj == '.')
                return true;

            if (nextObj == '#')
                return false;

            if (nextObj == '[')
                return CanMoveVertical(map, nextPosition, move) && CanMoveVertical(map, nextPosition + Vec2i.UnitX, move);

            if (nextObj == ']')
                return CanMoveVertical(map, nextPosition, move) && CanMoveVertical(map, nextPosition - Vec2i.UnitX, move);

            return false;
        }

        private static void MoveHorizontal(CharMap map, Vec2i position, Vec2i move)
        {
            var nextPosition = position + move;

            var nextObj = map[nextPosition];

            if (nextObj != '.')
            {
                MoveHorizontal(map, nextPosition, move);
            }

            map[nextPosition] = map[position];
            map[position] = '.';
        }

        private static void MoveVertical(CharMap map, Vec2i position, Vec2i move)
        {
            var nextPosition = position + move;

            var nextObj = map[nextPosition];

            if (nextObj != '.')
            {
                if (nextObj == '[')
                {
                    MoveVertical(map, nextPosition, move);
                    MoveVertical(map, nextPosition + Vec2i.UnitX, move);
                }
                else
                {
                    MoveVertical(map, nextPosition, move);
                    MoveVertical(map, nextPosition - Vec2i.UnitX, move);
                }
            }

            map[nextPosition] = map[position];
            map[position] = '.';
        }

        private static void TryMakeMove(CharMap map, ref Vec2i position, Vec2i move)
        {
            if (move.Y != 0)
            {
                if (!CanMoveVertical(map, position, move))
                    return;

                MoveVertical(map, position, move);
            }
            else
            {
                if (!CanMoveHorizontal(map, position, move))
                    return;

                MoveHorizontal(map, position, move);
            }

            position += move;
        }

        #endregion Private Methods
    }
}
