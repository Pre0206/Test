using IressToyRobotCodeChallenge.Model;
using System;

namespace IressToyRobotCodeChallenge.Command
{
    public enum CommandResultStatus
    {
        OK,
        FAIL,
    }

    public sealed class CommandResult
    {
        public CommandType CommandType { get; private set; }
        public CommandResultStatus CommandResultStatus { get; private set; }
        public LocationContext LocationContext { get; private set; }
        public String Message { get; private set; }


        public CommandResult(CommandType commandType, 
            CommandResultStatus status, 
            LocationContext locationContext = null, 
            String message = "")
        {
            this.CommandType = commandType;
            this.CommandResultStatus = status;
            this.LocationContext = locationContext;
            this.Message = message;
        }

        public bool IsSuccess()
        {
             return this.CommandResultStatus == CommandResultStatus.OK;
        }
    }
}
