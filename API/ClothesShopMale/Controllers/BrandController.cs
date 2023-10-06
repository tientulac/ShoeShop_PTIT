using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Services.BrandService;
using ShoeShopAPI.Services.CategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoeShopAPI.Controllers
{
    public class BrandController : ApiController
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/v1/brand")]
        public ResponseBase<List<BrandDTO>> GetList()
        {
            try
            {
                return new ResponseBase<List<BrandDTO>>
                {
                    data = _brandService.GetAll(),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<BrandDTO>>
                {
                    status = 500
                };
            }
        }

        [HttpPost]
        [Route("api/v1/brand")]
        public ResponseBase<Brand> Save(Brand req)
        {
            try
            {
                if (req.brand_id > 0)
                {
                    _brandService.Update(req);
                }
                else
                {
                    _brandService.Add(req);
                }
                return new ResponseBase<Brand>
                {
                    data = req,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Brand>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpDelete]
        [Route("api/v1/brand/{id}")]
        public ResponseBase<bool> Delete(int id = 0)
        {
            try
            {
                _brandService.Remove(id);
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
    }
}