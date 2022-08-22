using System;

namespace Ex03.GarageLogic
{
    public abstract class Energy
    {
        protected float m_CurrentEnergy;
        protected readonly float r_MaxEnergy;
        public float EnergyPercentage
        {
            get
            {
                return (m_CurrentEnergy / r_MaxEnergy) * 100;
            }
        }

        public Energy(float i_MaxEnergy)
        {
            m_CurrentEnergy = 0f;
            r_MaxEnergy = i_MaxEnergy;
        }

        public void FillEnergy(float i_AmountToAdd)
        {
            if (m_CurrentEnergy + i_AmountToAdd > r_MaxEnergy)
            {
                throw new ValueRangeException(0, r_MaxEnergy - m_CurrentEnergy);
            }

            m_CurrentEnergy += i_AmountToAdd;
        }
    }
}