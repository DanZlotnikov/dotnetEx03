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
1 - Insert vehicle to the garage
2 - List vehicle plate numbers in the garage by status
3 - Change vehicle status
4 - Fill tires to the maximum
5 - Refuel vehicle (valid for fuel vehicles)
6 - Recharge a vehicle (valid for electric vehicles)
7 - Display vehicle information
8 - Exit"));

            chosenOption = getInputRange(1, (int)(eMenuOptions.LastMenuItem - 1));
            handleMenuInput(chosenOption);
        }

        private static int getInputRange(int i_MinRange, int i_MaxRange)
        {
            string inputStr = string.Empty;
            int inputNum = -1;
            bool isNumber = false;
            bool isInRange = false;

            while ((!isNumber) || (!isInRange))
            {
                inputStr = Console.ReadLine();
                isNumber = int.TryParse(inputStr, out inputNum);
                if (!isNumber)
                {
                    Console.WriteLine("Input is invalid!");
                }
                else if (inputNum < i_MinRange || inputNum > i_MaxRange)
                {
                    Console.WriteLine("Option is out of range!");
                }
                else
                {
                    break;
                }

                Console.WriteLine(string.Format("Please enter a number between 1 - {0}", i_MaxRange));
            }

            return inputNum;
        }

        public void handleMenuInput(int i_MenuOption)
        {
            eMenuOptions menuOption = (eMenuOptions)i_MenuOption;
            switch(menuOption)
            {
                case eMenuOptions.InsertVehicle:
                    insertNewVehicle();
                    break;

                case eMenuOptions.GetPlateNumbers:
                    getPlateNumbers();
                    break;

                case eMenuOptions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;

                case eMenuOptions.FillTiresToMax:
                    fillTiresMax();
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
                Console.WriteLine("Please enter vehicle plate number");
                plateNumber = Console.ReadLine();
                doesVehicleExist = r_Garage.IsVehicleInGarage(plateNumber);
                if (!doesVehicleExist)
                {
                    Console.WriteLine("This vehicle isn't in the garage");
                }
            }

            return plateNumber;
        }

        private void getBrandNameAndWheelsManufacturer(out string o_BrandName, out string o_WheelsManufacturer)
        {
            Console.WriteLine("Brand name:");
            o_BrandName = Console.ReadLine();
            Console.WriteLine("Wheels manufacturer name:");
            o_WheelsManufacturer = Console.ReadLine();
        }

        private Vehicle getWantedVehicleByVehicleOptions(string i_PlateNumber)
        {
            List<string> vehicleTypes = VehicleFactory.GetVehicleTypes();

            Console.WriteLine(string.Format(
                "What type of vehicle do you want? Please enter the corresponding number:",
                vehicleTypes.Count));
            for (int i = 0; i < vehicleTypes.Count; i++)
            {
                Console.WriteLine(string.Format(
                    "{0} - {1}",
                    i + 1, vehicleTypes[i]));
            }

            int wantedVehicleType = getInputRange(1, (int)(VehicleFactory.eVehicleType.LastItem - 1));

            getBrandNameAndWheelsManufacturer(out string brandName, out string wheelsManufacturer);

            return VehicleFactory.CreateVehicle((VehicleFactory.eVehicleType)wantedVehicleType,
             i_PlateNumber, brandName, wheelsManufacturer, getInputRange, getValidFloat);
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
                    Console.WriteLine("Please enter a valid float!");
                }
            }

            return floatNumber;
        }

        public void insertNewVehicle()
        {
            Console.WriteLine("Please enter the plate number:");
            string plateNumberString = Console.ReadLine();

            if (!r_Garage.IsVehicleInGarage(plateNumberString))
            {
                Console.WriteLine("What is the owner's name?");
                string ownerName = Console.ReadLine();
                Console.WriteLine("What is the owner' phone number?"); 
                string ownerPhoneNumber = Console.ReadLine();
                Vehicle wantedVehicle = getWantedVehicleByVehicleOptions(plateNumberString);
                r_Garage.InsertVehicle(wantedVehicle, ownerName, ownerPhoneNumber); 
                Console.WriteLine("The vehicle was successfully inserted to the garage.");
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
@"Which filter do you want? 
0 - No filter
1 - Vehicles on repaired
2 - Repaired vehicles only
3 - Paid vehicles only
Please enter the number representing your choice."));

            int filterOptions = getInputRange(0, (int)VehicleDescription.eVehicleStatus.LastItem);
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
                Console.WriteLine("There are no vehicle in the garage matching this filter");
            }
            else
            {
                Console.WriteLine("================Listing Vehicles================");
                foreach (string plateNumber in plateNumbers)
                {
                    Console.WriteLine(plateNumber);
                }
                Console.WriteLine("======================End=======================");
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
            VehicleDescription.eVehicleStatus wantedNewStatus = (VehicleDescription.eVehicleStatus)getInputRange(1,
             (int)VehicleDescription.eVehicleStatus.LastItem);
            r_Garage.ChangeVehicleStatus(plateNumber, wantedNewStatus);
            Console.WriteLine("Status changed!"); 
        }

        public void fillTiresMax()
        {
            string plateNumber = getValidPlateNumber();
            r_Garage.FillTiresToMax(plateNumber);
            Console.WriteLine("Tires full!");
        }

        public void refuel()
        {
            string plateNumber = getValidPlateNumber();
            Console.WriteLine(string.Format(
@"What type of fuel? 
1 - Soler
2 - Octan95
3 - Octan96
4 - Octan98
Enter the number representing your choice."));
            FuelEnergy.eFuelType wantedFuelType = (FuelEnergy.eFuelType)getInputRange(1, (int)FuelEnergy.eFuelType.LastItem);
            Console.WriteLine("How much fuel do you want to fill?");
            float amountToFill = getValidFloat();

            try
            {
                r_Garage.FillFuel(plateNumber, wantedFuelType, amountToFill);
                Console.WriteLine("Refueled!");
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
                Console.WriteLine(string.Format("Maximum tank size is {0} litres",
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
                Console.WriteLine("Charged!");
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(formatException.Message);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(string.Format("Maximum value is {0}", valueOutOfRangeException.MaxValue));
            }
        }

        public void displayVehicleDescription()
        {
            string plateNumber = getValidPlateNumber();
            Console.WriteLine(r_Garage.DisplayFullVehicleDescription(plateNumber));
        }
    }
}
