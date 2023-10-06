using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.OrderInfoService
{
    public interface IOrderInfoService
    {
        List<Order> GetList(FilterOrderInfo req);
        void Save(Order req);
        void UpdateItem(OrderInfoDTO req);
        void CancleOrder(int order_infor_id = 0);
        Order GetById(int order_infor_id = 0);
        void Delete(int id = 0);
        void UpdateOrder(OrderInfoDTO req);
        void UpdateOrderItemOnline(OrderInfoDTO req);
    }
}