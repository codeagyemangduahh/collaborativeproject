using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models.ViewModels
{
    public class DetailsVaporizer
    {

        public VaporizerDto SelectedVaporizer { get; set; }
        public IEnumerable<HerbsDto> CompatibleHerbs { get; set; }

        public IEnumerable<HerbsDto> AvailableHerbs { get; set; }


    }
}