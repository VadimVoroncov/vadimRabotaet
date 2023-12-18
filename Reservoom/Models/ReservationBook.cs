using Reservoom.Exceptions;

namespace Reservoom.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> _reservations; // Список резерваций
        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        // TODO Возвращаем все резервации для указанного пользователя.
        public IEnumerable<Reservation> GetReservationsForUser(string userName)
        {
            return _reservations.Where(r => r.UserName == userName);
        }

        // TODO Добавляем новую резервацию в список, проверяя на конфликты с существующими резервациями.
        public void AddReservation(Reservation reservation)
        {
            foreach (Reservation existingReservation in  _reservations)
            {
                if(existingReservation.Conflicts(reservation)) 
                {
                    throw new ReservationConflictException(existingReservation, reservation);
                }
            }
            _reservations.Add(reservation);
        }
    }
}
