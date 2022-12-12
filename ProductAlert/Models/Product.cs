using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductAlert.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string AboutProduct { get; set; }
        public DateTime ManufacturerDate { get; set; }
        public DateTime ExpiringDate { get; set; }
        public string BatchNumber { get; set; }
        public string Description { get; set; }

        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string Consequency { get; set; }
        public string Img { get; set; }
    }
}