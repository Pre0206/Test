using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Processor;
using System;

namespace IressToyRobotCodeChallenge.Model
{
    public class ToyRobot
    {
        public LocationContext CurrentLocationContext { get; private set; }
        private readonly CommandProcessor CommandProcessor;
        public Table Table { get; private set; }

        public ToyRobot(Table table, CommandProcessor commandProcessor)
        {
            this.CurrentLocationContext = null;
            this.Table = table;
            this.CommandProcessor = commandProcessor;
        }

        public CommandResult Process(String command)
        {
            CommandType commandType = CommandType.INVALID;
            String otherParameters = "";

            if (string.IsNullOrEmpty(command))
            {
                return new CommandResult(commandType,
                    CommandResultStatus.FAIL,
                    message: "Command should not be null or empty");
            }

            GetCommandAndParameters(command, ref commandType, ref otherParameters);

            CommandResult result = CommandProcessor.Process(commandType, otherParameters, CurrentLocationContext);

            if (result.IsSuccess() && result.LocationContext != null)
            {
                if (Table.AmIWithinTableArea(result.LocationContext.Coordinate))
                {
                    CurrentLocationContext = result.LocationContext;
                }
            }

            return result;
        }

        private static void GetCommandAndParameters(String command, ref CommandType commandType, ref String otherParameters)
        {
            String cmd = command;

            int indexOfSeparator = command.IndexOf(" ");
            if (indexOfSeparator != -1)
            {
                cmd = command.Substring(0, indexOfSeparator + 1);
                otherParameters = command.Substring(indexOfSeparator + 1);
            } 

            if (!Enum.TryParse(cmd, out commandType))
            {
                throw new ArgumentException("Failed to map command type for value - " + cmd);
            }
        }
    }
}