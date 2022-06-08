using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Model;
using IressToyRobotCodeChallenge.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IressToyRobotCodeChallengeTest
{
    [TestClass]
    public class EndToEndTest
    {
        private static Table table = new Table(new KeyValuePair<int, int>(5, 5));
        private static CommandProcessor commandProcessor = GetCommandProcessor();

        private static CommandProcessor GetCommandProcessor()
        {
            var commandTypeVsCommand = new Dictionary<CommandType, ICommand>();
            commandTypeVsCommand[CommandType.PLACE] = new PlaceCommand(CommandType.PLACE);
            commandTypeVsCommand[CommandType.LEFT] = new LeftCommand(CommandType.LEFT);
            commandTypeVsCommand[CommandType.RIGHT] = new RightCommand(CommandType.RIGHT);
            commandTypeVsCommand[CommandType.MOVE] = new MoveCommand(CommandType.MOVE);
            commandTypeVsCommand[CommandType.REPORT] = new ReportCommand(CommandType.REPORT);
            return new CommandProcessor(commandTypeVsCommand);
        }

        [TestMethod]
        public void SingleMoveInstructionProcessedSuccessfully()
        {
            var toyRobot = new ToyRobot(table, commandProcessor);

            CommandResult result = toyRobot.Process("PLACE 0,0,NORTH");
            result = toyRobot.Process("MOVE");
            result = toyRobot.Process("REPORT");

            Assert.AreEqual(result.Message, "Output: 0,1,NORTH");
        }

        [TestMethod]
        public void SingleLeftInstructionLeftProcessedSuccessfully()
        {
            var toyRobot = new ToyRobot(table, commandProcessor);

            CommandResult result = toyRobot.Process("PLACE 0,0,NORTH");
            result = toyRobot.Process("LEFT");
            result = toyRobot.Process("REPORT");

            Assert.AreEqual(result.Message, "Output: 0,0,WEST");
        }

        [TestMethod]
        public void MultipleInstructionsProcessedSuccessfully()
        {
            var toyRobot = new ToyRobot(table, commandProcessor);

            CommandResult result = toyRobot.Process("PLACE 1,2,EAST");
            result = toyRobot.Process("MOVE");
            result = toyRobot.Process("MOVE");
            result = toyRobot.Process("LEFT");
            result = toyRobot.Process("MOVE");
            result = toyRobot.Process("REPORT");

            Assert.AreEqual(result.Message, "Output: 3,3,NORTH");
        }

        [TestMethod]
        public void AllCommandsAreDiscardedUntilValidPlaceCommandIsMissing()
        {
            var toyRobot = new ToyRobot(table, commandProcessor);

            CommandResult result = toyRobot.Process("MOVE");
            result = toyRobot.Process("MOVE");
            result = toyRobot.Process("LEFT");
            result = toyRobot.Process("MOVE");
            result = toyRobot.Process("REPORT");
            Assert.AreEqual(result.Message, "Skipping command until a valid PLACE command is executed");

            // Adding PLACE command so now subsequent commands will be processed
            result = toyRobot.Process("PLACE 0,0,NORTH");
            result = toyRobot.Process("MOVE");
            result = toyRobot.Process("REPORT");

            Assert.AreEqual(result.Message, "Output: 0,1,NORTH");
        }

        [TestMethod]
        public void AnyMoveCommandThatDropsRobotArePreventedButOthersAreAllowed()
        {
            var toyRobot = new ToyRobot(table, commandProcessor);

            // Adding PLACE command to push robot off table is prevented
            CommandResult result = toyRobot.Process("PLACE 5,5,NORTH");
            result = toyRobot.Process("MOVE");
            result = toyRobot.Process("REPORT");

            Assert.AreEqual(result.Message, "Output: 5,5,NORTH");

            // Allow other valid command(s)
            result = toyRobot.Process("LEFT");
            result = toyRobot.Process("LEFT");
            result = toyRobot.Process("REPORT");
            Assert.AreEqual(result.Message, "Output: 5,5,SOUTH");
        }
    }
}
