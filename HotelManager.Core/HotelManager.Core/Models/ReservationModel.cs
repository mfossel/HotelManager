using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Models
{
    class ReservationModel
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal CostPerNight { get; set; }
        public decimal TotalCost { get; set; } //Calculate from Checkin/Checkout date???
        public CustomerModel Customer { get; set; }
        public RoomModel Room { get; set; }

    }
}
