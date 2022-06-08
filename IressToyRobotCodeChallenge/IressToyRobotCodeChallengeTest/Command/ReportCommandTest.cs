using System;
using System.Collections.Generic;
using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Model;
using IressToyRobotCodeChallenge.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IressToyRobotCodeChallengeTest
{
    [TestClass]
    public class ReportCommandTest
    {
        [TestMethod]
        public void ReportCommandExecuteSuccessfully()
        {
            var coordinate = new KeyValuePair<int, int>(0, 0);
            var face = Direction.NORTH;
            var reportCommand = new ReportCommand(CommandType.REPORT);

            LocationContext locationContext = new LocationContext(coordinate, face);
            CommandResult result = reportCommand.Execute(locationContext,String.Empty);

            Assert.IsTrue(result.CommandResultStatus == CommandResultStatus.OK);
            Assert.AreEqual(coordinate, result.LocationContext.Coordinate);
            Assert.AreEqual(face, result.LocationContext.Face);
            Assert.AreEqual(string.Format("Output: {0}", result.LocationContext.ToString()), result.Message);
        }
    }
}
