using System;
using Microsoft.AspNetCore.Identity;
using V.Application.Security.Services.Abstraction;
using V.Application.Security.ViewModels.Abstraction;
using V.Domain.Identity.Entities.Abstraction;
using V.Infrastructure.EF.Identity.Base.Entities;

namespace V.Application.Security.Identity.Services
{
    public class AccountIdentityService<TKey> : IAccountService<TKey> where TKey : IEquatable<TKey>
    {
        UserManager<IUser<TKey>> userManager;
        SignInManager<IUser<TKey>> sigInManager;

        public AccountIdentityService(UserManager<IUser<TKey>> userManager, SignInManager<IUser<TKey>> sigInManager)
        {
            this.userManager = userManager;
            this.sigInManager = sigInManager;
        }

        public async Task<string[]?> Register(IUser<TKey> user, string password, IEnumerable<string>? roles = null)
        {
            string[]? errors = null;

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                if (roles != null && roles.Count() > 0)
                {
                    var resultRole = await userManager.AddToRolesAsync(user, roles);

                    if (resultRole.Succeeded == false)
                    {
                        for (int i = 0; i < result.Errors.Count(); ++i)
                            result.Errors.Append(result.Errors.ElementAt(i));
                    }
                }
            }

            if (result.Errors != null && result.Errors.Count() > 0)
            {
                errors = new string[result.Errors.Count()];
                for (int i = 0; i < result.Errors.Count(); ++i)
                {
                    errors[i] = result.Errors.ElementAt(i).Description;
                }
            }

            return errors;
        }

        public async Task<bool> Login(IUserLogin user, bool isPersistent)
        {
            var result = await sigInManager.PasswordSignInAsync(user.UserName, user.Password, isPersistent, true);
            return result.Succeeded;
        }

        public Task Logout()
        {
            return sigInManager.SignOutAsync();
        }
    }
}

