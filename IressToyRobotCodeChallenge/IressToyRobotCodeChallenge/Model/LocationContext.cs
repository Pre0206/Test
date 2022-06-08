using System.Collections.Generic;

namespace IressToyRobotCodeChallenge.Model
{
    public enum Direction { NORTH = 0, SOUTH = 180, EAST = 90, WEST = 270 };

    public class LocationContext
    {
        public KeyValuePair<int, int> Coordinate { get; private set; }
        public Direction Face { get; private set; }

        LocationContext(): this(new KeyValuePair<int, int>(0, 0)) {
        }

        public LocationContext(KeyValuePair<int, int> coordinate, Direction direction = Direction.NORTH)
        {
            Coordinate = coordinate;
            Face = direction;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", Coordinate.Key, Coordinate.Value, Face);
        }
    }
}
