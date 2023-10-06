using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Models
{
    public class ResponseBase<T>
    {
        public T data { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public string exMessage { get; set; }
    }
}