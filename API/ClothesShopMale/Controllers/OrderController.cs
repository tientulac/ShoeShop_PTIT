using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using ShoeShopAPI.Services.AccountService;
using ShoeShopAPI.Services.OrderService;

namespace ShoeShopAPI.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        [Route("api/v1/order/get-list")]
        public ResponseBase<List<sp_LoadOrderResult>> GetList(OrderDTO req)
        {
            try
            {
                return new ResponseBase<List<sp_LoadOrderResult>>
                {
                    data = _orderService.GetAll(req),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<sp_LoadOrderResult>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/order")]
        public ResponseBase<bool> Save(Order req)
        {
            try
            {
                _orderService.Add(req);
                return new ResponseBase<bool>
                {
                    data = true,
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

        [HttpGet]
        [Route("api/v1/order/cancle/{id}")]
        public ResponseBase<bool> Cancle(int id = 0)
        {
            try
            {
                _orderService.Cancle(id);
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

        [HttpDelete]
        [Route("api/v1/order/{id}")]
        public ResponseBase<bool> Delete(int id = 0)
        {
            try
            {
                _orderService.Remove(id);
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

        [HttpGet]
        [Route("api/v1/order/updateStatus/{id}/{status}")]
        public ResponseBase<bool> UpdateStatus(int id = 0, int status = 0)
        {
            try
            {
                _orderService.UpdateStatus(id, status);
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
        [Route("api/v1/order/orderByFilter")]
        public ResponseBase<List<sp_LoadOrderResult>> GetByFitler(FilterOrder req)
        {
            try
            {
                return new ResponseBase<List<sp_LoadOrderResult>>
                {
                    data = _orderService.GetByFitler(req),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<sp_LoadOrderResult>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }
    }
}