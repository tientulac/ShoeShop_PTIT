using AutoMapper;
using Newtonsoft.Json;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShopAPI.Services.OrderInfoService
{
    public class OrderInfoService : IOrderInfoService
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IMapper _mapper;

        public OrderInfoService(
                IRepository<Order> repository,
                IMapper mapper,
                IRepository<ProductAttribute> productAttributeRepository
        )
        {
            _repository = repository;
            _mapper = mapper;
            _productAttributeRepository = productAttributeRepository;
        }

        public void CancleOrder(int order_infor_id = 0)
        {
            try
            {
                var order = _repository.GetAll().Where(x => x.order_id == order_infor_id).FirstOrDefault();
                order.status = 4;
                var listCartItem = JsonConvert.DeserializeObject<List<ProductAttributeDTO>>(order.order_item);
                if (listCartItem.Any())
                {
                    listCartItem.ForEach(x =>
                    {
                        var pa = _productAttributeRepository.GetAll()?.FirstOrDefault(p => p.product_attribue_id == x.product_attribue_id) ?? null;
                        if (pa != null)
                        {
                            pa.amount += x.amountCart;
                            _productAttributeRepository.Update(pa);
                        }
                    });
                }
                _repository.Update(order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id = 0)
        {
            try
            {
                var ord = _repository.GetAll().Where(x => x.order_id == id).FirstOrDefault();
                ord.status = 4;
                ord.is_delete = true;
                ord.deleted_at = DateTime.Now;
                _repository.Update(ord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Order GetById(int order_infor_id = 0)
        {
            return _repository.GetAll().Where(x => x.order_id == order_infor_id).FirstOrDefault();
        }

        public List<Order> GetList(FilterOrderInfo req)
        {
            var list = _repository.GetAll().Where(x => x.type == 2).ToList();
            if (req != null)
            {
                if (!String.IsNullOrEmpty(req.order_code))
                {
                    list = list.Where(x => x.order_code.ToLower().Contains(req.order_code)).ToList();
                }
                if (req.from_date != null)
                {
                    list = list.Where(x => x.created_at >= req.from_date).ToList();
                }
                if (req.to_date != null)
                {
                    list = list.Where(x => x.created_at <= req.to_date).ToList();
                }
            }
            return list;
        }

        public void Save(Order req)
        {
            var listCartItem = JsonConvert.DeserializeObject<List<ProductAttributeDTO>>(req.order_item);
            if (listCartItem.Any())
            {
                listCartItem.ForEach(x =>
                {
                    var pa = _productAttributeRepository.GetAll()?.FirstOrDefault(p => p.product_attribue_id == x.product_attribue_id) ?? null;
                    if (pa != null)
                    {
                        pa.amount -= x.amountCart;
                        _productAttributeRepository.Update(pa);
                    }
                });
            }
            if (req.order_id > 0)
            {
                var order = _repository.GetAll().Where(x => x.order_id == req.order_id).FirstOrDefault();
                order.phone = req.phone;
                order.cusomter_type = req.cusomter_type;
                order.full_name = req.full_name;
                order.seller = req.seller;
                order.phone_seller = req.seller;
                order.id_city = req.id_city;
                order.id_district = req.id_district;
                order.id_ward = req.id_ward;
                order.address = req.address;
                order.waiting = req.waiting;
                order.status = req.status;
                order.note = req.note;
                order.updated_at = DateTime.Now;
                order.type = 2;
                _repository.Update(order);
            }
            else
            {
                req.created_at = DateTime.Now;
                _repository.Add(req);
            }
        }

        public void UpdateItem(OrderInfoDTO req)
        {
            var listCartItem = JsonConvert.DeserializeObject<List<ProductAttributeDTO>>(req.Order.order_item);

            if (req.Order.order_id > 0)
            {
                var order = _repository.GetAll().Where(x => x.order_id == req.Order.order_id).FirstOrDefault();
                order.order_item = req.Order.order_item;
                order.total = req.Order.total;
                _repository.Update(order);

                foreach (var item in JsonConvert.DeserializeObject<List<ProductAttributeDTO>>(req.ListAllProduct))
                {
                    var pa = _productAttributeRepository.GetAll().FirstOrDefault(x => x.product_attribue_id == item.product_attribue_id);
                    pa.amount = item.amount;
                    _productAttributeRepository.Update(pa);
                }
            }
        }

        public void UpdateOrder(OrderInfoDTO req)
        {
            var listCartItem = JsonConvert.DeserializeObject<List<ProductAttributeDTO>>(req.Order.order_item);
            var listCartItemDB = new List<ProductAttributeDTO>();
            var _orderItem = _repository.GetAll().FirstOrDefault(x => x.order_id == req.Order.order_id).order_item;

            if (req.Order.order_id > 0)
            {
                if (!string.IsNullOrEmpty(_orderItem))
                {
                    listCartItemDB = JsonConvert.DeserializeObject<List<ProductAttributeDTO>>(_orderItem);
                    foreach (var item in listCartItemDB)
                    {
                        var pa = _productAttributeRepository.GetAll().FirstOrDefault(x => x.product_attribue_id == item.product_attribue_id);
                        pa.amount += item.amountCart;
                        _productAttributeRepository.Update(pa);
                    }
                }

                var order = _repository.GetAll().Where(x => x.order_id == req.Order.order_id).FirstOrDefault();
                order.order_item = JsonConvert.SerializeObject(listCartItem);
                order.total = listCartItem.Sum(x => x.amountCart * x.price);
                _repository.Update(order);

                foreach (var item in JsonConvert.DeserializeObject<List<ProductAttributeDTO>>(req.Order.order_item))
                {
                    var pa = _productAttributeRepository.GetAll().FirstOrDefault(x => x.product_attribue_id == item.product_attribue_id);
                    pa.amount -= item.amountCart;
                    _productAttributeRepository.Update(pa);
                }
            }
        }

        public void UpdateOrderItemOnline(OrderInfoDTO req)
        {
            var listCartItem = JsonConvert.DeserializeObject<List<ProductAttributeDTO>>(req.Order.order_item);
            if (!listCartItem.Where(item => item.amount_bought + item.amount - item.amountCart > 0).Any())
            {
                throw new Exception("Số lượng muốn mua > số lượng trong kho. Cập nhật giỏ hàng thất bại !");
            }
            foreach (var item in listCartItem)
            {
                var pa = _productAttributeRepository.GetAll().FirstOrDefault(x => x.product_attribue_id == item.product_attribue_id);
                pa.amount = item.amount_bought + item.amount - item.amountCart;
                item.amount = pa.amount.Value;
                _productAttributeRepository.Update(pa);
            }
            var order = _repository.GetAll().Where(x => x.order_id == req.Order.order_id).FirstOrDefault();
            order.order_item = JsonConvert.SerializeObject(listCartItem);
            order.total = listCartItem.Sum(x => x.amountCart * x.price);
            _repository.Update(order);
        }
    }
}