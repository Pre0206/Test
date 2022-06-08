using IressToyRobotCodeChallenge.Model;

namespace IressToyRobotCodeChallenge.Command
{
    public class LeftCommand : ICommand
    {
        public CommandType CommandType
        {
            get;
            private set;
        }

        public LeftCommand(CommandType commandType)
        {
            this.CommandType = commandType;
        }

        public CommandResult Execute(LocationContext currentLocationContext, string parameters)
        {
            int currentAngle = (currentLocationContext.Face == Direction.NORTH) ? 360 : (int)currentLocationContext.Face;
            Direction newDirection = (Direction)currentAngle - Constants.AngleToMove;
            return new CommandResult(CommandType,
                CommandResultStatus.OK, 
                locationContext: new LocationContext(currentLocationContext.Coordinate, newDirection));
        }
    }
}
