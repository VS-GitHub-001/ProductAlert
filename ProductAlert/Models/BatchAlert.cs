using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductAlert.Models
{
    public class BatchAlert
    {
        public int Id { get; set; }
        public string Batch { get; set; }
        public ICollection<ProductNotify> ProductNotifies { get; set; }
    }
}