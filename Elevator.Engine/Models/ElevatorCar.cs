using Elevator.Engine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Models
{


    public class ElevatorCar
    {
        public int NumberOfPeopleOnBoard { get;  set; }
        public Floor CurrentFloor { get; set; }

        /// <summary>
        /// Maximum weight an elevator can carry, expressed as number of people
        /// </summary>
        public int MaximumWeight { get;  set; }
        public ElevatorState State { get; set; }
        public Direction Direction { get; set; }
        public int Number { get; set; }
        public DoorState DoorState { get; set; }

        /// <summary>
        /// maximumWeight expressed as number of people individual elevator can carry, which is uniform for all elevators
        /// </summary>
        /// <param name="maximumCapacity"></param>      
        public ElevatorCar(int maximumCapacity)
        {
            // Assuming elevator default floor is 1
            CurrentFloor = new Floor();
            State = ElevatorState.Idle;//assuming elevator default state is idle
            Direction = Direction.Up;//Assume floor 1 is the first
            NumberOfPeopleOnBoard = 0; //assuming elevator default number of people is 0
            MaximumWeight = maximumCapacity;
            DoorState = DoorState.Closed;
        }       
    }
}
