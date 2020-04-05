using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASLibrary.Models;

namespace TASUI.Requesters
{
    public interface ICreateSeatRequester
    {
        void CreateSeatFormClosed();
        void SeatCreated(TripModel trip, List<string> logs);
    }
}
