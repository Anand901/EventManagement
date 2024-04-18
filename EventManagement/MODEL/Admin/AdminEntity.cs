using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Admin
{
    public class AdminEntity
    {
        public long AdminId { get; set; }   
        public string Flag { get; set; }    
        public string AdminName { get; set; }    
        public string AdminAdress{ get; set; } 
        public string AdminEmail { get; set; }    
        public string AdminPhoneNo { get; set; }  
        public string AdminPassword { get; set; }   
            public DateTime CeratedDate { get; set; }

    }
}
