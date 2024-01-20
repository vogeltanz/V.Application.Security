using System;
namespace V.Application.Security.ViewModels.Abstraction
{
    public interface IUserLogin
    {
        string UserName { get; set; }
        string Password { get; set; }
    }
}

