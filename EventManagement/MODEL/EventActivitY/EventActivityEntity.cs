using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.EventActivitY
{
    public class EventActivityEntity
    {
        public long ActivityId {  get; set; }  
        public string Flag { get; set; }    
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime ActivityStartTime { get; set; } 
        public DateTime ActivityEndTime { get; set; } 

        public long ActivityPrice { get; set; }    

        public string EventName { get; set; }   

        public long EventId { get; set; }   
    }
}
