using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            FuelMotorcycle = 1,
            ElectricalMotorcycle,
            FuelCar,
            ElectricalCar,
            FuelTruck,

            LastItem
        }

        private struct FuelMotorcycleProperties
        {
            public const int k_WheelsNumber = 2;
            public const float k_AirPressure = 28;
            public const FuelEnergy.eFuelType k_FuelType = FuelEnergy.eFuelType.Octan98;
            public const float k_MaxFuelLiters = 5.5f;
        }

        private struct ElectricalMotorcycleProperties
        {
            public const int k_WheelsNumber = 2;
            public const float k_AirPressure = 28;
            public const float k_MaxBatteryHours = 1.6f;
        }

        private struct FuelCarProperties
        {
            public const int k_WheelsNumber = 4;
            public const float k_AirPressure = 30;
            public const FuelEnergy.eFuelType k_FuelType = FuelEnergy.eFuelType.Octan95;
            public const float k_MaxFuelLiters = 50f;
        }

        private struct ElectricalCarProperties
        {
            public const int k_WheelsNumber = 4;
            public const float k_AirPressure = 30;
            public const float k_MaxBatteryHours = 2.8f;
        }

        private struct FuelTruckProperties
        {
            public const int k_WheelsNumber = 16;
            public const float k_AirPressure = 26;
            public const FuelEnergy.eFuelType k_FuelType = FuelEnergy.eFuelType.Soler;
            public const float k_MaxFuelLiters = 110f;
        }

        public static List<string> GetVehicleTypes()
        {
            return new List<string>{"FuelMotorcycle", "ElectricalMotorcycle", "FuelCar", "ElectricalCar", "Truck"};
        }

        private static void getMotorcycleProperties(Func<int, int, int> GetValidInputRange, out int o_EngineVolume, out Motorcycle.eLicenseType o_LicenseType)
        {
            Console.WriteLine("Enter engine volume: ");
            o_EngineVolume = int.Parse(Console.ReadLine());
            Console.WriteLine(string.Format(
@"Please choose license type:
1 - A 
2 - A1 
3 - A2 
4 - B
Enter the number representing your choice."));
            o_LicenseType = (Motorcycle.eLicenseType)GetValidInputRange(1, (int)Motorcycle.eLicenseType.LastItem);
        }

        private static void getCarProperties(Func<int, int, int> GetValidInputRange, out Car.eColor o_CarColor, out Car.eDoorsNumber o_CarDoorsNumber)
        {
            Console.WriteLine(string.Format(
@"Please choose car color:
1 - Yellow 
2 - White 
3 - Black
4 - Blue
Enter the number representing your choice."));
            o_CarColor = (Car.eColor)GetValidInputRange(1, (int)Car.eColor.LastItem);
            Console.WriteLine(string.Format(
@"How many doors?
1 - Two 
2 - Three
3 - Four
4 - Five
Enter the number representing your choice."));
            o_CarDoorsNumber = (Car.eDoorsNumber)GetValidInputRange(1, (int)Car.eDoorsNumber.LastItem);
        }

        private static void getTruckProperties(Func<float> GetValidFloat, out bool o_ContainsDangerousMaterial, out float o_MaximumCarryWeight)
        {
            string input = string.Empty;

            do
            {
                Console.WriteLine("Contains dangerous material? Y/N");
                input = Console.ReadLine();
            }
            while (input != "Y" && input != "N");

            if (input == "Y")
            {
                o_ContainsDangerousMaterial = true;
            }
            else
            {
                o_ContainsDangerousMaterial = false;
            }

            Console.WriteLine("Max carrying weight?");
            o_MaximumCarryWeight = GetValidFloat();
        }

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_PlateNumber, string i_BrandName,
         string i_WheelsManufacturer, Func<int, int, int> GetValidInputRange, Func<float> GetValidFloat)
        {
            Vehicle createdVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelMotorcycle:
                getMotorcycleProperties(GetValidInputRange, out int fuelEngineVolume, out Motorcycle.eLicenseType fuelLicenseType);
                createdVehicle = createFuelMotorcycle(i_WheelsManufacturer, fuelLicenseType, fuelEngineVolume,
                 i_BrandName, i_PlateNumber);
                break;
                case eVehicleType.ElectricalMotorcycle:
                getMotorcycleProperties(GetValidInputRange, out int electricalEngineVolume, out Motorcycle.eLicenseType electricalLicenseType);
                createdVehicle = createElectricalMotorcycle(i_WheelsManufacturer, electricalLicenseType, electricalEngineVolume,
                 i_BrandName, i_PlateNumber);
                break;
                case eVehicleType.FuelCar:
                getCarProperties(GetValidInputRange, out Car.eColor fuelCarColor, out Car.eDoorsNumber fuelCarDoorsNumber);
                createdVehicle = createFuelCar(fuelCarColor, fuelCarDoorsNumber, i_BrandName, i_PlateNumber,
                 i_WheelsManufacturer);
                break;
                case eVehicleType.ElectricalCar:
                getCarProperties(GetValidInputRange, out Car.eColor electricalCarColor, out Car.eDoorsNumber electricalCarDoorsNumber);
                createdVehicle = createElectricalCar(electricalCarColor, electricalCarDoorsNumber, i_BrandName, i_PlateNumber,
                 i_WheelsManufacturer);
                break;
                case eVehicleType.FuelTruck:
                getTruckProperties(GetValidFloat, out bool doesContainDangerousMaterial, out float maximumCarryingWeight);
                createdVehicle = createFuelTruck(doesContainDangerousMaterial, maximumCarryingWeight, 
                i_BrandName, i_PlateNumber, i_WheelsManufacturer);
                break;
            }

            return createdVehicle;
        }

        private static List<Wheel> createListOfWheels(string i_ManufacturerName, float i_MaxTirePressure, int i_WheelsNumber)
        {
            List<Wheel> wheels = new List<Wheel>(i_WheelsNumber);

            for(int i = 0; i < wheels.Count; i++)
            {
                Wheel newTempWheel = new Wheel(i_ManufacturerName, 0, i_MaxTirePressure);
                wheels.Add(newTempWheel);
            }

            return wheels;
        }

        public static Motorcycle createFuelMotorcycle(string i_ManufacturerName,
         Motorcycle.eLicenseType i_LicenceType, int i_EngineVolume, string i_BrandName, string i_PlateNumber)
        {
            List<Wheel> wheels = createListOfWheels(i_ManufacturerName,
             FuelMotorcycleProperties.k_AirPressure, FuelMotorcycleProperties.k_WheelsNumber);
            FuelEnergy fuelEnergy = new FuelEnergy(FuelMotorcycleProperties.k_FuelType,
             FuelMotorcycleProperties.k_MaxFuelLiters);
            Motorcycle motorcycle = new Motorcycle(i_LicenceType, i_EngineVolume, i_BrandName, i_PlateNumber, wheels, fuelEnergy);

            return motorcycle;
        }

        public static Motorcycle createElectricalMotorcycle(string i_ManufacturerName,
         Motorcycle.eLicenseType i_LicenceType, int i_EngineVolume, string i_BrandName, string i_PlateNumber)
        {
            List<Wheel> wheels = createListOfWheels(i_ManufacturerName,
             ElectricalMotorcycleProperties.k_AirPressure, ElectricalMotorcycleProperties.k_WheelsNumber);
            ElectricalEnergy electricalEnergy = new ElectricalEnergy(ElectricalMotorcycleProperties.k_MaxBatteryHours);
            Motorcycle motorcycle = new Motorcycle(i_LicenceType, i_EngineVolume, i_BrandName, i_PlateNumber, wheels, electricalEnergy);

            return motorcycle;
        }

        public static Car createFuelCar(Car.eColor i_CarColor, Car.eDoorsNumber i_CarDoorsNumber,
            string i_BrandName, string i_PlateNumber, string i_ManufacturerName)
        {
            List<Wheel> wheels = createListOfWheels(i_ManufacturerName,
             FuelCarProperties.k_AirPressure, FuelCarProperties.k_WheelsNumber);
            FuelEnergy fuelEnergy = new FuelEnergy(FuelCarProperties.k_FuelType, FuelCarProperties.k_MaxFuelLiters);
            Car car = new Car(i_CarColor, i_CarDoorsNumber, i_BrandName, i_PlateNumber, wheels, fuelEnergy);

            return car;
        }

        public static Car createElectricalCar(Car.eColor i_CarColor, Car.eDoorsNumber i_CarDoorsNumber,
            string i_BrandName, string i_PlateNumber, string i_ManufacturerName)
        {
            List<Wheel> wheels = createListOfWheels(i_ManufacturerName,
             ElectricalCarProperties.k_AirPressure, ElectricalCarProperties.k_WheelsNumber);
            ElectricalEnergy electricalEnergy = new ElectricalEnergy(ElectricalCarProperties.k_MaxBatteryHours);
            Car car = new Car(i_CarColor, i_CarDoorsNumber, i_BrandName, i_PlateNumber, wheels, electricalEnergy);

            return car;
        }

        public static Truck createFuelTruck(bool i_DoesContainDangerousMaterial, float i_MaximumCarryingWeight,
            string i_BrandName, string i_PlateNumber, string i_ManufacturerName)
        {
            List<Wheel> wheels = createListOfWheels(i_ManufacturerName,
             FuelTruckProperties.k_AirPressure, FuelTruckProperties.k_WheelsNumber);
            FuelEnergy fuelEnergy = new FuelEnergy(FuelTruckProperties.k_FuelType, FuelTruckProperties.k_MaxFuelLiters);
            Truck truck = new Truck(i_DoesContainDangerousMaterial, i_MaximumCarryingWeight, i_BrandName, i_PlateNumber, wheels, fuelEnergy);

            return truck;
        }
    }
}
