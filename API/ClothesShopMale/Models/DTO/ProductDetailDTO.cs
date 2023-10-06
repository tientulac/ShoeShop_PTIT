using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Models.DTO
{
    public class ProductDetailDTO : BaseEntity
    {
        public int product_detail_id { get; set; }
        public string detail { get; set; }
        public int product_id { get; set; }
    }
}