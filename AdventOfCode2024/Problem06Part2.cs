using System.Text;
using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;

public class Problem06Part2
{
    #region Public Methods

    public static void Solve()
    {
        var reader = File.OpenText("input.txt");

        var map = new List<StringBuilder>();

        Vec2i initPosition = new Vec2i(0, 0);
        char initDirection = Char.MinValue;

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
                {
                    initPosition = new Vec2i(pos, map.Count - 1);
                    initDirection = directionChar;
                }
            }
        }

        var maxX = map[0].Length;
        var maxY = map.Count;

        int loopCount = 0;

        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                var currentCell = map[y][x];

                if (currentCell == '#'
                    || currentCell == '^'
                    || currentCell == '>'
                    || currentCell == 'v'
                    || currentCell == '<')
                    continue;

                map[y][x] = '#';

                var position = initPosition;
                var direction = initDirection;

                var moveHistory = new Dictionary<Vec2i, HashSet<char>>();
                bool isLoop = false;

                while (true)
                {
                    if (!moveHistory.TryGetValue(position, out var track))
                    {
                        track = new HashSet<char>();
                        moveHistory[position] = track;
                    }

                    if (!track.Add(direction))
                    {
                        isLoop = true;
                        break;
                    }

                    var directionStep = directions[direction];

                    var newPosition = position + directionStep;

                    if (newPosition.X < 0 || newPosition.X >= maxX || newPosition.Y < 0 || newPosition.Y >= maxY)
                    {
                        break;
                    }

                    var nextCell = map[newPosition.Y][newPosition.X];

                    if (nextCell == '#')
                    {
                        if (direction == '^')
                            direction = '>';
                        else if (direction == '>')
                            direction = 'v';
                        else if (direction == 'v')
                            direction = '<';
                        else if (direction == '<')
                            direction = '^';
                    }
                    else
                    {
                        position = newPosition;
                    }
                }

                if (isLoop)
                    loopCount++;

                map[y][x] = '.';
            }
        }

        Console.WriteLine(loopCount);
    }

    #endregion Public Methods
}