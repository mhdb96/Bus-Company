using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;
using TASLibrary.Models;

namespace TASLibrary.DataAccess
{
    public class PseudoConnector : IDataConnection
    {
        public CLinkedList<BusModel> GetBus_All()
        {
            return BusModel.GetSampleData();
        }

        public CLinkedList<DestinationModel> GetDestination_All()
        {
            return DestinationModel.GetSampleData();
        }

        public CLinkedList<DriverModel> GetDriver_All()
        {
            return DriverModel.GetSampleData();
        }
    }
}
