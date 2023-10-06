using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Services.ProductAttributeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ShoeShopAPI.Controllers
{
    public class ProductAttributeController : ApiController
    {
        private readonly IProductAttributeService _productAttributeService;
        private readonly IMapper _mapper;

        public ProductAttributeController(IMapper mapper, IProductAttributeService productAttributeService)
        {
            _mapper = mapper;
            _productAttributeService = productAttributeService;
        }

        [HttpGet]
        [Route("api/v1/productattribute")]
        public ResponseBase<List<ProductAttributeDTO>> GetAttribute()
        {
            try
            {
                return new ResponseBase<List<ProductAttributeDTO>>
                {
                    data = _productAttributeService.GetAttribute(),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ProductAttributeDTO>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/productattribute/save")]
        public ResponseBase<bool> Save(ProductAttribute req)
        {
            try
            {
                _productAttributeService.Save(req);
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

        [HttpPost]
        [Route("api/v1/productattribute/save-image-product")]
        public ResponseBase<bool> SaveImageProduct(ProductImageDTO req)
        {
            try
            {
                _productAttributeService.SaveImageProduct(req);
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
        [Route("api/v1/productattribute/detail")]
        public ResponseBase<IEnumerable<ProductDetailDTO>> GetListDetail()
        {
            try
            {
                return new ResponseBase<IEnumerable<ProductDetailDTO>>
                {
                    data = _productAttributeService.GetListDetail(),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<IEnumerable<ProductDetailDTO>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("api/v1/productattribute/image")]
        public ResponseBase<List<ProductImageDTO>> GetListImage()
        {
            try
            {
                return new ResponseBase<List<ProductImageDTO>>
                {
                    data = _productAttributeService.GetListImage(),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ProductImageDTO>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/productattribute")]
        public ResponseBase<ProductAttribute> SaveColor(ProductAttribute req)
        {
            try
            {
                _productAttributeService.SaveColor(req);
                return new ResponseBase<ProductAttribute>
                {
                    data = req,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ProductAttribute>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/productattribute/detail/save")]
        public ResponseBase<bool> SaveDetail(ProductDetail req)
        {
            try
            {
                _productAttributeService.SaveDetail(req);
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

        [HttpPost]
        [Route("api/v1/productattribute/image")]
        public ResponseBase<ProductImage> SaveImage(ProductImage req)
        {
            try
            {
                _productAttributeService.SaveImage(req);
                return new ResponseBase<ProductImage>
                {
                    data = req,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ProductImage>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpDelete]
        [Route("api/v1/productattribute/{id}")]
        public ResponseBase<bool> DeleteAttribute(int id = 0)
        {
            try
            {
                _productAttributeService.DeleteAttribute(id);
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
        [Route("api/v1/productattribute/detail/{id}")]
        public ResponseBase<bool> DeleteDetail(int id = 0)
        {
            try
            {
                _productAttributeService.DeleteDetail(id);
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
        [Route("api/v1/productattribute/image/{id}")]
        public ResponseBase<bool> DeleteImage(int id = 0)
        {
            try
            {
                _productAttributeService.DeleteImage(id);
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