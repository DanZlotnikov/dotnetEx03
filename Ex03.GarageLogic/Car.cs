using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Yellow = 1,
            White,
            Black,
            Blue,
           
            LastItem
        }

        public enum eDoorsNumber
        {
            Two = 1,
            Three,
            Four,
            Five,

            LastItem
        }

        private readonly eColor r_Color;
        private readonly eDoorsNumber r_DoorsNumber;

        public Car(eColor i_Color, eDoorsNumber i_DoorsNumber,
            string i_BrandName, string i_PlateNumber, List<Wheel> i_Wheels, Energy i_EnergyType)
            : base(i_BrandName, i_PlateNumber, i_Wheels, i_EnergyType)
        {
            r_Color = i_Color;
            r_DoorsNumber = i_DoorsNumber;
        }

        private string getColor()
        {
            string colorString = string.Empty;

            switch (r_Color)
            {
                case eColor.Yellow:
                    colorString = "Yellow";
                    break;
                case eColor.Blue:
                    colorString = "Blue";
                    break;
                case eColor.Black:
                    colorString = "Black";
                    break;
                case eColor.White:
                    colorString = "White";
                    break;
            }

            return colorString;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("Car color : {0} , Number of Doors : {1} ", getColor(), r_DoorsNumber + 1);
        }
    }
}
