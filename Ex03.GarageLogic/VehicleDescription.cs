using System;

namespace Ex03.GarageLogic
{
    public class VehicleDescription
    {
        public enum eVehicleStatus
        {
            BeingRepaired = 1,
            AlreadyRepaired,
            Paid,

            // Must be last:
            LastItem
        }

        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private readonly Vehicle r_Vehicle;
        private eVehicleStatus m_currentVehicleStatus = eVehicleStatus.BeingRepaired;

        public string OwnerName
        {
            get
            {
                return r_OwnerName;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return r_OwnerPhoneNumber;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_currentVehicleStatus;
            }
            set
            {
                m_currentVehicleStatus = value;
            }
        }

        public VehicleDescription(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            r_Vehicle = i_Vehicle;
        }

        public override string ToString()
        {
            return string.Format(
@"Plate Number: {0}
Brand Name: {1}
Owner Name: {2}
Vehicle Status in garage: {3}
Specific Info: {4}
", r_Vehicle.PlateNumber, r_Vehicle.BrandName, r_OwnerName, m_currentVehicleStatus, r_Vehicle.ToString());
        }
    }
}
