using Elevator.Engine.Models;

namespace Elevator.Engine.Definitions
{
    public interface IFloorService
    {
        void UpdateFloor(int count, int floor);
        void CreateFloors(int floors);
        
    }
}