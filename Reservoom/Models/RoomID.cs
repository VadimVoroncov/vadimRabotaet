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

        // TODO Проверяем, равен ли указанный объект текущему объекту RoomID.
        // Возвращает true, если номер этажа и номер комнаты равны, иначе возвращает false.
        public override bool Equals(object? obj)
        {
            return obj is RoomID roomID 
                && FloorNumber == roomID.FloorNumber 
                && RoomNumber == roomID.RoomNumber; 
        }

        // TODO Возвращаем хэш-код объекта RoomID, используя номер этажа и номер комнаты.
        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        // TODO Возвращаем строковое представление объекта RoomID, состоящее из номера этажа и номера комнаты, объединенных вместе.
        public override string ToString()
        {
            return $"{FloorNumber}{RoomNumber}";
        }

        // TODO Перегруженный оператор для сравнения двух объектов RoomID на равенство.
        // Возвращает true, если объекты равны, иначе возвращает false.
        public static bool operator == (RoomID left, RoomID right)
        {
            if(left is null && right is null)
            {
                return true;
            }
            return left is not null && left.Equals(right);
        }
        // TODO Перегруженный оператор для сравнения двух объектов RoomID на неравенство.
        // Возвращает true, если объекты не равны, иначе возвращает false.
        public static bool operator !=(RoomID left, RoomID right)
        {
            return !(left == right);
        }
    }
}
