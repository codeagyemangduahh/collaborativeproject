using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models.ViewModels
{
    public class DetailsGarden
    {
        public GardenDto SelectedGarden { get; set; }
        public IEnumerable<HerbsDto> RelatedHerb { get; set; }
    }
}