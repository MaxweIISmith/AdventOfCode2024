using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public class Problem14Part2
    {
        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var regex = new Regex("p=(?<x>\\d+),(?<y>\\d+) v=(?<vX>-{0,1}\\d+),(?<vY>-{0,1}\\d+)");

            var robots = new List<Robot>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var match = regex.Match(line);

                var x = Int32.Parse(match.Groups["x"].Value);
                var y = Int32.Parse(match.Groups["y"].Value);
                var vX = Int32.Parse(match.Groups["vX"].Value);
                var vY = Int32.Parse(match.Groups["vY"].Value);

                robots.Add(new Robot()
                {
                    Position = new Vec2i(x, y),
                    Velocity = new Vec2i(vX, vY)
                });
            }

            var fieldSizeX = 101;
            var fieldSizeY = 103;

            var time = 84;

            while (time < fieldSizeX * fieldSizeY)
            {
                var list = new List<Robot>();

                foreach (var robot in robots)
                {
                    var (x, y) = robot.Position + time * robot.Velocity;
                    x = (x % fieldSizeX + fieldSizeX) % fieldSizeX;
                    y = (y % fieldSizeY + fieldSizeY) % fieldSizeY;

                    list.Add(new Robot()
                    {
                        Position = new Vec2i(x, y)
                    });
                }

                PrintMap(list, fieldSizeX, fieldSizeY);
                Console.WriteLine($"Time={time}===============================================================");
                Console.ReadKey(false);

                time += 103;
            }
        }

        private static void PrintMap(List<Robot> robots, int fieldSizeX, int fieldSizeY)
        {
            var field = new int[fieldSizeX, fieldSizeY];
            foreach (var robot in robots)
            {
                var (x, y) = robot.Position;

                field[x, y]++;
            }

            var sb = new StringBuilder();

            for (int y = 0; y < fieldSizeY; y++)
            {
                for (int x = 0; x < fieldSizeX; x++)
                {
                    if (field[x, y] == 0)
                        sb.Append(" ");
                    else
                        sb.Append(field[x, y]);
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }

        private class Robot
        {
            public Vec2i Position { get; set; }

            public Vec2i Velocity { get; set; }

            public override string ToString()
            {
                return $"p=({Position.X}, {Position.Y}), v=({Velocity.X}, {Velocity.Y})";
            }
        }
    }
}
