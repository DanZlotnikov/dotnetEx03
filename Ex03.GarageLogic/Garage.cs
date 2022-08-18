using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleDescription> r_Vehicles;

        public Garage()
        {
            r_Vehicles = new Dictionary<string, VehicleDescription>();
        }

        public bool IsVehicleInGarage(string i_PlateNumber)
        {
            return r_Vehicles.ContainsKey(i_PlateNumber);
        }

        public void InsertVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            VehicleDescription currentVehicleDescription = new VehicleDescription(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle);
            r_Vehicles.Add(i_Vehicle.PlateNumber, currentVehicleDescription);
        }

        public List<string> GetPlateNumbers()
        {
            List<string> plates = new List<string>();

            foreach (VehicleDescription vehicleDescription in r_Vehicles.Values)
            {
                plates.Add(vehicleDescription.Vehicle.PlateNumber);
            }

            return plates;
        }

        public List<string> GetPlateNumbers(VehicleDescription.eVehicleStatus i_VehicleStatus)
        {
            List<string> platesByStatus = new List<string>();

            foreach(VehicleDescription vehicleDescription in r_Vehicles.Values)
            {
                if(vehicleDescription.VehicleStatus == i_VehicleStatus)
                {
                    platesByStatus.Add(vehicleDescription.Vehicle.PlateNumber);
                }
            }

            return platesByStatus;
        }

        public void ChangeVehicleStatus(string i_PlateNumber, VehicleDescription.eVehicleStatus i_NewVehicleStatus)
        {
            r_Vehicles[i_PlateNumber].VehicleStatus = i_NewVehicleStatus;
        }

        public void FillTiresToMax(string i_PlateNumber)
        {
            r_Vehicles[i_PlateNumber].Vehicle.FillTiresToMax();
        }

        public void FillFuel(string i_PlateNumber, FuelEnergy.eFuelType i_FuelType, float i_AmountOfFuel)
        {
            FuelEnergy currentFuelEnergy = r_Vehicles[i_PlateNumber].Vehicle.Energy as FuelEnergy;
            
            if(currentFuelEnergy == null)
            {
                throw new FormatException("This is not a fuel engine!");
            }
            
            currentFuelEnergy.FillFuel(i_FuelType, i_AmountOfFuel);
        }

        public void ChargeBattery(string i_PlateNumber, float i_MinutesToCharge)
        {
            ElectricalEnergy currentElectricalEnergy = r_Vehicles[i_PlateNumber].Vehicle.Energy as ElectricalEnergy;
            
            if(currentElectricalEnergy == null)
            {
                throw new FormatException("This is not an electrical engine!");
            }

            currentElectricalEnergy.FillEnergy(i_MinutesToCharge / 60);
        }

        public string DisplayFullVehicleDescription(string i_PlateNumber)
        {
            return r_Vehicles[i_PlateNumber].ToString();
        }
    }
}
