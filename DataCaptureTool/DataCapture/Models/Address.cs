using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCapture.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string StreetAddress { get; set; }        
    }
}
