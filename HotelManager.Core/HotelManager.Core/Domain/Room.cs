﻿using HotelManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Domain
{
    public class Room
    {
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public int RoomNumber { get; set; }
        public int NumberOfBeds { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Workorder> Workorders { get; set; }


        public Room() { }

        public Room(RoomModel model)
        {
            this.Update(model);
        }

        public void Update(RoomModel model)
        {
            RoomId = model.RoomId;
            UserId = model.UserId;
            RoomNumber = model.RoomNumber;
            NumberOfBeds = model.NumberOfBeds;
        }

    }
}
