using IressToyRobotCodeChallenge.Model;
using System.Collections.Generic;

namespace IressToyRobotCodeChallenge.Command
{
    public class MoveCommand : ICommand
    {
        public CommandType CommandType
        {
            get;
            private set;
        }

        public MoveCommand(CommandType commandType)
        {
            this.CommandType = commandType;
        }

        public CommandResult Execute(LocationContext currentLocationContext, string parameters)
        {
            KeyValuePair<int, int> coordinate = new KeyValuePair<int, int>();
            switch (currentLocationContext.Face)
            {
                case Direction.NORTH:
                    coordinate = new KeyValuePair<int, int>(currentLocationContext.Coordinate.Key, currentLocationContext.Coordinate.Value + Constants.GridPointsToMove);
                    break;
                case Direction.WEST:
                    coordinate = new KeyValuePair<int, int>(currentLocationContext.Coordinate.Key - Constants.GridPointsToMove, currentLocationContext.Coordinate.Value);
                    break;
                case Direction.SOUTH:
                    coordinate = new KeyValuePair<int, int>(currentLocationContext.Coordinate.Key, currentLocationContext.Coordinate.Value - Constants.GridPointsToMove);
                    break;
                case Direction.EAST:
                    coordinate = new KeyValuePair<int, int>(currentLocationContext.Coordinate.Key + Constants.GridPointsToMove, currentLocationContext.Coordinate.Value);
                    break;
                default:
                    return new CommandResult(
                        CommandType, 
                        CommandResultStatus.FAIL, 
                        message: string.Format("Unrecognised enum value for Direction enum - {0}", currentLocationContext.Face));
            }

            return new CommandResult(CommandType, CommandResultStatus.OK, locationContext: new LocationContext(coordinate, currentLocationContext.Face));
        }
    }
}
