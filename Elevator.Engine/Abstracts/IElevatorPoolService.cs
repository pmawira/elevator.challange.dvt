using Elevator.Engine.Models;

namespace Elevator.Engine.Definitions
{
    public interface IElevatorPoolService
    {
        void CreateElevatorPool(int numberOfElevators, int maximumCapacity);

        void CallElevator(int numberOfPeopleBoarding, int floor);
        void MoveElevatorToDestination(int targetFloor);
        void RemovePeople(int numberOfPeopleToRemove, int floor, int elevatorNumber);        
        void Logs();

    }
}