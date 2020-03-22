using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.CustomDataStructures;
using TASLibrary.Models;

namespace TASLibrary.DataAccess
{
    public class TextFileConnector : IDataConnection
    {
        public CLinkedList<BusModel> GetBus_All()
        {
            throw new NotImplementedException();
        }

        public CLinkedList<DestinationModel> GetDestination_All()
        {
            throw new NotImplementedException();
        }

        public CLinkedList<DriverModel> GetDriver_All()
        {
            throw new NotImplementedException();
        }
    }
}
