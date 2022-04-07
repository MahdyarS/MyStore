using MyStore.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UserServices.Command.LoginService
{
    public interface ILoginService
    {
        ResultDto Execute(RequestLoginDto request);
    }
}
