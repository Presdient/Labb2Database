using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2Database.DataModels
{
    public class Stores
    {
        [Key]
        public int ID { get; set; }
        public string StoreName { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public virtual ICollection<StockBalance> StockBalance { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
