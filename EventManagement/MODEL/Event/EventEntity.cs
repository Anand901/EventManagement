using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Event
{
    public class EventEntity
    {   
        public string Flag { get; set; }        
        public long EventId { get; set; }   

        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public DateTime EventStartDate { get; set; }

        public DateTime EventEndDate { get; set; }  

        public string EventImage { get; set; }      

        public string AdminEmail { get; set; }  
    }
}
