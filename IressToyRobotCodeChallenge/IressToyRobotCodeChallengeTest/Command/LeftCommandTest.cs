using System;
using System.Collections.Generic;
using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Model;
using IressToyRobotCodeChallenge.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IressToyRobotCodeChallengeTest
{
    [TestClass]
    public class LeftCommandTest
    {
        [TestMethod]
        public void LeftCommandExecuteSuccessfully()
        {
            var coordinate = new KeyValuePair<int, int>(0, 0);
            var leftCommand = new LeftCommand(CommandType.LEFT);            
            LocationContext locationContext = new LocationContext(coordinate, Direction.NORTH);
            CommandResult result = leftCommand.Execute(locationContext, "");

            Assert.IsTrue(result.CommandResultStatus == CommandResultStatus.OK);
            Assert.IsTrue(result.Message == String.Empty);
            Assert.AreEqual(coordinate, result.LocationContext.Coordinate);
            Assert.AreEqual(result.LocationContext.Face, Direction.WEST);
        }
    }
}
