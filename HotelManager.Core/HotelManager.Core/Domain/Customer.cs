using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string TelephoneNumber { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} + {LastName}";

        public virtual User User { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
