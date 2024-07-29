using Elevator.Engine.Enums;
using Elevator.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Abstracts
{
    public interface IElevatorPoolHelperService
    {
        ElevatorCar? GetNearestElevator(List<ElevatorCar> _availableElevators, int requestedFloor);
        List<ElevatorCar> MoveElevator(List<ElevatorCar> _availableElevators, int targetFloor, ElevatorCar elevator);
    }
}
