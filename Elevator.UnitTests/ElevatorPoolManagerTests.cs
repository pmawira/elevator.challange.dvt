using Elevator.Engine.Abstracts;
using Elevator.Engine.Definitions;
using Elevator.Engine.Enums;
using Elevator.Engine.Models;
using Elevator.Engine.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.UnitTests
{
    [TestFixture]
    public class ElevatorPoolManagerTests
    {
        [Test]
        public void CallElevator_UpdatesFloor_AddsPeopleToElevator()
        {
            // Arrange
            //Create service collection
            var service = new ServiceCollection();

            //register services
            service.AddSingleton<IElevatorPoolService, ElevatorPoolService>();
            service.AddScoped<IFloorService, FloorService>();
            service.AddTransient<IElevatorMonitorService, ElevatorMonitorService>();
            service.AddTransient<IElevatorPoolHelperService, ElevatorPoolHelperService>();


            //build service provider
            var serviceProvider = service.BuildServiceProvider();

            var poolService = serviceProvider.GetService<IElevatorPoolService>();
            var floorService = serviceProvider.GetService<IFloorService>();

            var availableElevators = new List<ElevatorCar>
        {
            new ElevatorCar(100) { Number = 1, MaximumWeight = 100, NumberOfPeopleOnBoard = 0, DoorState = DoorState.Open },
            new ElevatorCar(100) { Number = 2, MaximumWeight = 100, NumberOfPeopleOnBoard = 0, DoorState = DoorState.Open }
        };        

            int numberOfPeopleBoarding = 3;
            int floor = 2;

            // Act
            poolService.CallElevator(numberOfPeopleBoarding, floor);

            // Assert
            Assert.AreEqual(3, availableElevators[0].NumberOfPeopleOnBoard);
            Assert.AreEqual(2, availableElevators[0].CurrentFloor.Number);
            Assert.AreEqual(DoorState.Closed, availableElevators[0].DoorState);
        }
    }
}
