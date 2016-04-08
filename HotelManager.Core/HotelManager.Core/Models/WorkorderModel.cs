using HotelManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Models
{
    class WorkorderModel
    {
        public int RoomNumber { get; set; }
        public Priorities Priority { get; set; }
        public DateTime Date { get; set; }
        public Boolean Closed { get; set; }

    }
}
