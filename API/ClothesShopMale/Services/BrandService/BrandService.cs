using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShopAPI.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _repository;
        private readonly IMapper _mapper;

        public BrandService(IRepository<Brand> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Add(Brand entity)
        {
            try
            {
                entity.created_at = DateTime.Now;
                _repository.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BrandDTO> GetAll()
        {
            var brands = _repository.GetAll().ToList();
            return _mapper.Map<List<BrandDTO>>(brands);
        }

        public BrandDTO GetById(int id)
        {
            var brand = _repository.GetAll().Where(x => x.brand_id == id).FirstOrDefault();
            return _mapper.Map<BrandDTO>(brand);
        }

        public void Remove(int id)
        {
            try
            {
                var brand = _repository.GetAll().Where(x => x.brand_id == id).FirstOrDefault();
                brand.is_delete = true;
                brand.deleted_at = DateTime.Now;
                _repository.Update(brand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Brand entity)
        {
            try
            {
                var brand = _repository.GetAll().Where(x => x.brand_id == entity.brand_id).FirstOrDefault();
                brand.brand_name = entity.brand_name;
                brand.brand_code = entity.brand_code;
                brand.image = entity.image;
                brand.updated_at = DateTime.Now;
                _repository.Update(brand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}