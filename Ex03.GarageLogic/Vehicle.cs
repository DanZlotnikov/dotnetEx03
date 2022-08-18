using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_BrandName;
        private readonly string r_PlateNumber;
        private List<Wheel> r_Wheels;
        private readonly Energy r_Energy;

        public string BrandName
        {
            get
            {
                return r_BrandName;
            }
        }

        public string PlateNumber
        {
            get
            {
                return r_PlateNumber;
            }
        }

        public Energy Energy
        {
            get
            {
                return r_Energy;
            }
        }

        public Vehicle(string i_BrandName, string i_PlateNumber, List<Wheel> i_Wheels, Energy i_Energy)
        {
            r_BrandName = i_BrandName;
            r_PlateNumber = i_PlateNumber;
            r_Wheels = i_Wheels;
            r_Energy = i_Energy;
        }

        public void FillTiresToMax()
        {
            foreach(Wheel wheel in r_Wheels)
            {
                wheel.FillTireToMax();
            }
        }

        public override string ToString()
        {
            StringBuilder wheelsInfo = new StringBuilder();

            for (int i=0;i<r_Wheels.Count;i++)
            {
                wheelsInfo.Append(string.Format("Wheel #{0}:{1}", (i + 1), r_Wheels[i].ToString()));
            }

            return string.Format(
@"Wheels: {0}
{1}
", wheelsInfo, r_Energy.ToString());
        }
    }
}
