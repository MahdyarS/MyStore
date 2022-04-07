using MyStore.Common.ResultDto;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Application.Services.UserServices.Command.RegisterService
{
    public interface IRegisterService
    {
        ResultDto Execute(RequestRegisterUserDto request);
    }
}
