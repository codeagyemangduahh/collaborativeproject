using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models.ViewModels
{
    public class DetailsSupplier
    {

        public SupplierDto SelectedSupplier { get; set; }
        public IEnumerable<VaporizerDto> RelatedVaporizer { get; set; }

    }
}