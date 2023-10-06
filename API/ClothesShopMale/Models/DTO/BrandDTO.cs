using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Models.DTO
{
    public class BrandDTO : BaseEntity
    {
        public int brand_id { get; set; }
        public string brand_code { get; set; }
        public string brand_name { get; set; }
        public int status { get; set; }
        public string image { get; set; }
    }
}