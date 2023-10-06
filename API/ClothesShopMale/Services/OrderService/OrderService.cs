using AutoMapper;
using Newtonsoft.Json;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly LinqDataContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(
            IRepository<Order> repository, 
            IMapper mapper, 
            IRepository<ProductAttribute> productAttributeRepository,
            LinqDataContext dbContext)
        {
            _repository = repository;
            _mapper = mapper;
            _productAttributeRepository = productAttributeRepository;
            _dbContext = dbContext;
        }

        public void Add(Order entity)
        {
            try
            {
                var listCartItem = JsonConvert.DeserializeObject<List<ProductAttributeDTO>>(entity.order_item);
                if (listCartItem.Any())
                {
                    listCartItem.ForEach(x =>
                    {
                        var pa = _productAttributeRepository?.GetAll().FirstOrDefault(p => p.product_attribue_id == x.product_attribue_id) ?? null;
                        if (pa != null)
                        {
                            pa.amount -= x.amountCart;
                            _productAttributeRepository.Update(pa);
                        }
                    });
                }
                entity.created_at = DateTime.Now;
                entity.type = 1;
                _repository.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cancle(int id = 0)
        {
            var ord = _repository.GetAll().Where(x => x.order_id == id).FirstOrDefault();
            ord.status = 4;
            ord.deleted_at = DateTime.Now;
            _repository.Update(ord);
        }

        public List<sp_LoadOrderResult> GetAll(OrderDTO req)
        {
            var list = _dbContext.sp_LoadOrder().ToList();

            if (req != null)
            {
                if (!string.IsNullOrEmpty(req.order_code))
                {
                    list = list.Where(x => $"HD00{x.order_id}" == req.order_code).ToList();
                }
                if (!string.IsNullOrEmpty(req.full_name))
                {
                    list = list.Where(x => x.full_name.ToLower().Contains(req.full_name.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(req.phone))
                {
                    list = list.Where(x => x.phone.ToLower().Contains(req.phone.ToLower())).ToList();
                }
                if (req.status > 0)
                {
                    list = list.Where(x => x.status == req.status).ToList();
                }
                if (req.type_payment > 0)
                {
                    list = list.Where(x => x.type_payment == req.type_payment).ToList();
                }
                if (req.created_at != null)
                {
                    list = list.Where(x => x.created_at.HasValue && x.created_at.Value.Date == req.created_at.Value.Date).ToList();
                }
                if (req.deleted_at != null)
                {
                    list = list.Where(x => x.deleted_at.HasValue && x.deleted_at.Value.Date == req.deleted_at.Value.Date).ToList();
                }
            }
            return list;
        }

        public List<sp_LoadOrderResult> GetByFitler(FilterOrder req)
        {
            List<sp_LoadOrderResult> result = _dbContext.sp_LoadOrder().ToList();
            if (req.status > 0)
            {
                result = result.Where(x => x.status == req.status).ToList();
            }
            return result;
        }

        public void Remove(int id)
        {
            var ord = _repository.GetAll().Where(x => x.order_id == id).FirstOrDefault();
            ord.status = 4;
            ord.is_delete = true;
            ord.deleted_at = DateTime.Now;
            _repository.Update(ord);
        }

        public void UpdateStatus(int id = 0, int status = 0)
        {
            try
            {
                var ord = _repository.GetAll().Where(x => x.order_id == id).FirstOrDefault();
                ord.status = status;
                if (status == 4)
                {
                    ord.is_delete = true;
                    ord.deleted_at = DateTime.Now;
                }
                else
                {
                    ord.updated_at = DateTime.Now;
                }
                _repository.Update(ord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}