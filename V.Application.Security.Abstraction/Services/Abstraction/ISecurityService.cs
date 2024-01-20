using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using V.Domain.Identity.Entities.Abstraction;

namespace V.Application.Security.Abstraction.Services.Abstraction
{
    public interface ISecurityService<TKey>
    {
        Task<IUser<TKey>?> FindUserByUsername(string username);
        Task<IUser<TKey>?> FindUserByEmail(string email);
        Task<IList<string>> GetUserRoles(IUser<TKey> user);
        Task<IUser<TKey>?> GetCurrentUser(IPrincipal principal);
    }
}

