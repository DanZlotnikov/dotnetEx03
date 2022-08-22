using System;

namespace Ex03.GarageLogic
{
    public class ValueRangeException : Exception
    {
        private readonly float r_MinValue;
        public float MinValue
        {
            get
            {
                return r_MinValue;
            }
        }
        private readonly float r_MaxValue;
        public float MaxValue
        {
            get
            {
                return r_MaxValue;
            }
        }

        public ValueRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format("Value out of range. The range is {0} - {1}", i_MinValue, i_MaxValue))
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }
    }
}