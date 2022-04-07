using Microsoft.AspNetCore.Identity;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UserServices.Command.DisableUserService
{
    public interface IDisableUserService
    {
        ResultDto Execute(string userName);
    }

    public class DisableUserService : IDisableUserService
    {
        private readonly UserManager<User> _userManager;

        public DisableUserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ResultDto Execute(string userName)
        {
            try
            {
                var user = _userManager.FindByNameAsync(userName).Result;
                if (user.LockoutEnd < DateTimeOffset.Now)
                {
                    _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(new DateTime(2222, 2, 2))).Wait();
                    return new ResultDto(true, "کاربر با موفقیت غیرفعال شد!");
                }
                _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now).Wait();

                return new ResultDto(true, "کاربر با موفقیت فعال شد!");

            }
            catch (Exception)
            {
                return new ResultDto(false, "عملیات ناموفق!");
                throw;
            }
            return new ResultDto(false, "عملیات ناموفق!");

        }
    }
}
