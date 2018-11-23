using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.Models
{
    [Table("vendors")]
    public class Vendors
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Company Name")]
        public string VendorCopName { get; set; }

        [Display(Name ="Address")]
        public string HQAddress { get; set; }

        [Display(Name ="Tel")]
        public string Tel { get; set; }

        [Display(Name ="Manager Name")]
        public string HeadPersonName { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastEdited { get; set; }
    }
}
