using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<ProductAttribute> _attRepository;
        private readonly LinqDataContext _context;
        private readonly IMapper _mapper;

        public ProductService(
            IRepository<Product> repository,
            IMapper mapper,
            IRepository<ProductAttribute> attRepository,
            LinqDataContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _attRepository = attRepository;
            _context = context;
        }

        public bool CheckStock(ProductAttributeDTO req)
        {
            try
            {
                var _att = _attRepository.GetAll().FirstOrDefault(x => x.product_attribue_id == req.product_attribue_id);
                if (_att.amount - req.amountCart < 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id = 0)
        {
            var p = _context.Products.Where(x => x.product_id == id).FirstOrDefault();
            p.deleted_at = DateTime.Now;
            p.is_delete = true;
            _context.SubmitChanges();
            var att = _context.ProductAttributes.Where(x => x.product_id == id);
            _context.ProductAttributes.DeleteAllOnSubmit(att);
            _context.SubmitChanges();
            var dt = _context.ProductDetails.Where(x => x.product_id == id);
            _context.ProductDetails.DeleteAllOnSubmit(dt);
            _context.SubmitChanges();
            var img = _context.ProductImages.Where(x => x.product_id == id);
            _context.ProductImages.DeleteAllOnSubmit(img);
            _context.SubmitChanges();
        }

        public List<sp_ProductLoadListAllResult> GetAllProduct()
        {
            return _context.sp_ProductLoadListAll().ToList();
        }

        public List<ProductDTO> GetByFitler(FilterProduct req)
        {
            var list = (from a in _repository.GetAll()
                        select new ProductDTO
                        {
                            product_id = a.product_id,
                            brand_id = a.brand_id,
                            category_id = a.category_id,
                            origin = a.origin,
                            product_name = a.product_name,
                            status = a.status,
                            created_at = a.created_at,
                            updated_at = a.updated_at,
                            deleted_at = a.deleted_at,
                            product_code = a.product_code,
                            category_name = _context.Categories.Where(x => x.category_id == a.category_id).FirstOrDefault().category_name ?? "",
                            brand_name = _context.Brands.Where(x => x.brand_id == a.brand_id).FirstOrDefault().brand_name ?? "",
                            is_delete = a.is_delete
                        }).ToList();
            if (!string.IsNullOrEmpty(req.fitlerPrice))
            {
                if (req.fitlerPrice.Equals("gt500"))
                {
                    var listGT500 = _context.ProductAttributes.Where(x => x.price > 500000).Select(p => p.product_id);
                    if (listGT500.Any())
                    {
                        list = list.Where(x => listGT500.Any(p => p.GetValueOrDefault() == x.product_id)).ToList();
                    }
                }
                if (req.fitlerPrice.Equals("lt500"))
                {
                    var listLT500 = _context.ProductAttributes.Where(x => x.price < 500000).Select(p => p.product_id);
                    if (listLT500.Any())
                    {
                        list = list.Where(x => listLT500.Any(p => p.GetValueOrDefault() == x.product_id)).ToList();
                    }
                }
                if (req.fitlerPrice.Equals("gt1000"))
                {
                    var listGT1000 = _context.ProductAttributes.Where(x => x.price > 1000000).Select(p => p.product_id);
                    if (listGT1000.Any())
                    {
                        list = list.Where(x => listGT1000.Any(p => p.GetValueOrDefault() == x.product_id)).ToList();
                    }
                }
            }
            if (req.brand_id > 0)
            {
                list = list.Where(x => x.brand_id == req.brand_id).ToList();
            }
            if (req.category_id > 0)
            {
                list = list.Where(x => x.category_id == req.category_id).ToList();
            }
            return list;
        }

        public List<ProductDTO> GetList(ProductDTO req)
        {
            var list = (from a in _repository.GetAll()
                        select new ProductDTO
                        {
                            product_id = a.product_id,
                            brand_id = a.brand_id,
                            category_id = a.category_id,
                            origin = a.origin,
                            product_name = a.product_name,
                            status = a.status,
                            created_at = a.created_at,
                            updated_at = a.updated_at,
                            deleted_at = a.deleted_at,
                            product_code = a.product_code,
                            category_name = _context.Categories.Where(x => x.category_id == a.category_id).FirstOrDefault().category_name ?? "",
                            brand_name = _context.Brands.Where(x => x.brand_id == a.brand_id).FirstOrDefault().brand_name ?? "",
                            is_delete = a.is_delete
                        }).ToList();
            foreach (var p in list)
            {
                if (_context.ProductAttributes.Where(x => x.product_id == p.product_id).Any())
                {
                    p.min_price = _context.ProductAttributes.Where(x => x.product_id == p.product_id).OrderBy(m => m.price).FirstOrDefault().price.GetValueOrDefault();
                    p.max_price = _context.ProductAttributes.Where(x => x.product_id == p.product_id).OrderByDescending(m => m.price).FirstOrDefault().price.GetValueOrDefault();
                }
                else
                {
                    p.min_price = 0;
                    p.max_price = 0;
                }
            }
            if (req != null)
            {
                if (!String.IsNullOrEmpty(req.product_code))
                {
                    list = list.Where(x => x.product_code.Contains(req.product_code)).ToList();
                }
                if (!String.IsNullOrEmpty(req.product_name))
                {
                    list = list.Where(x => x.product_name.ToLower().Contains(req.product_name.ToLower())).ToList();
                }
                if (req.category_id != null)
                {
                    list = list.Where(x => x.category_id == req.category_id).ToList();
                }
                if (req.brand_id != null)
                {
                    list = list.Where(x => x.brand_id == req.brand_id).ToList();
                }
                if (!String.IsNullOrEmpty(req.fitler_price))
                {
                    if (req.fitler_price.Equals("gt500"))
                    {
                        list = list.Where(x => x.price > 500000).ToList();
                    }
                    if (req.fitler_price.Equals("lt500"))
                    {
                        list = list.Where(x => x.price < 500000).ToList();
                    }
                    if (req.fitler_price.Equals("gt1000"))
                    {
                        list = list.Where(x => x.price > 1000000).ToList();
                    }
                }
            }
            return list;
        }

        public List<ColorDto> GetListColor()
        {
            var list = _context.ProductAttributes.Select(x => x.color).ToList() ?? new List<string>();
            var listString = new List<string>();
            var listResult = new List<ColorDto>();
            var count = 0;
            if (list.Count > 0)
            {
                list.ForEach(x =>
                {
                    var size = x.Split(',');
                    listString.AddRange(size);
                });
            }

            if (listString.Count > 0)
            {
                foreach (var str in listString.Distinct())
                {
                    count++;
                    listResult.Add(new ColorDto
                    {
                        id = count,
                        name = str,
                        color = str
                    });
                }
            }
            return listResult.Distinct().OrderBy(x => x.color).ToList();
        }

        public List<SizeDTO> GetListSize()
        {
            var list = _context.ProductAttributes.Select(x => x.size).ToList() ?? new List<string>();
            var listString = new List<string>();
            var listResult = new List<SizeDTO>();
            var count = 0;
            if (list.Count > 0)
            {
                list.ForEach(x =>
                {
                    var size = x.Split(',');
                    listString.AddRange(size);
                });
            }

            if (listString.Count > 0)
            {
                foreach (var str in listString.Distinct())
                {
                    count++;
                    listResult.Add(new SizeDTO
                    {
                        id = count,
                        name = str,
                        size = str
                    });
                }
            }
            return listResult.Distinct().OrderBy(x => x.size).ToList();
        }

        public void Save(ProductDTO req)
        {
            if (req.product_id > 0)
            {
                var product = _repository.GetAll().Where(x => x.product_id == req.product_id).FirstOrDefault();
                product.brand_id = req.brand_id;
                product.category_id = req.category_id;
                product.origin = req.origin;
                product.product_name = req.product_name;
                product.updated_at = DateTime.Now;
                product.product_code = req.product_code;
                _repository.Update(product);
            }
            else
            {
                var _product = new Product();
                _product.brand_id = req.brand_id;
                _product.category_id = req.category_id;
                _product.origin = req.origin;
                _product.product_name = req.product_name;
                _product.status = 1;
                _product.created_at = DateTime.Now;
                _product.product_code = req.product_code;
                _product.status = 1;
                _repository.Add(_product);
            }
        }
    }
}