using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Enums
{
    public enum ElevatorState
    {
        /// <summary>
        /// Default state
        /// </summary>
        Idle,
        Moving,
        Stopped,
        Faulty,
        Loading,
        Closed,
        Open
    }
}
