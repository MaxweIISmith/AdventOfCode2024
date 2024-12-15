using System.Text;
using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;

public static class Problem06Part1
{
    #region Public Methods

    public static void Solve()
    {
        var reader = File.OpenText("input.txt");

        var map = new List<StringBuilder>();

        Vec2i position = new Vec2i(0, 0);

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

        foreach (var line in map)
        {
            Console.WriteLine(line.ToString());
        }

        var sum = map.Sum(sb => sb.ToString().Count(c => c == 'X'));
        Console.WriteLine(sum);
    }

    #endregion Public Methods
}