using Elevator.Engine.Models;

namespace Elevator.Engine.Definitions
{
    public interface IElevatorMonitorService
    {
        void Log(ElevatorCar elevator);
        void LogAll();
    }
}