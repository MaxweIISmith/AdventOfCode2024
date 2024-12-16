using System.Text.RegularExpressions;
using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public static class Problem14Part1
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

            var time = 100;

            var fieldSizeX = 101;
            var fieldSizeY = 103;
            var middleX = fieldSizeX / 2;
            var middleY = fieldSizeY / 2;

            var sector1 = 0;
            var sector2 = 0;
            var sector3 = 0;
            var sector4 = 0;

            foreach (var robot in robots)
            {
                var (x, y) = robot.Position + time * robot.Velocity;
                x = (x % fieldSizeX + fieldSizeX) % fieldSizeX;
                y = (y % fieldSizeY + fieldSizeY) % fieldSizeY;

                if (x > middleX)
                {
                    if (y > middleY)
                        sector1++;
                    else if (y < middleY)
                        sector2++;
                }
                else if (x < middleX)
                {
                    if (y > middleY)
                        sector4++;
                    else if (y < middleY)
                        sector3++;
                }
            }

            Console.WriteLine(sector1 * sector2 * sector3 * sector4);
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