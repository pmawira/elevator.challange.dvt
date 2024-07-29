using Elevator.Engine;
using Elevator.Engine.Abstracts;
using Elevator.Engine.Definitions;
using Elevator.Engine.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Elevator.Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Configure DI
            //Create service collection
            var service = new ServiceCollection();

            //register services
            service.AddSingleton<IElevatorPoolService, ElevatorPoolService>();
            service.AddScoped<IFloorService, FloorService>();
            service.AddTransient<IElevatorMonitorService, ElevatorMonitorService>();
            service.AddTransient<IElevatorPoolHelperService, ElevatorPoolHelperService>();


            //build service provider
            var serviceProvider = service.BuildServiceProvider();

            var poolService = serviceProvider.GetService<IElevatorPoolService>();
            var floorService = serviceProvider.GetService<IFloorService>();
            var monitorService = serviceProvider?.GetService<IElevatorMonitorService>();

            int numberOfElevators = 0;
            int numberOfFloors = 0;
            //number of people an elevator can transport.
            int maximumCapacity = 0;
            string? operationOption = string.Empty;

            Console.WriteLine("Press 'Enter' to continue and 'E' to exit");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            while (true)
            {

                if (keyInfo.Key == ConsoleKey.E)
                {
                    break; // Exit the loop and terminate the application
                }

                Console.WriteLine("Enter the number of Elevators:");
                int.TryParse(Console.ReadLine(), out numberOfElevators);
                Console.WriteLine("Please enter the number of floors:");
                int.TryParse(Console.ReadLine(), out numberOfFloors);
                Console.WriteLine("Please Enter maximum capacity");
                int.TryParse(Console.ReadLine(), out maximumCapacity);

                //Create elevator pool
                poolService?.CreateElevatorPool(numberOfElevators, maximumCapacity);

                //Create floors
                floorService?.CreateFloors(numberOfFloors);

                while (true)
                {

                    Console.WriteLine("Select operation in the options:");
                    Console.WriteLine("1: Call elevator");
                    Console.WriteLine("2: Remove people");
                    Console.WriteLine("3: Show Elevator status");


                    operationOption = Console.ReadLine();

                    if (operationOption == "1")
                    {
                        Console.WriteLine("Enter requested floor and number of people waiting, separated by comma respectively");
                    }
                    else if (operationOption == "2")
                    {
                        Console.WriteLine("Enter requested floor, number of people waiting and elevator number, separated by comma respectively");
                    }
                    else if (operationOption == "3")
                    {
                        poolService?.Logs();
                        continue;
                    }

                    var inputString = Console.ReadLine();
                    //remove white space
                    inputString = inputString.Replace(" ", "");

                    var inputs = inputString?.Split(",").ToArray();

                    if (!inputs.Any() || inputs.Length != 2)
                    {
                        Console.WriteLine(" Wrong entry, try again");
                        continue;
                    }

                    if (operationOption == "1")
                    {

                        //Calls the nearest available elevator, load and transport people
                        poolService?.CallElevator(int.Parse(inputs[1]), int.Parse(inputs[0]));
                        //Transport
                        Console.WriteLine("Enter destination floor");
                        poolService?.MoveElevatorToDestination(int.Parse(Console.ReadLine()));
                        continue;

                    }
                    else if (operationOption == "2")
                    {
                        //Remove people
                        poolService?.RemovePeople(int.Parse(inputs[1]), int.Parse(inputs[0]), int.Parse(inputs[2]));
                        continue;

                    }

                    Console.WriteLine("Wrong entry, press Enter to try again or 'E' to exit");

                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.E) { break; }


                }
                break;
            }
        }
    }
}