using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.Models;
using TASLibrary.CustomDataStructures;
using TASLibrary.Enums;

namespace TASLibrary.DataAccess
{
    public interface IDataConnection
    {
        Logger Logger { get; set; }
        CLinkedList<BusModel> GetBus_All();
        CLinkedList<DestinationModel> GetDestination_All();
        CLinkedList<DriverModel> GetDriver_All();
        CLinkedList<TripModel> GetTrip_All(DateTime date);
        void Trip_InsertAll(CLinkedList<TripModel> trips, DateTime selectedDate);
        int GetDbInfo(DbInfo info);
        void UpdateDbInfo(DbInfo info, int number);        
    }
}
