using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace ShoeShopAPI.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _repository;
        private readonly IMapper _mapper;
        private readonly LinqDataContext _dbContext;

        public AccountService(IRepository<Account> repository, IMapper mapper, LinqDataContext dbContext)
        {
            _repository = repository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public List<sp_Account_LoadResult> GetAll(AccountDTO req)
        {
            var list = _dbContext.sp_Account_Load().ToList();
            if (req != null)
            {
                if (!string.IsNullOrEmpty(req.user_name))
                {
                    list = list.Where(x => x.user_name.ToLower().Contains(req.user_name)).ToList();
                }
                if (!string.IsNullOrEmpty(req.phone))
                {
                    list = list.Where(x => x.phone.Contains(req.phone)).ToList();
                }
                if (!string.IsNullOrEmpty(req.full_name))
                {
                    list = list.Where(x => x.full_name.ToLower().Contains(req.full_name)).ToList();
                }
                if (!string.IsNullOrEmpty(req.email))
                {
                    list = list.Where(x => x.email.ToLower().Contains(req.email)).ToList();
                }
            }
            return list;
        }

        public Account GetById(int id)
        {
            return _repository.GetAll().Where(x => x.account_id == id).FirstOrDefault();
        }

        public AccountDTO Login(Account entity)
        {
            try
            {
                var acc = (from a in _repository.GetAll().Where(x => x.user_name == entity.user_name && x.password == entity.password)
                           select new AccountDTO
                           {
                               account_id = a.account_id,
                               user_name = a.user_name,
                               active = a.active,
                               admin = a.admin,
                               avatar = a.avatar,
                               full_name = a.full_name,
                               phone = a.phone,
                               role_code = a.role_code,
                               email = a.email,
                               town = a.town,
                               district = a.district,
                               city = a.city,
                               token = createToken(a.user_name)
                           }).FirstOrDefault();
                return acc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Account Register(Account entity)
        {
            try
            {
                entity.created_at = DateTime.Now;
                _repository.Add(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remove(int id)
        {
            try
            {
                var acc = _repository.GetAll().Where(x => x.account_id == id).FirstOrDefault();
                acc.is_delete = true;
                acc.deleted_at = DateTime.Now;
                _repository.Update(acc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Account Update(Account entity)
        {
            try
            {
                var acc = _repository.GetAll().Where(x => x.account_id == entity.account_id).FirstOrDefault();
                acc.address = entity.address;
                acc.phone = entity.phone;
                acc.full_name = entity.full_name;
                acc.role_code = entity.role_code;
                acc.active = entity.active;
                acc.admin = entity.admin;
                acc.email = entity.email;
                acc.town = entity.town;
                acc.district = entity.district;
                acc.city = entity.city;
                acc.updated_at = DateTime.Now;
                _repository.Update(acc);
                return acc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string createToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //đặt thời gian hết hạn token
            DateTime expires = DateTime.UtcNow.AddDays(10);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in

            var userIdentity = new ClaimsIdentity("Identity");
            userIdentity.Label = "Identity";
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, username));
            userIdentity.AddClaim(new Claim("Username", username));
            //userIdentity.AddClaim(new Claim("Category", Category));
            //userIdentity.HasClaim(ClaimTypes.Role, Category);
            var claims = new List<Claim>();

            var identity = new ClaimsPrincipal(userIdentity);
            Thread.CurrentPrincipal = identity;
            //string sec = EncryptCode;
            string sec = "088881139703564148785";
            //string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1" + Category;
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://unisoft.edu.vn/", audience: "http://unisoft.edu.vn/",
                        subject: userIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}