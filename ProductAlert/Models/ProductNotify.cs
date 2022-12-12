using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductAlert.Models
{
    public class ProductNotify
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public int BatchAlertId { get; set; }
        public BatchAlert BatchAlert { get; set; }
        public ApplicationUser User { get; set; }
    }
}

//e-notify
//mou notice