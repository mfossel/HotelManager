using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Domain
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal CostPerNight { get; set; }
        public decimal TotalCost { get; set; } //Calculate from Checkin/Checkout date???
        public Customer Customer { get; set; }
        public Room Room { get; set; }
    }
}
