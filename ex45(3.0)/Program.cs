using System;
using System.Collections.Generic;

namespace ex45_3._0_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddDirection = "1";
            const string CommandShowAllDirections = "2";
            const string CommandExit = "3";

            RailwayStation railwayStation = new RailwayStation();
            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("Конфигуратор пассажирских поездов");
                Console.Write($"\n{CommandAddDirection} - создать направление\n" +
                    $"{CommandShowAllDirections} - показать все направления\n" +
                    $"{CommandExit} - выйти из программы\n" +
                    $"\nВаш ввод: ");

                switch (Console.ReadLine())
                {
                    case CommandAddDirection:
                        railwayStation.AddDirection();
                        break;

                    case CommandShowAllDirections:
                        railwayStation.ShowInfo();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;
                }

                Console.Clear();
            }
        }
    }

    class RailwayStation
    {
        List<Direction> _directions = new List<Direction>();
        List<Wagon> _wagons = new List<Wagon>();
        List<Train> _trains = new List<Train>();

        Wagon _smallWagon = new Wagon(20, 'S');
        Wagon _mediumWagon = new Wagon(50, 'M');
        Wagon _largeWagon = new Wagon(100, 'L');

        public void AddDirection()
        {
            if (IsDirectionAlreadyExist() == false)
            {
                CreateTrain();
            }
        }

        public void ShowInfo()
        {
            int position = 1;
            Console.Clear();

            if (_trains.Count > 0)
            {
                Console.WriteLine("Направления: ");

                foreach (Direction direction in _directions)
                {
                    direction.ShowInfo();
                }

                Console.SetCursorPosition(40, 0);
                Console.Write("Поезда: ");

                foreach (Train train in _trains)
                {
                    Console.SetCursorPosition(40, position);
                    train.ShowInfo();
                    position++;
                }
            }
            else
            {
                Console.WriteLine("Пока направлений нет...");
            }

            Console.ReadKey();
        }

        private int CreatePassengersCount()
        {
            Random random = new Random();
            int passengersCount = random.Next(100, 501);
            return passengersCount;
        }

        private string DrawWagons()
        {
            string train = "";

            foreach (Wagon wagon in _wagons)
            {
                train += wagon.GetInfo();
            }

            return train;
        }

        private bool IsDirectionAlreadyExist()
        {
            Console.Write("\nВпишите точку отправления: ");
            string departure = Console.ReadLine();
            Console.Write("Впишите точку прибытия: ");
            string arrival = Console.ReadLine();
            bool isExist = false;

            foreach (Direction direction in _directions)
            {
                if (direction.PointOfDeparture == departure && direction.PointOfArrival == arrival)
                {
                    isExist = true;
                }
            }

            if (isExist == false)
            {
                _directions.Add(new Direction(departure, arrival));
                return false;
            }
            else
            {
                Console.WriteLine("Такое направление уже есть...");
                Console.ReadKey();
                return true;
            }
        }

        private void CreateTrain()
        {
            const string CommandAddSmallWagon = "1";
            const string CommandAddMediumWagon = "2";
            const string CommandAddLargeWagon = "3";
            const string CommandSendTrain = "4";

            List<Wagon> wagons = new List<Wagon>();
            _wagons = wagons;

            bool isPassengersPlanted = true;
            int passengersCount = CreatePassengersCount();

            while (isPassengersPlanted)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Поезд:");
                Console.WriteLine(DrawWagons());
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

            _trains.Add(new Train(DrawWagons()));
        }

        private void AddWagon(Wagon wagon)
        {
            _wagons.Add(wagon);
        }

        private int RemovePassengers(int passengersCount, Wagon wagon)
        {
            return passengersCount - wagon.Capacity;
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

    class Train
    {
        private string _trainMark;

        public Train(string trainMark)
        {
            _trainMark = trainMark;
        }

        public void ShowInfo()
        {
            Console.Write($"{_trainMark}");
            Console.WriteLine("[)");
        }
    }

    class Wagon
    {
        private char _mark;

        public Wagon(int capacity, char mark)
        {
            Capacity = capacity;
            _mark = mark;
        }

        public int Capacity { get; private set; }

        public string GetInfo()
        {
            return $"[{_mark}]-";
        }
    }

    class Direction
    {
        public Direction(string pointOfDeparture, string pointOfArrival)
        {
            PointOfDeparture = pointOfDeparture;
            PointOfArrival = pointOfArrival;
        }

        public string PointOfDeparture { get; private set; }
        public string PointOfArrival { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{PointOfDeparture} - {PointOfArrival}");
        }
    }
}