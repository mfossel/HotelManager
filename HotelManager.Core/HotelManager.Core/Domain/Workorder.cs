﻿using HotelManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Domain
{
    public enum Priorities
    {
        Emergency = 5,
        High = 4,
        Medium = 3,
        Low = 2,
        Routine = 1
    }

    public class Workorder
    {
        public int WorkorderId { get; set; }
        public int RoomId{ get; set; }
        public string UserId { get; set; }
        public Priorities Priority { get; set; }
        public DateTime Date { get; set; }
        public Boolean Closed { get; set; }

        public virtual User User { get; set; }
        public virtual Room Room { get; set; }

        public Workorder() { }
        public Workorder(WorkorderModel model)
        {
            this.Update(model);
        }

        public void Update (WorkorderModel model)
        {
            WorkorderId = model.WorkorderId;
            RoomId = model.RoomId;
            UserId = model.UserId;
            Priority = model.Priority;
            Date = model.Date;
            Closed = model.Closed;
        }

    }
}
