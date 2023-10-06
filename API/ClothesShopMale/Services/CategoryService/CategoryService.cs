using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Add(Category entity)
        {
            try
            {
                var category = new Category();
                category.category_name = entity.category_name;
                category.category_code = entity.category_code;
                category.image = entity.image;
                category.created_at = DateTime.Now;
                _repository.Add(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryDTO> GetAll()
        {
            var categories = _repository.GetAll().ToList();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public CategoryDTO GetById(int id)
        {
            var category = _repository.GetAll().Where(x => x.category_id == id).FirstOrDefault();
            return _mapper.Map<CategoryDTO>(category);
        }

        public void Remove(int id)
        {
            try
            {
                var category = _repository.GetAll().Where(x => x.category_id == id).FirstOrDefault();
                category.is_delete = true;
                category.deleted_at = DateTime.Now;
                _repository.Update(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Category entity)
        {
            try
            {
                var category = _repository.GetAll().Where(x => x.category_id == entity.category_id).FirstOrDefault();
                category.category_name = entity.category_name;
                category.category_code = entity.category_code;
                category.image = entity.image;
                category.updated_at = DateTime.Now;
                _repository.Update(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}