using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Models.DTO
{
    public class ColorDto : BaseEntity
    {
        public int id { get; set; }
        public string color { get; set; }
        public string name { get; set; }
    }
}