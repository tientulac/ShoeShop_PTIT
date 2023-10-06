using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Models
{
    public class BaseEntity
    {
        public DateTime? created_at {  get; set; }
        public DateTime? updated_at {  get; set; }
        public DateTime? deleted_at {  get; set; }
        public string created_by {  get; set; }
        public string updated_by {  get; set; }
        public string deleted_by {  get; set; }
        public bool? is_delete {  get; set; }
    }
}