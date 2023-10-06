using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using ShoeShopAPI.Services.OrderService;
using ShoeShopAPI.Services.OrderInfoService;

namespace ShoeShopAPI.Controllers
{
    public class OrderInfoController : ApiController
    {
        private readonly IOrderInfoService _orderInfoService;
        private readonly IMapper _mapper;

        public OrderInfoController(IMapper mapper, IOrderInfoService orderInfoService)
        {
            _mapper = mapper;
            _orderInfoService = orderInfoService;
        }


        [HttpPost]
        [Route("api/v1/orderInfor/filter")]
        public ResponseBase<List<Order>> GetList(FilterOrderInfo req)
        {
            try
            {
                return new ResponseBase<List<Order>>
                {
                    data = _orderInfoService.GetList(req),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Order>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/orderInfor")]
        public ResponseBase<Order> Save(Order req)
        {
            try
            {
                _orderInfoService.Save(req);
                return new ResponseBase<Order>
                {
                    data = req,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Order>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/orderInfor/updateItem")]
        public ResponseBase<Order> UpdateItem(OrderInfoDTO req)
        {
            try
            {
                _orderInfoService.UpdateItem(req);
                return new ResponseBase<Order>
                {
                    data = req.Order,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Order>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("api/v1/orderInfor/cancleOrder/{order_infor_id}")]
        public ResponseBase<Order> CancleOrder(int order_infor_id = 0)
        {
            try
            {
                _orderInfoService.CancleOrder(order_infor_id);
                return new ResponseBase<Order>
                {
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Order>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }
        [HttpGet]
        [Route("api/v1/orderInfor/getById/{order_infor_id}")]
        public ResponseBase<Order> GetById(int order_infor_id = 0)
        {
            try
            {

                return new ResponseBase<Order>
                {
                    data = _orderInfoService.GetById(order_infor_id),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Order>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpDelete]
        [Route("api/v1/orderInfor/{id}")]
        public ResponseBase<bool> Delete(int id = 0)
        {
            try
            {
                _orderInfoService.Delete(id);
                return new ResponseBase<bool>
                {
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/orderInfor/updateOrder")]
        public ResponseBase<Order> UpdateOrder(OrderInfoDTO req)
        {
            try
            {
                _orderInfoService.UpdateOrder(req);
                return new ResponseBase<Order>
                {
                    data = req.Order,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Order>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [Route("api/v1/orderInfor/updateOrderItemOnline")]
        public ResponseBase<Order> UpdateOrderItemOnline(OrderInfoDTO req)
        {
            try
            {
                _orderInfoService.UpdateOrderItemOnline(req);
                return new ResponseBase<Order>
                {
                    data = req.Order,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Order>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }
    }
}