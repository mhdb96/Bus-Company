using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.Models;
using TASLibrary.CustomDataStructures;

namespace TASLibrary.DataAccess
{
    public interface IDataConnection
    {
        CLinkedList<BusModel> GetBus_All();
        CLinkedList<DestinationModel> GetDestination_All();
        CLinkedList<DriverModel> GetDriver_All();
        CLinkedList<TripModel> GetTrip_All();
        void Trip_InsertAll(CLinkedList<TripModel> trips);
    }
}
