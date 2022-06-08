using System;
using System.Collections.Generic;
using IressToyRobotCodeChallenge.Command;
using IressToyRobotCodeChallenge.Model;
using IressToyRobotCodeChallenge.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IressToyRobotCodeChallengeTest
{
    [TestClass]
    public class PlaceCommandTest
    {
        private static KeyValuePair<int, int> initialCoordinate = new KeyValuePair<int, int>(0, 0);
        private static ICommand placeCommand = new PlaceCommand(CommandType.PLACE);

        [TestMethod]
        public void PlaceCommandExecuteSuccessfully()
        {
            var newCoordinate = new KeyValuePair<int, int>(3, 2);
            var newDirection = Direction.WEST;

            LocationContext locationContext = new LocationContext(initialCoordinate, Direction.NORTH);
            CommandResult result = placeCommand.Execute(locationContext, 
                String.Format("{0},{1},{2}", newCoordinate.Key, newCoordinate.Value, newDirection));

            Assert.IsTrue(result.CommandResultStatus == CommandResultStatus.OK);
            Assert.IsTrue(result.Message == String.Empty);
            Assert.AreEqual(newCoordinate, result.LocationContext.Coordinate);
            Assert.AreEqual(newDirection, result.LocationContext.Face);
        }

        [TestMethod]
        public void WhenInvalidParametersThenPlaceCommandReportsError()
        {
            String INVALID_PARAM = "I am invalid";
            LocationContext locationContext = new LocationContext(initialCoordinate, Direction.NORTH);
            CommandResult result = placeCommand.Execute(locationContext, INVALID_PARAM);

            Assert.IsTrue(result.CommandResultStatus == CommandResultStatus.FAIL);
            Assert.AreEqual(String.Format(
                "Invalid entry in Place command arguments ({0}). Must be like '0,0,NORTH'",
                INVALID_PARAM), result.Message);
        }
    }
}
