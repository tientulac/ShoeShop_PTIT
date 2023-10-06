using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.BrandService
{
    public interface IBrandService
    {
        List<BrandDTO> GetAll();
        BrandDTO GetById(int id);
        void Add(Brand entity);
        void Update(Brand entity);
        void Remove(int id);
    }
}