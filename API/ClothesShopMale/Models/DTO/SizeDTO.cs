using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Models.DTO
{
    public class SizeDTO : BaseEntity
    {
        public int id { get; set; }
        public string size { get; set; }
        public string name { get; set; }
    }
}