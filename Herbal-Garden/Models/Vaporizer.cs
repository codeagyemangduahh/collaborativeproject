using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models
{
    public class Vaporizer
    {
        [Key]
        public int VaporizerID { get; set; }
        public string VaporizerName { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }

        //a vaporizer is compatible with many herbs
        public ICollection<Herbs> Herbss { get; set; }
    }

    public class VaporizerDto
    {
        public int VaporizerID { get; set; }
        public string VaporizerName { get; set; }

        //weight is in kg
        

        public int SupplierID { get; set; }
        public string SupplierName { get; set; }

        

    }

}
