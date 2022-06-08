using System;
using System.Collections.Generic;
using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Model;
using IressToyRobotCodeChallenge.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IressToyRobotCodeChallengeTest
{
    [TestClass]
    public class RightCommandTest
    {
        [TestMethod]
        public void RightCommandExecuteSuccessfully()
        {
            var coordinate = new KeyValuePair<int, int>(0, 0);
            var rightCommand = new RightCommand(CommandType.RIGHT);
            LocationContext locationContext = new LocationContext(coordinate, Direction.NORTH);
            CommandResult result = rightCommand.Execute(locationContext, "");

            Assert.IsTrue(result.CommandResultStatus == CommandResultStatus.OK);
            Assert.IsTrue(result.Message == String.Empty);
            Assert.AreEqual(coordinate, result.LocationContext.Coordinate);
            Assert.AreEqual(result.LocationContext.Face, Direction.EAST);
        }
    }
}
