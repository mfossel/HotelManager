using HotelManager.Core.Models;
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

        public Customer() { }

        public Customer(CustomerModel model)
        {
            this.Update(model);
        }

        public void Update(CustomerModel model)
        {
            CustomerId = model.CustomerId;
            UserId = model.UserId;
            FirstName = model.FirstName;
            LastName = model.LastName;
            EmailAddress = model.EmailAddress;
            TelephoneNumber = model.TelephoneNumber;

        }

    }
}
