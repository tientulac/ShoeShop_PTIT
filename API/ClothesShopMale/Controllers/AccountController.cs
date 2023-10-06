using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Repositories;
using ShoeShopAPI.Services.AccountService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace ShoeShopAPI.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;


        public AccountController(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("api/v1/account/get-list")]
        public ResponseBase<List<sp_Account_LoadResult>> GetList(AccountDTO req)
        {
            try
            {
                return new ResponseBase<List<sp_Account_LoadResult>>
                {
                    data = _accountService.GetAll(req),
                    status = 200,
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<sp_Account_LoadResult>>
                {
                    status = 500,
                    exMessage = ex.Message,
                };
            }
        }

        [HttpGet]
        [Route("api/v1/account/findbyid/{id}")]
        public ResponseBase<Account> FindById(int id = 0)
        {
            try
            {
                return new ResponseBase<Account>
                {
                    data = _accountService.GetById(id),
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<Account>
                {
                    status = 500,
                    exMessage = ex.Message,
                };
            }
        }

        [HttpDelete]
        [Route("api/v1/account/{id}")]
        public ResponseBase<bool> Delete(int id = 0)
        {
            try
            {
                _accountService.Remove(id);
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
                    exMessage = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("api/v1/account")]
        public ResponseBase<AccountDTO> Save(AccountDTO req)
        {
            try
            {
                var acc = _mapper.Map<Account>(req);
                if (req.account_id > 0)
                {
                    _accountService.Register(req);
                }
                else
                {
                    _accountService.Update(req);
                }
                return new ResponseBase<AccountDTO>
                {
                    data = req,
                    status = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseBase<AccountDTO>
                {
                    status = 500,
                    exMessage = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("api/v1/account/login")]
        public ResponseBase<AccountDTO> Login(AccountDTO req)
        {
            try
            {
                var acc = _accountService.Login(req);
                if (acc.account_id > 0)
                {
                    return new ResponseBase<AccountDTO>
                    {
                        data = acc,
                        status = 200
                    };
                }
                else
                {
                    return new ResponseBase<AccountDTO>
                    {
                        status = 500
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<AccountDTO>
                {
                    status = 500,
                    message = ex.Message
                };
            }
        }

    }
}