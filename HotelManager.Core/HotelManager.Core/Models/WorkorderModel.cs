using HotelManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Models
{
    public class WorkorderModel
    {
        public int WorkorderId { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public Priorities Priority { get; set; }
        public DateTime Date { get; set; }
        public Boolean Closed { get; set; }

    }
}
