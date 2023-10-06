using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Services.BlogService;
using ShoeShopAPI.Services.CategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ShoeShopAPI.Controllers
{
    public class BlogController : ApiController
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/v1/blog")]
        public ResponseBase<List<Blog>> GetList()
        {
            try
            {
                return new ResponseBase<List<Blog>>
                {
                    data = _blogService.GetAll(),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Blog>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/blog")]
        public ResponseBase<Blog> Save(Blog req)
        {
            try
            {
                _blogService.Add(req);
                return new ResponseBase<Blog>
                {
                    data = req,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Blog>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpDelete]
        [Route("api/v1/blog/{id}")]
        public ResponseBase<bool> Delete(int id = 0)
        {
            try
            {
                _blogService.Remove(id);
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
        [Route("api/v1/blog/{id}")]
        public ResponseBase<Blog> FindById(int id = 0)
        {
            try
            {
                return new ResponseBase<Blog>
                {
                    data = _blogService.GetById(id),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Blog>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }
    }
}