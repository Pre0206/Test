using IressToyRobotCodeChallenge.Model;

namespace IressToyRobotCodeChallenge.Command
{
    public class ReportCommand : ICommand
    {
        public CommandType CommandType
        {
            get;
            private set;
        }

        public ReportCommand(CommandType commandType)
        {
            this.CommandType = commandType;
        }

        public CommandResult Execute(LocationContext currentLocationContext, string parameters)
        {
            return new CommandResult(CommandType, 
                CommandResultStatus.OK, 
                currentLocationContext,
                string.Format("Output: {0}", currentLocationContext.ToString()));
        }
    }
}
