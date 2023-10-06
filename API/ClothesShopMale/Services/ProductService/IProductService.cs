using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.ProductService
{
    public interface IProductService
    {
        List<ProductDTO> GetList(ProductDTO req);
        List<ProductDTO> GetByFitler(FilterProduct req);
        void Save(ProductDTO req);
        void Delete(int id = 0);
        List<ColorDto> GetListColor();
        List<sp_ProductLoadListAllResult> GetAllProduct();
        bool CheckStock(ProductAttributeDTO req);
        List<SizeDTO> GetListSize();
    }
}