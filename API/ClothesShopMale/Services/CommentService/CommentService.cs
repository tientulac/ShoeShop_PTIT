using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShopAPI.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _repository;
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public CommentService(IRepository<Comment> repository, IRepository<Account> accountRepository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public void Add(Comment entity)
        {
            try
            {
                entity.created_at = DateTime.Now;
                _repository.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommentDTO> GetAll()
        {
            var list = (from a in _repository.GetAll()
                        select new CommentDTO
                        {
                            comment_id = a.comment_id,
                            account_id = a.account_id,
                            comment1 = a.comment1,
                            created_at = a.created_at,
                            product_id = a.product_id,
                            star = a.star,
                            user_name = _accountRepository.GetAll().Where(x => x.account_id == a.account_id).FirstOrDefault().user_name ?? ""
                        }).ToList();
            return list;
        }

        public void Remove(int id)
        {
            try
            {
                var cmt = _repository.GetAll().Where(x => x.comment_id == id).FirstOrDefault();
                _repository.Remove(cmt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}