using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentTirePressure;
        private readonly float r_MaxTirePressure;

        public Wheel(string i_ManufacturerName, float i_CurrentTirePressure, float i_MaxTirePressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentTirePressure = i_CurrentTirePressure;
            r_MaxTirePressure = i_MaxTirePressure;
        }

        public void FillTire(float i_TirePressureToAdd)
        {
            if (m_CurrentTirePressure + i_TirePressureToAdd > r_MaxTirePressure)
            {
                throw new ValueRangeException(0, r_MaxTirePressure - m_CurrentTirePressure);
            }

            m_CurrentTirePressure += i_TirePressureToAdd;
        }

        public void FillTireToMax()
        {
            m_CurrentTirePressure = r_MaxTirePressure;
        }
    }
}
