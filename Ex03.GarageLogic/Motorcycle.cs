using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A = 1,
            A1,
            A2,
            B,

            // Must be last:
            LastItem
        }

        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineCapacity;

        public Motorcycle(eLicenseType i_LicenseType, int i_EngineCapacity,
            string i_BrandName, string i_PlateNumber, List<Wheel> i_Wheels, Energy i_EnergyType)
            : base(i_BrandName, i_PlateNumber, i_Wheels, i_EnergyType)
        {
            r_LicenseType = i_LicenseType;
            r_EngineCapacity = i_EngineCapacity;
        }

        private string getLicenseType()
        {
            string licenseTypeString = string.Empty;

            switch (r_LicenseType)
            {
                case eLicenseType.A:
                licenseTypeString = "A";
                break;
                case eLicenseType.A1:
                licenseTypeString = "A1";
                break;
                case eLicenseType.A2:
                licenseTypeString = "A2";
                break;
                case eLicenseType.B:
                licenseTypeString = "B";
                break;
            }

            return licenseTypeString;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("License : {0} , Volume of engine : {1} ", getLicenseType(), r_EngineCapacity);
        }
    }
}
