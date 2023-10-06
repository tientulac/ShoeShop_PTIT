using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.CategoryService
{
    public interface ICategoryService
    {
        List<CategoryDTO> GetAll();
        CategoryDTO GetById(int id);
        void Add(Category entity);
        void Update(Category entity);
        void Remove(int id);
    }
}