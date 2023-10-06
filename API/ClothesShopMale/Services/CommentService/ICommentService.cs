using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using System.Collections.Generic;

namespace ShoeShopAPI.Services.CommentService
{
    public interface ICommentService
    {
        List<CommentDTO> GetAll();
        void Add(Comment entity);
        void Remove(int id);
    }
}