using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.Models;

namespace TASUI.Requesters
{
    public interface ICreateTripRequester
    {
        void CreateTripFormClosed();
        void TripCreated(TripModel trip);
    }
}
