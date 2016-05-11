using HotelManager.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Domain
{
    public class User : IUser<string>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string HotelName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Workorder> Workorders { get; set; }

    }
}
