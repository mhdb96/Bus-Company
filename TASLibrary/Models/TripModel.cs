using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASLibrary.Models
{
    public class TripModel
    {
        public int No { get; set; }
        public DestinationModel Destination { get; set; }
        public DateTime Date { get; set; }
        public BusModel Bus { get; set; }
        public DriverModel Driver { get; set; }
        public decimal SeatPrice { get; set; }
        //public int Seats { get; set; }

    }
}
