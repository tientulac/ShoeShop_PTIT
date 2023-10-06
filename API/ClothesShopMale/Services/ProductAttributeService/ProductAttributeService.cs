using AutoMapper;
using Microsoft.Ajax.Utilities;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace ShoeShopAPI.Services.ProductAttributeService
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly IRepository<ProductAttribute> _attRepository;
        private readonly IRepository<ProductDetail> _detailRepository;
        private readonly IRepository<ProductImage> _imgRepository;
        private readonly IMapper _mapper;

        public ProductAttributeService(
            IRepository<ProductAttribute> attRepository,
            IRepository<ProductDetail> detailRepository,
            IRepository<ProductImage> imgRepository,
            IMapper mapper
        )
        {
            _attRepository = attRepository;
            _detailRepository = detailRepository;
            _imgRepository = imgRepository;
            _mapper = mapper;
        }


        public void DeleteAttribute(int id = 0)
        {
            var att = _attRepository.GetAll().Where(x => x.product_attribue_id == id).FirstOrDefault();
            _attRepository.Remove(att);
        }

        public void DeleteDetail(int id = 0)
        {
            var dt = _detailRepository.GetAll().Where(x => x.product_detail_id == id).FirstOrDefault();
            _detailRepository.Remove(dt);

        }

        public void DeleteImage(int id = 0)
        {
            var img = _imgRepository.GetAll().Where(x => x.product_image_id == id).FirstOrDefault();
            _imgRepository.Remove(img);
        }

        public List<ProductAttributeDTO> GetAttribute()
        {
            var list = _attRepository.GetAll().Select(x => new ProductAttributeDTO
            {
                product_attribute_id = x.product_attribue_id,
                size = x.size,
                color = x.color,
                price = x.price.GetValueOrDefault(),
                product_id = x.product_id.GetValueOrDefault(),
                amount = x.amount.GetValueOrDefault()
            }).ToList();
            return list;
        }

        public IEnumerable<ProductDetailDTO> GetListDetail()
        {
            var list = _detailRepository.GetAll().Select(x => new ProductDetailDTO
            {
                product_detail_id = x.product_detail_id,
                detail = x.detail,
                product_id = x.product_id.GetValueOrDefault()
            }).ToList();
            return list;
        }

        public List<ProductImageDTO> GetListImage()
        {
            var list = _imgRepository.GetAll().Select(x => new ProductImageDTO
            {
                product_image_id = x.product_image_id,
                image = x.image,
                product_id = x.product_id.GetValueOrDefault()
            }).ToList();
            return list;
        }

        public void Save(ProductAttribute req)
        {
            try
            {
                if (req.product_attribue_id > 0)
                {
                    var pa = _attRepository.GetAll().FirstOrDefault(x => x.product_attribue_id == req.product_attribue_id);
                    pa.size = req.size;
                    pa.color = req.color;
                    pa.amount = req.amount;
                    pa.price = req.price;
                    _attRepository.Update(pa);
                }
                else
                {
                    _attRepository.Add(req);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveColor(ProductAttribute req)
        {
            try
            {
                _attRepository.Add(req);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveDetail(ProductDetail req)
        {
            try
            {
                _detailRepository.Add(req);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveImage(ProductImage req)
        {
            try
            {
                _imgRepository.Add(req);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveImageProduct(ProductImageDTO req)
        {
            try
            {
                var listPImage = _imgRepository.GetAll().Where(x => x.product_id == req.product_id);
                listPImage.ForEach(x =>
                {
                    _imgRepository.Remove(x);
                });
                req.list_image_checked.ForEach(x =>
                {
                    _imgRepository.Add(new ProductImage
                    {
                        image = x,
                        product_id = req.product_id
                    });
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}