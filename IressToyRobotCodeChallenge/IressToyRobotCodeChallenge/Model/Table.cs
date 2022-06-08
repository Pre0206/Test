using System.Collections.Generic;

namespace IressToyRobotCodeChallenge.Model
{
    public sealed class Table
    {
        public KeyValuePair<int, int> BottomLeft { get; private set; }
        public KeyValuePair<int, int> TopRight { get; private set; }


        public Table(KeyValuePair<int, int> topRight)
        {
            TopRight = topRight;
            BottomLeft = new KeyValuePair<int, int>(0, 0);
        }

        public Table(KeyValuePair<int, int> bottomLeft, KeyValuePair<int, int> topRight)
        {
            BottomLeft = bottomLeft;
            TopRight = topRight;
        }

        public bool AmIWithinTableArea(KeyValuePair<int, int> coordinate)
        {
            return BottomLeft.Key <= coordinate.Key && coordinate.Key <= TopRight.Key &&
                 BottomLeft.Value <= coordinate.Value && coordinate.Value <= TopRight.Value;
        }

        public override string ToString()
        {
            return string.Format("[{0},{1}] [{2},{3}]", BottomLeft.Key, BottomLeft.Value, TopRight.Key, TopRight.Value);
        }
    }
}
