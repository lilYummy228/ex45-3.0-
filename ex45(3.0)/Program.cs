using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex45_3._0_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RailwayStation railwayStation = new RailwayStation();
            Train train = new Train();

            while (true)
            {
                Console.Write("Ваш ввод: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        railwayStation.CreateRoute();
                        railwayStation.CreatePassengersCount();
                        train.Create(railwayStation);
                        break;

                    case "2":
                        break;

                    case "3":
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }

    class Train
    {
        List<Wagon> _wagons = new List<Wagon>();

        Wagon _smallWagon = new Wagon(20, 'S');
        Wagon _mediumWagon = new Wagon(50, 'M');
        Wagon _largeWagon = new Wagon(100, 'L');

        public string DrawWagons()
        {
            string train = "";

            foreach (Wagon wagon in _wagons)
            {
                wagon.ShowInfo();
            }

            return train += _wagons;
        }

        public void ClearWagons()
        {
            foreach (Wagon wagon in _wagons)
            {
                _wagons.Remove(wagon);
            }
        }

        public void Create(RailwayStation railwayStation)
        {
            const string CommandAddSmallWagon = "1";
            const string CommandAddMediumWagon = "2";
            const string CommandAddLargeWagon = "3";
            const string CommandSendTrain = "4";

            bool isPassengersPlanted = true;
            int passengersCount = railwayStation.CreatePassengersCount();

            while (isPassengersPlanted)
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Поезд:");
                DrawWagons();
                Console.SetCursorPosition(0, 0);

                if (passengersCount > 0)
                {
                    Console.WriteLine($"На это направление куплено {passengersCount} билетов");
                }
                else
                {
                    int freePlaces = passengersCount * -1;
                    Console.WriteLine($"В поезде есть {freePlaces} свободных мест");
                }


                Console.Write($"{CommandAddSmallWagon} - добавляет вагон вместимостью {_smallWagon.Capacity} мест\n" +
                    $"{CommandAddMediumWagon} - добавляет вагон вместимостью {_mediumWagon.Capacity} мест\n" +
                    $"{CommandAddLargeWagon} - добавляет вагон вместимостью {_largeWagon.Capacity} мест\n" +
                    $"{CommandSendTrain} - отправить поезд\n" +
                    $"\nВаш ввод: ");

                switch (Console.ReadLine())
                {
                    case CommandAddSmallWagon:
                        passengersCount = SeatPassengers(_smallWagon, passengersCount);
                        break;

                    case CommandAddMediumWagon:
                        passengersCount = SeatPassengers(_mediumWagon, passengersCount);
                        break;

                    case CommandAddLargeWagon:
                        passengersCount = SeatPassengers(_largeWagon, passengersCount);
                        break;

                    case CommandSendTrain:
                        isPassengersPlanted = IsPassengersPlanted(passengersCount);
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        public void AddWagon(Wagon wagon)
        {
            _wagons.Add(wagon);
        }

        private int RemovePassengers(int passengersCount, Wagon wagon)
        {
            return passengersCount -= wagon.Capacity;
        }

        private int SeatPassengers(Wagon wagon, int passengersCount)
        {
            AddWagon(wagon);
            return RemovePassengers(passengersCount, wagon);
        }

        private bool IsPassengersPlanted(int passengers)
        {
            if (passengers <= 0)
            {
                Console.WriteLine("Все пассажиры размещены, отправляем поезд...");
                return false;
            }
            else
            {
                Console.WriteLine("Недостаточно мест для всех пассажиров, купивших билеты. Добавьте еще вагонов...");
                return true;
            }
        }
    }

    class RailwayStation
    {
        List<Direction> _directions = new List<Direction>();
        List<string> _trains = new List<string>();

        public void CreateRoute()
        {
            Console.Write("\nВпишите точку отправления: ");
            string departure = Console.ReadLine();
            Console.Write("Впишите точку прибытия: ");
            string arrival = Console.ReadLine();
            _directions.Add(new Direction(departure, arrival));
        }

        public int CreatePassengersCount()
        {
            Random random = new Random();
            int passengersCount = random.Next(100, 501);
            return passengersCount;
        }
    }

    class Wagon
    {
        public Wagon(int capacity, char mark)
        {
            Capacity = capacity;
            Mark = mark;
        }

        public int Capacity { get; private set; }
        public char Mark { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"[{Mark}]-");
        }
    }

    class Direction
    {
        private string _pointOfDeparture;
        private string _pointOfArrival;

        public Direction(string pointOfDeparture, string pointOfArrival)
        {
            _pointOfDeparture = pointOfDeparture;
            _pointOfArrival = pointOfArrival;
        }

        public void ShowInfo()
        {
            Console.Write($"{_pointOfDeparture} - {_pointOfArrival}");
        }
    }
}
