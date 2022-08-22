using System;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : Energy
    {
        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98,

            LastItem
        }

        private readonly eFuelType r_FuelType;

        public float CurrentFuelLiters
        {
            get
            {
                return m_CurrentEnergy;
            }
        }

        public float MaxFuelLiters
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public FuelEnergy(eFuelType i_FuelType, float i_MaximumFuelLiters) : base(i_MaximumFuelLiters)
        {
            r_FuelType = i_FuelType;
        }

        public void FillFuel(eFuelType i_FuelType, float i_FuelToAddLiters)
        {
            if(i_FuelType != r_FuelType)
            {
                throw new ArgumentException("Wrong fuel type for this type of vehicle!");
            }

            FillEnergy(i_FuelToAddLiters);
        }

        public override string ToString()
        {
            string fuelTypeString = string.Empty;

            switch (r_FuelType)
            {
                case eFuelType.Soler:
                fuelTypeString = "Soler";
                break;
                case eFuelType.Octan95:
                fuelTypeString = "Octan95";
                break;
                case eFuelType.Octan96:
                fuelTypeString = "Octan96";
                break;
                case eFuelType.Octan98:
                fuelTypeString = "Octan98";
                break;
            }

            return string.Format("Fuel type: {0} {1}% full.", fuelTypeString, EnergyPercentage);
        }
    }
}