using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private readonly bool r_DoesContainDangerousMaterial;
        private readonly float r_CargoWeight;

        public Truck(bool i_DoesContainDangerousMaterial, float i_CargoWeight,
            string i_BrandName, string i_PlateNumber, List<Wheel> i_Wheels, Energy i_EnergyType)
            : base(i_BrandName, i_PlateNumber, i_Wheels, i_EnergyType)
        {
            r_DoesContainDangerousMaterial = i_DoesContainDangerousMaterial;
            r_CargoWeight = i_CargoWeight;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("Dangerous Material: {0}, Cargo Weight: {1}",
             r_DoesContainDangerousMaterial? "yes" : "no", r_CargoWeight);
        }
    }
}
