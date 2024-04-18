using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.UserEntity
{
    public class UserEntity
    {
        public long UserId { get; set; }
        public string Flag { get; set; }        
        public string UserName { get; set; }
        public string UserAdress { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNo { get; set; }
        public string UserPassword { get; set; }
        public DateTime CeratedDate { get; set; }
    }
}
