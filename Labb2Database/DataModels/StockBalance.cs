using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2Database.DataModels
{
    public class StockBalance
    {
        [Key]
        public int StoreID { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ISBN")]
        public virtual Books Books { get; set; }

        [ForeignKey("StoreID")]
        public virtual Stores Stores { get; set; }
    }
}
