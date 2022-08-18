using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            Garage garage = new Garage();
            UI userInterface = new UI(garage);

            while (!userInterface.DoesUserWantToExit)
            {
                userInterface.ShowMenu();
            }
        }
    }
}
