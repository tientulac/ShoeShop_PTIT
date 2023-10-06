using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Services.CategoryService;
using ShoeShopAPI.Services.CommentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoeShopAPI.Controllers
{
    public class CommentController : ApiController
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/v1/comment")]
        public ResponseBase<List<CommentDTO>> GetList()
        {
            try
            {
                return new ResponseBase<List<CommentDTO>>
                {
                    data = _commentService.GetAll(),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<CommentDTO>>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/v1/comment")]
        public ResponseBase<Comment> Save(Comment req)
        {
            try
            {
                _commentService.Add(req);
                return new ResponseBase<Comment>
                {
                    data = req,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Comment>
                {
                    status = 500,
                    exMessage = ex.Message
                };
            }
        }

        [HttpDelete]
        [Route("api/v1/comment/{id}")]
        public ResponseBase<bool> Delete(int id = 0)
        {
            try
            {
                _commentService.Remove(id);
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