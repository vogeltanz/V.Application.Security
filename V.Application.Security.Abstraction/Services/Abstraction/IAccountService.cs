using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using V.Application.Security.ViewModels.Abstraction;
using V.Domain.Identity.Entities.Abstraction;

namespace V.Application.Security.Services.Abstraction
{
    public interface IAccountService<TKey>
    {
        Task<string[]?> Register(IUser<TKey> user, string password, IEnumerable<string> roles);
        Task<bool> Login(IUserLogin user, bool isPersistent);
        Task Logout();
    }
}

