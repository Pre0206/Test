using IressToyRobotCodeChallenge.Model;

namespace IressToyRobotCodeChallenge.Command
{
    public class RightCommand : ICommand
    {
        public CommandType CommandType
        {
            get;
            private set;
        }

        public RightCommand(CommandType commandType)
        {
            this.CommandType = commandType;
        }

        public CommandResult Execute(LocationContext currentLocationContext, string parameters)
        {
            int newAngle = (int)currentLocationContext.Face + Constants.AngleToMove;

            Direction newDirection = (newAngle == 360) ? Direction.NORTH : (Direction)newAngle;

            return new CommandResult(CommandType, 
                CommandResultStatus.OK,
                locationContext: new LocationContext(currentLocationContext.Coordinate, newDirection));
        }
    }
}
