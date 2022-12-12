using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductAlert.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}