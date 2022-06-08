using IressToyRobotCodeChallenge.Model;
using System;

namespace IressToyRobotCodeChallenge.Command
{
    public enum CommandType
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT,

        INVALID
    }

    public interface ICommand
    {
        CommandType CommandType { get; }
        CommandResult Execute(LocationContext currentLocationContext, String parameters);
    }
}
