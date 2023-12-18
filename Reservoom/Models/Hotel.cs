using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;
        public string Name { get; }
        public Hotel(string name)
        {
            Name = name;
            _reservationBook = new ReservationBook();
        }

        // TODO Возвращаем все резервации для указанного пользователя
        public IEnumerable<Reservation> GetReservationsForUser(string userName)
        {
            return _reservationBook.GetReservationsForUser(userName);
        }

        // TODO Добавляем новую резервацию в список резерваций отеля, используя объект ReservationBook.
        public void MakeReservation(Reservation reservation)
        {
            _reservationBook.AddReservation(reservation);
        }
    }
}
