using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using V.Application.Security.Abstraction.Services.Abstraction;
using V.Infrastructure.EF.Identity.Base.Entities;
using V.Domain.Identity.Entities.Abstraction;
using System.Security.Principal;

namespace V.Application.Security.Identity.Services
{
    public class SecurityIdentityService<TKey> : ISecurityService<TKey> where TKey : IEquatable<TKey>
    {
        UserManager<IUser<TKey>> userManager;

        public SecurityIdentityService(UserManager<IUser<TKey>> userManager)
        {
            this.userManager = userManager;
        }

        public Task<IUser<TKey>?> FindUserByUsername(string username)
        {
            return userManager.FindByNameAsync(username);
        }

        public Task<IUser<TKey>?> FindUserByEmail(string email)
        {
            return userManager.FindByEmailAsync(email);
        }

        public Task<IList<string>> GetUserRoles(IUser<TKey> user)
        {
            return userManager.GetRolesAsync(user);
        }

        public Task<IUser<TKey>?> GetCurrentUser(IPrincipal principal)
        {
            if (principal is ClaimsPrincipal claimsPrincipal)
            {
                return userManager.GetUserAsync(claimsPrincipal);
            }

            throw new NotImplementedException($"{nameof(GetCurrentUser)} method in {nameof(SecurityIdentityService<TKey>)} class does not support the type: {principal.GetType()}.");
        }
    }
}

