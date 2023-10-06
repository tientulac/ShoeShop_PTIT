using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.ProductAttributeService
{
    public interface IProductAttributeService
    {
        List<ProductAttributeDTO> GetAttribute();
        void Save(ProductAttribute req);
        void SaveImageProduct(ProductImageDTO req);
        IEnumerable<ProductDetailDTO> GetListDetail();
        List<ProductImageDTO> GetListImage();
        void SaveColor(ProductAttribute req);
        void SaveDetail(ProductDetail req);
        void SaveImage(ProductImage req);
        void DeleteAttribute(int id = 0);
        void DeleteDetail(int id = 0);
        void DeleteImage(int id = 0);
    }
}