using Microsoft.AspNetCore.Identity;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UserServices.Query.GetRolesService
{
    public interface IGetRolesService
    {
        ResultDto<List<string>> Execute();
    }
    public class GetRolesService : IGetRolesService
    {
        private readonly RoleManager<Role> _roleManager;

        public GetRolesService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public ResultDto<List<string>> Execute()
        {
            return new ResultDto<List<string>>(true,"موفقیت آمیز!")
            {
                Data = _roleManager.Roles.Select(p => p.Name).ToList()
            }; 
        }
    }
}
