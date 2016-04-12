using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Core.Models
{
    public class RoomModel
    {
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public int RoomNumber { get; set; }
        public int NumberOfBeds { get; set; }
    }
}
