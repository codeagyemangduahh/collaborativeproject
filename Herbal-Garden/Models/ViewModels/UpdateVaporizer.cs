using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models.ViewModels
{
    public class UpdateVaporizer
    {

        //This viewmodel is a class which stores information that we need to present to /Vaporizer/Update/{}

        //the existing Vaporizer information

        public VaporizerDto SelectedVaporizer { get; set; }

        // all suppliers to choose from when updating this animal

        public IEnumerable<SupplierDto> SupplierOptions { get; set; }


    }
}