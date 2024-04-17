using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Herbal_Garden.Models
{
    public class Herbs
    {
        [Key]
        public int HerbsID { get; set; }
        public string HerbsName { get; set; }

        public string Treatment { get; set; }
        [ForeignKey("Garden")]

        public int GardenID { get; set; }
        public virtual Garden Garden { get; set; }


        public ICollection<Vaporizer> Vaporizers { get; set; }

    }

    public class HerbsDto
    {
        public int HerbsID { get; set; }
        public string HerbsName { get; set; }
        public string Treatment { get; set; }

        public int GardenID { get; set; }
        public string GardenName  { get; set; }
    }
}
