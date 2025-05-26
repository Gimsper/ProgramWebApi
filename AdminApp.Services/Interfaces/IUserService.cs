using AdminApp.Core.DTO.User;
using AdminApp.Core.Entities;
using AdminApp.Utils.Models;

namespace AdminApp.Services.Interfaces
{
    public interface IUserService : _IBaseService<User>
    {
        Task<ResultOperation> LoginAsync(LoginDTO request);
    }
}
