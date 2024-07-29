using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Models
{
    public class Floor
    {
        public int NumberOfPeopleWaiting { get; set; }
        public int Number { get; set; }
        public Floor()
        {
            NumberOfPeopleWaiting = 0;
        }
    }
}
