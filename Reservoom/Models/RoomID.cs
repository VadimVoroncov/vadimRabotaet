using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class RoomID
    {
        public int FloorNumber { get; set; }
        public int RoomNumber { get; set; }
        public RoomID(int floorNumber, int roomNumber)
        {
            FloorNumber = floorNumber;
            RoomNumber = roomNumber;
        }

        public override bool Equals(object? obj)
        {
            return obj is RoomID roomID 
                && FloorNumber == roomID.FloorNumber 
                && RoomNumber == roomID.RoomNumber; 
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        public override string ToString()
        {
            return $"{FloorNumber}{RoomNumber}";
        }

        public static bool operator == (RoomID left, RoomID right)
        {
            if(left is null && right is null)
            {
                return true;
            }
            return left is not null && left.Equals(right);
        }
        public static bool operator !=(RoomID left, RoomID right)
        {
            return !(left == right);
        }
    }
}
