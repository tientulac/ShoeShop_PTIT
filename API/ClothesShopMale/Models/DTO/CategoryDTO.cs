using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Models.DTO
{
    public class CategoryDTO : BaseEntity
    {
        public int category_id { get; set; }
        public string category_code { get; set; }
        public string category_name { get; set; }
        public int status { get; set; }
        public string image { get; set; }
    }
}