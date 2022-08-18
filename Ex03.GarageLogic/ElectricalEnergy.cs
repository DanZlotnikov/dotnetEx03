using System;

namespace Ex03.GarageLogic
{
    public class ElectricalEnergy : Energy
    {
        public float MaxBatteryTimeHours
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        public float CurrentBatteryTimeHours
        {
            get
            {
                return m_CurrentEnergy;
            }
        }

        public ElectricalEnergy(float i_MaxBatteryHours) : base(i_MaxBatteryHours)
        {

        }

        public override string ToString()
        {
            return string.Format("Electrical {0}% full.", EnergyPercentage);
        }
    }
}
