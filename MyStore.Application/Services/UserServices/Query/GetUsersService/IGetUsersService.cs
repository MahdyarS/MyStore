using Microsoft.AspNetCore.Identity;
using MyStore.Common.Pagination;
using MyStore.Common.ResultDto;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UserServices.Query.GetUsersService
{
    public interface IGetUsersService
    {
        ResultDto<GetUsersResultDto> Execute(GetUsersRequest request);
    }

    public class GetUsersService : IGetUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public GetUsersService(UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ResultDto<GetUsersResultDto> Execute(GetUsersRequest request)
        {
            var paginationResult = _userManager.Users
                .Where(p => p.UserName.Contains(request.SearchKey))
                .Select(p => new UserModelInAdminList
                {
                    Email = p.Email,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Roles = (List<string>)_userManager.GetRolesAsync(p).Result,
                    PhoneNumber = p.PhoneNumber,
                    IsDisabled = (p.LockoutEnd > DateTimeOffset.Now),
                    end = p.LockoutEnd
                    
                }).ToPaged(request.PageIndex, request.ItemsInPage);

            var now = DateTime.Now;
            var noww = DateTimeOffset.Now;


            if (!paginationResult.Succeeded)
            {
                return new ResultDto<GetUsersResultDto>(false, paginationResult.Message);
            }

            var result = new GetUsersResultDto()
            {
                UsersList = paginationResult.RequestedPageList,
                PagesCount = paginationResult.PagesCount,
                PrevIsDiabled = paginationResult.PrevIsDiabled,
                NextIsDisabled = paginationResult.NextIsDisabled,
                FirstPageIndexToShow = paginationResult.FirstPageIndexToShow,
                LastPageIndexToShow = paginationResult.LastPageIndexToShow,
                RequestedPageIndex = request.PageIndex,
                RequestedSearchKey = request.SearchKey
            };
            

            return new ResultDto<GetUsersResultDto>(true, paginationResult.Message, result);

        }
    }

    public class GetUsersRequest
    {
        public GetUsersRequest(string searchKey, int pageIndex, int itemsInPage)
        {
            SearchKey = searchKey;
            PageIndex = pageIndex;
            ItemsInPage = itemsInPage;
        }

        public string SearchKey { get; set; }
        public int PageIndex { get; set; }
        public int ItemsInPage { get; set; }
    }

    public class GetUsersResultDto 
    {
        public List<UserModelInAdminList> UsersList { get; set; }
        public int PagesCount { get; set; }
        public string RequestedSearchKey { get; set; }
        public int RequestedPageIndex { get; set; }
        public bool PrevIsDiabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
    }
    public class UserModelInAdminList
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public string Email { get; set; }
        public bool IsDisabled { get; set; }
        public DateTimeOffset? end { get; set; }

    }
}
