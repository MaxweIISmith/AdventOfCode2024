//https://adventofcode.com/2024/

using System.Text;

namespace AdventOfCode2024
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            var reader = File.OpenText("input.txt");

            var map = new List<StringBuilder>();

            Vec2i position = null;

            var directions = new Dictionary<char, Vec2i>()
            {
                { '^', new Vec2i(x: 0, y: -1) },
                { '>', new Vec2i(x: 1, y: 0) },
                { '<', new Vec2i(x: -1, y: 0) },
                { 'v', new Vec2i(x: 0, y: 1) },
            };

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (String.IsNullOrEmpty(line))
                    continue;

                map.Add(new StringBuilder(line));

                foreach (var directionChar in directions.Keys)
                {
                    var pos = line.IndexOf(directionChar);

                    if (pos != -1)
                        position = new Vec2i(pos, map.Count - 1);
                }
            }

            if (position == null)
            {
                throw new InvalidOperationException();
            }

            var maxX = map[0].Length;
            var maxY = map.Count;

            while (true)
            {
                var currentDirection = map[position.Y][position.X];

                map[position.Y][position.X] = 'X';

                var directionStep = directions[currentDirection];

                var newPosition = position + directionStep;

                if (newPosition.X < 0 || newPosition.X >= maxX || newPosition.Y < 0 || newPosition.Y >= maxY)
                {
                    break;
                }

                var nextCell = map[newPosition.Y][newPosition.X];

                if (nextCell == '#')
                {
                    if (currentDirection == '^')
                        currentDirection = '>';
                    else if (currentDirection == '>')
                        currentDirection = 'v';
                    else if (currentDirection == 'v')
                        currentDirection = '<';
                    else if (currentDirection == '<')
                        currentDirection = '^';

                    map[position.Y][position.X] = currentDirection;
                }
                else
                {
                    position = newPosition;
                    map[position.Y][position.X] = currentDirection;
                }
            }

            var sum = map.Sum(sb => sb.ToString().Count(c => c == 'X'));

            Console.WriteLine(sum);

#if DEBUG
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
#endif
        }

        #endregion Private Methods

        #region Private Classes

        private class Vec2i
        {
            #region Public Constructors

            public Vec2i(int x, int y)
            {
                X = x;
                Y = y;
            }

            #endregion Public Constructors

            #region Public Properties

            public int X { get; set; }

            public int Y { get; set; }

            #endregion Public Properties

            #region Public Methods

            public static Vec2i operator +(Vec2i lhs, Vec2i rhs)
            {
                return new Vec2i(lhs.X + rhs.X, lhs.Y + rhs.Y);
            }

            #endregion Public Methods
        }

        #endregion Private Classes
    }
}