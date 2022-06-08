using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Model;
using IressToyRobotCodeChallenge.Processor;
using System;
using System.Collections.Generic;
using CommandType = IressToyRobotCodeChallenge.Command.CommandType;

namespace IressToyRobotCodeChallenge
{
    public class MainApp
    {
        private const String EXIT_CODE = "-1";
        private static KeyValuePair<int, int> TABLE_TOP_RIGHT = new KeyValuePair<int, int>(5, 5);
        private static readonly String nextCommand = String.Format("Enter next command, {0} to exit", EXIT_CODE);
        private static readonly CommandProcessor commandProcessor = GetCommandProcessor();

        private static CommandProcessor GetCommandProcessor()
        {
            var commandTypeVsCommand = new Dictionary<CommandType, ICommand>();
            commandTypeVsCommand[CommandType.PLACE] = new PlaceCommand(CommandType.PLACE);
            commandTypeVsCommand[CommandType.MOVE] = new MoveCommand(CommandType.MOVE);
            commandTypeVsCommand[CommandType.LEFT] = new LeftCommand(CommandType.LEFT);
            commandTypeVsCommand[CommandType.RIGHT] = new RightCommand(CommandType.RIGHT);
            commandTypeVsCommand[CommandType.REPORT] = new ReportCommand(CommandType.REPORT);
            return new CommandProcessor(commandTypeVsCommand);               
        }

        public static void Main(string[] args)
        {
            var toyRobot = new ToyRobot(new Table(TABLE_TOP_RIGHT), commandProcessor);

            Console.WriteLine(nextCommand);    
            String command = Console.ReadLine();
            while (command != EXIT_CODE)
            {
                var result = toyRobot.Process(command);
                if (result.CommandType == CommandType.REPORT)
                {
                    //string message = result.IsSuccess() ?
                    //    String.Format("Output: {0}", result.LocationContext.ToString()) : result.Message;
                    Console.WriteLine(result.Message);
                }
                
                Console.WriteLine(nextCommand);
                command = Console.ReadLine();
            }

            Console.WriteLine("Enter any key to exit");
            Console.ReadLine();
        }
    }
}
