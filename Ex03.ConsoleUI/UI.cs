using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public enum eMenuOptions
        {
            InsertVehicle = 1,
            GetPlateNumbers,
            ChangeVehicleStatus,
            FillTiresToMax, 
            Refuel,
            ChargeBattery,
            DisplayVehicleDescription,
            ExitGarage,

            // Must be last:
            LastMenuItem
        }

        private bool m_UserWantsToExit = false;

        public bool DoesUserWantToExit
        {
            get
            {
                return m_UserWantsToExit;
            }
        }

        private readonly Garage r_Garage;

        public UI(Garage i_Garage)
        {
            r_Garage = i_Garage;
        }

        public void ShowMenu()
        {
            int chosenOption = -1;

            Console.WriteLine(string.Format(
@"Choose the wanted option:
1 - Insert a vehicle to the garage
2 - List by status the plate numbers in the garage
3 - Change the status of a vehicle
4 - Fill tires to maximum
5 - Refuel fuel vehicle
6 - Recharge an electric vehicle.
7 - Display vehicle full information
8 - Exit the garage"));

            chosenOption = getValidInputRange(1, (int)(eMenuOptions.LastMenuItem - 1));
            handleUserMenuInput(chosenOption);
        }

        private static int getValidInputRange(int i_MinRange, int i_MaxRange)
        {
            string inputString = string.Empty;
            int inputNumber = -1;
            bool isNumber = false;
            bool isInRange = false;

            while ((!isNumber) || (!isInRange))
            {
                inputString = Console.ReadLine();
                isNumber = int.TryParse(inputString, out inputNumber);
                if (!isNumber)
                {
                    Console.WriteLine("Your input is invalid");
                }
                else if (inputNumber < i_MinRange || inputNumber > i_MaxRange)
                {
                    Console.WriteLine("Chosen option is not in range");
                }
                else
                {
                    break;
                }

                Console.WriteLine(string.Format("please enter a number between 1 - {0}", i_MaxRange));
            }

            return inputNumber;
        }

        public void handleUserMenuInput(int i_MenuOption)
        {
            eMenuOptions menuOption = (eMenuOptions)i_MenuOption;
            switch(menuOption)
            {
                case eMenuOptions.InsertVehicle:
                    insertVehicle();
                    break;

                case eMenuOptions.GetPlateNumbers:
                    getPlateNumbers();
                    break;

                case eMenuOptions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;

                case eMenuOptions.FillTiresToMax:
                    fillTiresToMax();
                    break;

                case eMenuOptions.Refuel:
                    refuel();
                    break;

                case eMenuOptions.ChargeBattery:
                    chargeBattery();
                    break;

                case eMenuOptions.DisplayVehicleDescription:
                    displayVehicleDescription();
                    break;

                case eMenuOptions.ExitGarage:
                    m_UserWantsToExit = true;
                    break;
            }
        }

        private string getValidPlateNumber()
        {
            bool doesVehicleExist = false;
            string plateNumber = string.Empty;

            while(!doesVehicleExist)
            {
                Console.WriteLine("What is the wanted plate number?");
                plateNumber = Console.ReadLine();
                doesVehicleExist = r_Garage.IsVehicleInGarage(plateNumber);
                if (!doesVehicleExist)
                {
                    Console.WriteLine("This vehicle doesn't exist in the garage");
                }
            }

            return plateNumber;
        }

        private void getBrandNameAndWheelsManufacturer(out string o_BrandName, out string o_WheelsManufacturer)
        {
            Console.WriteLine("Please enter the brand name");
            o_BrandName = Console.ReadLine();
            Console.WriteLine("Please enter the wheels manufacturer name");
            o_WheelsManufacturer = Console.ReadLine();
        }

        private Vehicle getWantedVehicleByVehicleOptions(string i_PlateNumber)
        {
            List<string> vehicleTypes = VehicleFactory.GetVehicleTypes();

            Console.WriteLine(string.Format(
                "What kind of vehicle do you want? Enter the number representing your choice.",
                vehicleTypes.Count));
            for (int i = 0; i < vehicleTypes.Count; i++)
            {
                Console.WriteLine(string.Format(
                    "{0} - {1}",
                    i + 1, vehicleTypes[i]));
            }

            int wantedVehicleType = getValidInputRange(1, (int)(VehicleFactory.eVehicleType.LastItem - 1));

            getBrandNameAndWheelsManufacturer(out string brandName, out string wheelsManufacturer);

            return VehicleFactory.CreateVehicle((VehicleFactory.eVehicleType)wantedVehicleType,
             i_PlateNumber, brandName, wheelsManufacturer, getValidInputRange, getValidFloat);
        }

        public static float getValidFloat()
        {
            string floatString = string.Empty;
            bool isNumber = false;
            float floatNumber = 0;

            while (!isNumber)
            {
                floatString = Console.ReadLine();
                isNumber = float.TryParse(floatString, out floatNumber);
                if (!isNumber)
                {
                    Console.WriteLine("Please enter a valid float");
                }
            }

            return floatNumber;
        }

        public void insertVehicle()
        {
            Console.WriteLine("What is the plate number of the vehicle you want to insert?");
            string plateNumberString = Console.ReadLine();

            if (!r_Garage.IsVehicleInGarage(plateNumberString))
            {
                Console.WriteLine("What is the name of the owner?");
                string ownerName = Console.ReadLine();
                Console.WriteLine("What is the phone number of the owner?"); 
                string ownerPhoneNumber = Console.ReadLine();
                Vehicle wantedVehicle = getWantedVehicleByVehicleOptions(plateNumberString);
                r_Garage.InsertVehicle(wantedVehicle, ownerName, ownerPhoneNumber); 
                Console.WriteLine("The vehicle was inserted to the garage.");
            }
            else
            {
                Console.WriteLine("This vehicle is already in the garage");
                r_Garage.ChangeVehicleStatus(plateNumberString, VehicleDescription.eVehicleStatus.BeingRepaired);
            }
        }

        public void getPlateNumbers()
        {
            List<string> plateNumbers = null;

            Console.WriteLine(string.Format(
@"What filter do you want? 
0 - No filter
1 - Vehicles that are being repaired
2 - Repaired vehicles only
3 - Paid vehicles only
Enter the number representing your choice."));

            int filterOptions = getValidInputRange(0, (int)VehicleDescription.eVehicleStatus.LastItem);
            if (filterOptions == 0)
            {
                plateNumbers = r_Garage.GetPlateNumbers();
            }
            else
            {
                plateNumbers = r_Garage.GetPlateNumbers((VehicleDescription.eVehicleStatus)filterOptions);
            }

            if (plateNumbers.Count == 0)
            {
                Console.WriteLine("No vehicles in the garage as requested");
            }
            else
            {
                Console.WriteLine("===============Listing Vehicles===============");
                foreach (string plateNumber in plateNumbers)
                {
                    Console.WriteLine(plateNumber);
                }
                Console.WriteLine("=====================End======================");
            }
        }

        public void changeVehicleStatus()
        {
            string plateNumber = getValidPlateNumber();
            Console.WriteLine(string.Format(
@"Enter the new wanted status: 
1 - BeingRepaired
2 - AlreadyRepaired
3 - Paid
Enter the number representing your choice."));
            VehicleDescription.eVehicleStatus wantedNewStatus = (VehicleDescription.eVehicleStatus)getValidInputRange(1,
             (int)VehicleDescription.eVehicleStatus.LastItem);
            r_Garage.ChangeVehicleStatus(plateNumber, wantedNewStatus);
            Console.WriteLine("Changed the status successfully!"); 
        }

        public void fillTiresToMax()
        {
            string plateNumber = getValidPlateNumber();
            r_Garage.FillTiresToMax(plateNumber);
            Console.WriteLine("Filled tires successfully!");
        }

        public void refuel()
        {
            string plateNumber = getValidPlateNumber();
            Console.WriteLine(string.Format(
@"What type of fuel do you want? 
1 - Soler
2 - Octan95
3 - Octan96
4 - Octan98
Enter the number representing your choice."));
            FuelEnergy.eFuelType wantedFuelType = (FuelEnergy.eFuelType)getValidInputRange(1, (int)FuelEnergy.eFuelType.LastItem);
            Console.WriteLine("What is the amount you want to fill?");
            float amountToFill = getValidFloat();

            try
            {
                r_Garage.FillFuel(plateNumber, wantedFuelType, amountToFill);
                Console.WriteLine("Refueled successfully!");
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(formatException.Message);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(string.Format("Maximum tank size is {0} liters",
                    valueOutOfRangeException.MaxValue));
            }
        }

        public void chargeBattery()
        {
            string plateNumber = getValidPlateNumber();
            Console.WriteLine("How many minutes do you want to charge?");
            float minutesToCharge = getValidFloat();

            try
            {
                r_Garage.ChargeBattery(plateNumber, minutesToCharge);
                Console.WriteLine("Battery is now charged!");
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(formatException.Message);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(string.Format("Maximim value is {0}", valueOutOfRangeException.MaxValue));
            }
        }

        public void displayVehicleDescription()
        {
            string plateNumber = getValidPlateNumber();
            Console.WriteLine(r_Garage.DisplayFullVehicleDescription(plateNumber));
        }
    }
}
