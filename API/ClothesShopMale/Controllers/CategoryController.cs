using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using ShoeShopAPI.Services.CategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoeShopAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/v1/category")]
        public ResponseBase<List<CategoryDTO>> GetList()
        {
            try
            {
                var categories = _categoryService.GetAll();
                return new ResponseBase<List<CategoryDTO>>
                {
                    data = categories,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CategoryDTO>>
                {
                    exMessage = ex.Message,
                    status = 500
                };
            }
        }

        [HttpPost]
        [Route("api/v1/category")]
        public ResponseBase<CategoryDTO> Save(CategoryDTO req)
        {
            try
            {
                var category = _mapper.Map<Category>(req);
                if (req.category_id > 0)
                {
                    _categoryService.Update(category);
                }
                else
                {
                    _categoryService.Add(category);
                }
                return new ResponseBase<CategoryDTO>
                {
                    data = req,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<CategoryDTO>
                {
                    exMessage= ex.Message,
                    status = 500
                };
            }
        }

        [HttpDelete]
        [Route("api/v1/category/{id}")]
        public ResponseBase<bool> Delete(int id = 0)
        {
            try
            {
                _categoryService.Remove(id);
                return new ResponseBase<bool>
                {
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>
                {
                    exMessage = ex.Message,
                    status = 500
                };
            }
        }
    }
}