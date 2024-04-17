using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models.ViewModels
{
    public class DetailsHerbs
    {

        public HerbsDto SelectedHerbs { get; set; }
        public IEnumerable<VaporizerDto> ComaptibleVaporizer { get; set; }
        public IEnumerable<VaporizerDto> AvailableVaporizer { get; set; }


    }
}