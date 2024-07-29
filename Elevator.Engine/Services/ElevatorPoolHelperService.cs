using Elevator.Engine.Abstracts;
using Elevator.Engine.Enums;
using Elevator.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Services
{
    public class ElevatorPoolHelperService : IElevatorPoolHelperService
    {
        public ElevatorCar? GetNearestElevator(List<ElevatorCar> _availableElevators, int requestedFloor)
        {
            // Get all active available elevators, assuming only idle or loading can be reached. excluding faulty one
            var elevators = _availableElevators.Where(e => e.State != ElevatorState.Faulty && (e.State == ElevatorState.Idle || e.State == ElevatorState.Loading) && e.NumberOfPeopleOnBoard < e.MaximumWeight).ToList();

            if (elevators.Count == 0)
            {
                Console.WriteLine("All elevators are currently busy. Please try again later.");
                return null;
            }
            //ToDo:
            // Check if all the elevator are on the floor 1. if yes bring the first one


            //ToDO:
            // check if they are all on the top most floor, if yes bring the any of them


            ElevatorCar? nearestElevator = null;

            int shortestDistance = int.MaxValue;

            //check for the nearest available elevator;
            foreach (ElevatorCar elevator in elevators)
            {
                // check if there is any elevator at requestedFloor

                if (elevator.CurrentFloor.Number == requestedFloor)
                {
                    nearestElevator = elevator;
                    nearestElevator.State = ElevatorState.Loading;
                    break;
                }

                var distance = Math.Abs(elevator.CurrentFloor.Number - requestedFloor);
                if (distance < shortestDistance)
                {
                    nearestElevator = elevator;
                    shortestDistance = distance;
                }

            }
            return nearestElevator;
        }
        public List<ElevatorCar> MoveElevator(List<ElevatorCar> _availableElevators, int targetFloor, ElevatorCar elevator)
        {

            var movedToElevator = _availableElevators.FirstOrDefault(e => e.Number == elevator.Number);  
            
            if(movedToElevator == null) { return _availableElevators; }

            var index = _availableElevators.IndexOf(movedToElevator);

     
            //Move the elevator
            if (targetFloor < elevator.CurrentFloor.Number)
            {
                elevator.Direction = Direction.Down;
            }
            else
            {
                elevator.Direction = Direction.Up;
            }

            elevator.State = ElevatorState.Moving;

            _availableElevators[index] = elevator;


            return _availableElevators;
        }


    }
}
