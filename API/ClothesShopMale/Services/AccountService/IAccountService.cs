using ShoeShopAPI.Models.DTO;
using ShoeShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ShoeShopAPI.Services.AccountService
{
    public interface IAccountService
    {
        List<sp_Account_LoadResult> GetAll(AccountDTO req);
        Account GetById(int id);
        AccountDTO Login(Account entity);
        Account Register(Account entity);
        Account Update(Account entity);
        void Remove(int id);
    }
}