using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShopAPI.Services.BlogService
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _repository;
        private readonly IMapper _mapper;

        public BlogService(IRepository<Blog> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Add(Blog entity)
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

        public List<Blog> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Blog GetById(int id)
        {
            return _repository.GetAll().Where(x => x.blog_id == id).FirstOrDefault();
        }

        public void Remove(int id)
        {
            try
            {
                var blog = _repository.GetAll().Where(x => x.blog_id == id).FirstOrDefault();
                blog.is_delete = true;
                blog.deleted_at = DateTime.Now;
                _repository.Update(blog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}