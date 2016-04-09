using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Domain
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TelephoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        //TODO add number of rooms and other properties to Hotel info, make dashboard for hotel
    }
}
