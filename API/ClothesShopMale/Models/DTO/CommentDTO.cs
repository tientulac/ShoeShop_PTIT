using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Models.DTO
{
    public class CommentDTO: Comment
    {
        public string user_name { get; set; }
    }
}