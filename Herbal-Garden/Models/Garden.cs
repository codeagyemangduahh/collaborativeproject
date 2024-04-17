using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models
{
    public class Garden
    {

        [Key] 
        public int GardenID { get; set; }
        public string GardenName { get; set; }
        public string SoilType { get; set; }
    }

    public class GardenDto
    {

        public int GardenID { get; set; }
        public string GardenName { get; set; }
        public string SoilType { get; set; }


    }

}