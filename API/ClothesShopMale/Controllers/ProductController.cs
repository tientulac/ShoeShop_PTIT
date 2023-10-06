using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Services.OrderService;
using ShoeShopAPI.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoeShopAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }


        [HttpPost]
        [Route("api/v1/product/get-list")]
        public ResponseBase<List<ProductDTO>> GetList(ProductDTO req)
        {
            try
            {
                return new ResponseBase<List<ProductDTO>>
                {
                    data = _productService.GetList(req),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ProductDTO>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/productByFilter")]
        public ResponseBase<List<ProductDTO>> GetByFitler(FilterProduct req)
        {
            try
            {
                return new ResponseBase<List<ProductDTO>>
                {
                    data = _productService.GetByFitler(req),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ProductDTO>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/product")]
        public ResponseBase<Product> Save(ProductDTO req)
        {
            try
            {
                _productService.Save(req);
                return new ResponseBase<Product>
                {
                    data = req,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Product>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpDelete]
        [Route("api/v1/product/{id}")]
        public ResponseBase<bool> Delete(int id = 0)
        {
            try
            {
                _productService.Delete(id);
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
        [Route("api/v1/product/sizes")]
        public ResponseBase<List<SizeDTO>> GetListSize()
        {
            try
            {
                return new ResponseBase<List<SizeDTO>>
                {
                    data = _productService.GetListSize(),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<SizeDTO>>
                {
                    status = 500
                };
            }
        }

        [HttpGet]
        [Route("api/v1/product/colors")]
        public ResponseBase<List<ColorDto>> GetListColor()
        {
            try
            {
                return new ResponseBase<List<ColorDto>>
                {
                    data = _productService.GetListColor(),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ColorDto>>
                {
                    status = 500
                };
            }
        }

        [HttpGet]
        [Route("api/v1/product_all")]
        public ResponseBase<List<sp_ProductLoadListAllResult>> GetAllProduct()
        {
            try
            {
                return new ResponseBase<List<sp_ProductLoadListAllResult>>
                {
                    data = _productService.GetAllProduct(),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<sp_ProductLoadListAllResult>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/product/check_stock")]
        public ResponseBase<bool> CheckStock(ProductAttributeDTO req)
        {
            try
            {
                return new ResponseBase<bool>
                {
                    data = _productService.CheckStock(req),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>
                {
                    data = false,
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }
    }
}