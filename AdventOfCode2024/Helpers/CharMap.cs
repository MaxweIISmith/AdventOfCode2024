using System.Text;

namespace AdventOfCode2024.Helpers
{
    public class CharMap
    {
        #region Private Fields

        private readonly List<StringBuilder> _map;

        #endregion Private Fields

        #region Public Constructors

        public CharMap(IEnumerable<string> lines)
        {
            _map = new List<StringBuilder>();
            foreach (var line in lines)
            {
                _map.Add(new StringBuilder(line));
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public int SizeX
        {
            get { return _map[0].Length; }
        }

        public int SizeY
        {
            get { return _map.Count; }
        }

        #endregion Public Properties

        #region Public Indexers

        public char this[int x, int y]
        {
            get { return _map[y][x]; }
            set { _map[y][x] = value; }
        }

        public char this[Vec2i v]
        {
            get { return this[v.X, v.Y]; }
            set { this[v.X, v.Y] = value; }
        }

        #endregion Public Indexers

        #region Public Methods

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _map.Select(sb => sb.ToString()));
        }

        #endregion Public Methods
    }
}