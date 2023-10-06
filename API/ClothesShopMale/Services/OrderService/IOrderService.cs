using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.OrderService
{
    public interface IOrderService
    {
        List<sp_LoadOrderResult> GetAll(OrderDTO req);
        List<sp_LoadOrderResult> GetByFitler(FilterOrder req);
        void Add(Order entity);
        void Cancle(int id = 0);
        void Remove(int id);
        void UpdateStatus(int id = 0, int status = 0);
    }
}