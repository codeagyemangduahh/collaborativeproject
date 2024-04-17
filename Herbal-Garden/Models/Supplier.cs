using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models
{
    public class Supplier
    {

        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
    }

    public class SupplierDto
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        

    }
}
