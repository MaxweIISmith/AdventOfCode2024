using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public static class Problem12Part1
    {
        #region Public Methods

        public static void Solve()
        {
            var plantMap = new PlantMap(File.ReadAllLines("input.txt"));

            var flags = new bool[plantMap.MaxX, plantMap.MaxY];

            int result = 0;

            for (int x = 0; x < plantMap.MaxX; x++)
            {
                for (int y = 0; y < plantMap.MaxY; y++)
                {
                    if (flags[x, y])
                        continue;

                    var regionCoordinates = new List<Vec2i>();

                    var plantType = plantMap[x, y];

                    FindPlantCoords(plantMap, plantType, x, y, regionCoordinates);

                    foreach (var coord in regionCoordinates)
                    {
                        flags[coord.X, coord.Y] = true;
                    }

                    var perimeter = CalcPerimeter(regionCoordinates);

                    var area = regionCoordinates.Count;

                    result += perimeter * area;
                }
            }

            Console.WriteLine(result);
        }

        #endregion Public Methods

        #region Private Methods

        private static int CalcPerimeter(List<Vec2i> coords)
        {
            int p = 0;

            var minX = coords.Min(v => v.X);
            var maxX = coords.Max(v => v.X);

            for (int x = minX; x <= maxX; x++)
            {
                var xCoords = coords.Where(v => v.X == x).OrderBy(v => v.Y).ToList();

                p += 2;

                for (int i = 0; i < xCoords.Count - 1; i++)
                {
                    if (xCoords[i + 1].Y - xCoords[i].Y > 1)
                        p += 2;
                }
            }

            var minY = coords.Min(v => v.Y);
            var maxY = coords.Max(v => v.Y);

            for (int y = minY; y <= maxY; y++)
            {
                var yCoords = coords.Where(v => v.Y == y).OrderBy(v => v.X).ToList();

                p += 2;

                for (int i = 0; i < yCoords.Count - 1; i++)
                {
                    if (yCoords[i + 1].X - yCoords[i].X > 1)
                        p += 2;
                }
            }

            return p;
        }

        private static void FindPlantCoords(PlantMap plantMap, char plantType, int x, int y, List<Vec2i> coordsList)
        {
            if (x < 0 || x >= plantMap.MaxX)
                return;

            if (y < 0 || y >= plantMap.MaxY)
                return;

            if (plantMap[x, y] != plantType)
                return;

            var coords = new Vec2i(x, y);

            if (coordsList.Contains(coords))
                return;

            coordsList.Add(new Vec2i(x, y));

            FindPlantCoords(plantMap, plantType, x - 1, y, coordsList);
            FindPlantCoords(plantMap, plantType, x + 1, y, coordsList);
            FindPlantCoords(plantMap, plantType, x, y - 1, coordsList);
            FindPlantCoords(plantMap, plantType, x, y + 1, coordsList);
        }

        #endregion Private Methods

        #region Private Classes

        private class PlantMap
        {
            #region Private Fields

            private List<string> _map;

            #endregion Private Fields

            #region Public Constructors

            public PlantMap(IEnumerable<string> mapLines)
            {
                _map = new List<string>();
                _map.AddRange(mapLines);
            }

            #endregion Public Constructors

            #region Public Properties

            public int MaxX
            {
                get { return _map[0].Length; }
            }

            public int MaxY
            {
                get { return _map.Count; }
            }

            #endregion Public Properties

            #region Public Indexers

            public char this[int x, int y]
            {
                get { return _map[y][x]; }
            }

            #endregion Public Indexers
        }

        #endregion Private Classes
    }
}