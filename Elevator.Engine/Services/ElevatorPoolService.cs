using Elevator.Engine.Abstracts;
using Elevator.Engine.Definitions;
using Elevator.Engine.Enums;
using Elevator.Engine.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Services
{
    public class ElevatorPoolService : IElevatorPoolService
    {
        private List<ElevatorCar> _availableElevators = new List<ElevatorCar>();

        private ElevatorCar? elevatorCar;

        private readonly IElevatorPoolHelperService _helperService;
        private readonly IFloorService _floorService;

        public ElevatorPoolService(IElevatorPoolHelperService helperService, IFloorService floorService)
        {
            _helperService = helperService;
            _floorService = floorService;

        }
        /// <summary>
        /// Create elevator pool
        /// </summary>
        /// <param name="_numberOfElevators"></param>
        /// <param name="_maximumCapacity"></param>
        public void CreateElevatorPool(int _numberOfElevators, int _maximumCapacity)
        {
            var elevators = new List<ElevatorCar>();

            for (int i = 0; i < _numberOfElevators; i++)
            {
                elevators.Add(new ElevatorCar(_maximumCapacity)
                { Number = i + 1 });
            }

            _availableElevators = elevators;
            return;

        }
        public void CallElevator(int numberOfPeopleBoarding, int floor)
        {
            //add people on the floor
            _floorService.UpdateFloor(numberOfPeopleBoarding, floor);

            var nearestElevator = _helperService.GetNearestElevator(_availableElevators, floor);

            if (nearestElevator == null)
            {
                Console.WriteLine("All elevators are busy try again");
                return;
                
            }
            //Move elevator to the requested floor
            _availableElevators = _helperService.MoveElevator(_availableElevators, floor, nearestElevator);


            if (nearestElevator.NumberOfPeopleOnBoard + numberOfPeopleBoarding > nearestElevator.MaximumWeight)
            {
                nearestElevator.NumberOfPeopleOnBoard = nearestElevator.MaximumWeight;
                _floorService.UpdateFloor(floor, (nearestElevator.NumberOfPeopleOnBoard + numberOfPeopleBoarding) - nearestElevator.NumberOfPeopleOnBoard);
            }

            nearestElevator.NumberOfPeopleOnBoard += numberOfPeopleBoarding;
            nearestElevator.CurrentFloor.Number = floor;
            nearestElevator.DoorState = DoorState.Closed;
            

            elevatorCar = nearestElevator;


            Console.WriteLine($"{numberOfPeopleBoarding} people boarded the elevator {nearestElevator.Number}. Current number of people: {nearestElevator.NumberOfPeopleOnBoard}.");

        }
        /// <summary>
        /// Move a loaded elevator to destination floor
        /// </summary>
        /// <param name="floor"></param>
        public void MoveElevatorToDestination(int floor)
        {
            if(elevatorCar == null)
            {
                return;
            }

            _availableElevators = _helperService.MoveElevator(_availableElevators, floor, elevatorCar);
        }
        public void RemovePeople(int numberOfPeopleToRemove, int floor, int elevatorNumber)
        {
            var elevator = _availableElevators.FirstOrDefault(e => e.Number == elevatorNumber);

            if (elevator == null)
            {
                Console.WriteLine($"The elevator {elevatorNumber} is not in the system, try another one");
                return;
            }
            if (floor != elevator.CurrentFloor.Number)
            {
                Console.WriteLine($"Elevator {elevator.Number} is in motion, cannot remove people");
                return;
            }

            if (elevator.NumberOfPeopleOnBoard - numberOfPeopleToRemove < 0)
            {
                Console.WriteLine("Elevator has no people.");
                return;
            }

            elevator.NumberOfPeopleOnBoard -= numberOfPeopleToRemove;

            Console.WriteLine($"{numberOfPeopleToRemove} people alighted from the elevator. Current number of people: {elevator.NumberOfPeopleOnBoard}.");
        }
        public void Log(ElevatorCar elevator)
        {
            Console.WriteLine($"Current floor: {elevator.CurrentFloor}");
            Console.WriteLine($"State: {elevator.State}");
            Console.WriteLine($"Current direction: {elevator.Direction}");
            Console.WriteLine($"Number of people: {elevator.NumberOfPeopleOnBoard}");
            Console.WriteLine();
        }
        /// <summary>
        /// get elevator status
        /// </summary>
        public void Logs()
        {
            for (int i = 0; i < _availableElevators.Count; i++)
            {
                ElevatorCar elevator = _availableElevators[i];
                Console.WriteLine($"Elevator {i + 1}:");
                Console.WriteLine($"Current floor: {elevator.CurrentFloor}");
                Console.WriteLine($"State: {elevator.State}");
                Console.WriteLine($"Current direction: {elevator.Direction}");
                Console.WriteLine($"Number of people: {elevator.NumberOfPeopleOnBoard}");
                Console.WriteLine();

            }
        }

    }
}
