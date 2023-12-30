using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2Database.DataModels
{
    public class Books
    {
        [Key]
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }

        [ForeignKey("Authors")]
        public int AuthorID { get; set; }

        public int Pages { get; set; }
        public string Genre { get; set; }

        public virtual ICollection<StockBalance> StockBalance { get; set; }

        public virtual Authors Authors { get; set; }
    }
}
