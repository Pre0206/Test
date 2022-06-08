using IressToyRobotCodeChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IressToyRobotCodeChallenge.Command
{
    public class PlaceCommand : ICommand
    {
        public LocationContext LocationContext { get; private set; }

        public CommandType CommandType
        {
            get;
            private set;
        }

        public PlaceCommand(CommandType commandType)
        {
            this.CommandType = commandType;
        }

        private static bool TryExtractRequiredContext(String parameters, out LocationContext locationContext)
        {
            var items = parameters.Split(',').Select(v => v.Trim()).ToList();
            int x;
            int y;
            Direction direction;
            if ((items.Count() == 3) && 
                (int.TryParse(items[0], out x)) && 
                (int.TryParse(items[1], out y)) &&
                Enum.TryParse(items[2], out direction))
            {
                KeyValuePair<int, int> coordinate = new KeyValuePair<int, int>(x, y);
                locationContext = new LocationContext(coordinate, direction);

                return true;
            }

            locationContext = null;
            return false;
        }

        public CommandResult Execute(LocationContext currentLocationContext, string parameters)
        {
            LocationContext requiredContext;
            if (!TryExtractRequiredContext(parameters, out requiredContext))
            {
                return new CommandResult(CommandType, CommandResultStatus.FAIL, message: String.Format(
                "Invalid entry in Place command arguments ({0}). Must be like '0,0,NORTH'",
                parameters));
            }

            return new CommandResult(CommandType, CommandResultStatus.OK, locationContext: requiredContext);
        }
    }
}
