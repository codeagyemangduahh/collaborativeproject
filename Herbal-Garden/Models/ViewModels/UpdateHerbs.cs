using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models.ViewModels
{
    public class UpdateHerbs
    {

        //This viewmodel is a class which stores information that we need to present to /Herbs/Update/{}

        //the existing Herbs Information

        public HerbsDto SelectedHerb { get; set; }

        // all Gardens to choose from when updating this herb

        public IEnumerable<GardenDto> GardenOptions { get; set; }




    }
}