using System;
using System.Collections.Generic;
using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Model;
using IressToyRobotCodeChallenge.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IressToyRobotCodeChallengeTest
{
    [TestClass]
    public class MoveCommandTest
    {
        [TestMethod]
        public void MoveCommandExecuteSuccessfully()
        {
            var moveCommand = new LeftCommand(CommandType.MOVE);
            LocationContext locationContext = new LocationContext(new KeyValuePair<int, int>(0, 0), Direction.NORTH);
            CommandResult result = moveCommand.Execute(locationContext, "");

            Assert.IsTrue(result.CommandResultStatus == CommandResultStatus.OK);
            Assert.IsTrue(result.Message == String.Empty);
            Assert.AreEqual(result.LocationContext.Coordinate, new KeyValuePair<int, int>(0, 0));
            Assert.AreEqual(result.LocationContext.Face, Direction.WEST);
        }
    }
}
