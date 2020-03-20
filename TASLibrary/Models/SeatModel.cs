using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.Enums;

namespace TASLibrary.Models
{
    public class SeatModel
    {
        public int No { get; set; }
        public PassengerModel Passenger { get; set; }
        public SeatStatus Status { get; set; }
    }
}
