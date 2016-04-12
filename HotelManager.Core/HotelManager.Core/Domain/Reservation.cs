using HotelManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Domain
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string UserId { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal CostPerNight { get; set; }
        public decimal TotalCost { get; set; } //Calculate from Checkin/Checkout date???

        public virtual User User { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }

        public Reservation() { }

        public Reservation(ReservationModel model)
        {
            this.Update(model);
        }

        public void Update(ReservationModel model)
        {
            ReservationId = model.ReservationId;
            UserId = model.UserId;
            RoomId = model.RoomId;
            CustomerId = model.CustomerId;
            CheckInDate = model.CheckInDate;
            CheckOutDate = model.CheckOutDate;
            CostPerNight = model.CostPerNight;


        }
    }
}
