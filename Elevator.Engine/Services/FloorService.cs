using Elevator.Engine.Definitions;
using Elevator.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Services
{


    public class FloorService : IFloorService
    {
        private readonly List<Floor> _floors = new List<Floor>();
        public void UpdateFloor(int numberOfPeopleWaiting, int floorNumber)
        {//
            var floor = _floors.FirstOrDefault(f => f.Number == floorNumber);
            if (floor == null) { return; }
            floor.NumberOfPeopleWaiting += numberOfPeopleWaiting;
            var index = _floors.IndexOf(floor);
            _floors[index] = floor;
        }

        public void CreateFloors(int numberOfFloors)
        {
            for (int i = 0; i < numberOfFloors; i++)
            {
                var floor = new Floor()
                {
                    Number = i + 1
                };

                _floors.Add(floor);
            }
            
        }
    }
}
