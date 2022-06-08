using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Model;
using System;
using System.Collections.Generic;

namespace IressToyRobotCodeChallenge.Processor
{
    public class CommandProcessor
    {
        private readonly IDictionary<CommandType, ICommand> commandTypeVsCommand;

        public CommandProcessor(IDictionary<CommandType, ICommand> commandTypeVsCommand)
        {
            this.commandTypeVsCommand = new Dictionary<CommandType, ICommand>(commandTypeVsCommand);
        }

        public virtual CommandResult Process(CommandType commandType, String otherParameters, LocationContext locationContext)
        {
            if (!commandTypeVsCommand.ContainsKey(commandType))
            {
                throw new NotImplementedException(string.Format("Supplied command Type [{0}] is not supported", commandType));
            }

            // Exclude all commands until a valid PLACE command is executed
            if (locationContext == null && commandType != CommandType.PLACE)
            {
                return new CommandResult(commandType,
                    CommandResultStatus.FAIL,
                    message: "Skipping command until a valid PLACE command is executed");
            }

            var command = commandTypeVsCommand[commandType];
            return command.Execute(locationContext, otherParameters);
        }
    }
}
