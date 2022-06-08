using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Model;
using IressToyRobotCodeChallenge.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace IressToyRobotCodeChallengeTest
{
    [TestClass]
    public class ToyRobotTest
    {
        private Table table;
        private Mock<CommandProcessor> commandProcessor;
        private CommandResult successResult;

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

        [TestInitialize]
        public void Setup()
        {
            table = new Table(new KeyValuePair<int, int>(5, 5));
            commandProcessor = new Mock<CommandProcessor>(new Dictionary<CommandType, ICommand>());
            successResult = new CommandResult(CommandType.REPORT, CommandResultStatus.OK);
                   
            commandProcessor.Setup(x => x.Process(
                It.IsAny<CommandType>(), 
                It.IsAny<string>(),
                It.IsAny<LocationContext>()))
                .Returns(successResult);
        }

        [TestMethod]
        public void ToyRobotIsCreatedSuccessfully()
        {
            var toyRobot = new ToyRobot(table, commandProcessor.Object);
            Assert.AreEqual(table, toyRobot.Table);
            Assert.AreEqual(null, toyRobot.CurrentLocationContext);
        }

        [TestMethod]
        public void WhenEmptyCommandThenToyRobotProcessFails()
        {
            var toyRobot = new ToyRobot(table, commandProcessor.Object);
            var result = toyRobot.Process(string.Empty);

            Assert.AreEqual(CommandResultStatus.FAIL, result.CommandResultStatus);
            Assert.AreEqual("Command should not be null or empty", result.Message);
        }

        [TestMethod]
        public void WhenNullCommandThenToyRobotProcessFails()
        {
            var toyRobot = new ToyRobot(table, commandProcessor.Object);
            var result = toyRobot.Process(null);

            Assert.AreEqual(CommandResultStatus.FAIL, result.CommandResultStatus);
            Assert.AreEqual("Command should not be null or empty", result.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void WhenInvalidCommandTypeThenToyRobotProcessThrows()
        {
            var toyRobot = new ToyRobot(table, commandProcessor.Object);
            toyRobot.Process("INVALID_COMMAND");
        }

        [TestMethod]
        public void ToyRobotProcessesLeftCommandSuccessfully()
        {
            var toyRobot = new ToyRobot(table, commandProcessor.Object);
            var result = toyRobot.Process("LEFT");
            Assert.AreEqual(successResult, result);
        }

        [TestMethod]
        public void ToyRobotProcessesRightCommandSuccessfully()
        {
            var toyRobot = new ToyRobot(table, commandProcessor.Object);
            var result = toyRobot.Process("RIGHT");
            Assert.AreEqual(successResult, result);
        }

        [TestMethod]
        public void ToyRobotProcessesPLACECommandSuccessfully()
        {
            var toyRobot = new ToyRobot(table, commandProcessor.Object);
            var result = toyRobot.Process("PLACE 0,0,SOUTH");
            Assert.AreEqual(successResult, result);
        }

        [TestMethod]
        public void ToyRobotProcessesMoveCommandSuccessfully()
        {
            var toyRobot = new ToyRobot(table, commandProcessor.Object);
            var result = toyRobot.Process("MOVE");
            Assert.AreEqual(successResult, result);
        }

        [TestMethod]
        public void ToyRobotProcessesReportCommandSuccessfully()
        {
            var toyRobot = new ToyRobot(table, commandProcessor.Object);
            var result = toyRobot.Process("REPORT");
            Assert.AreEqual(successResult, result);
        }
    }
}