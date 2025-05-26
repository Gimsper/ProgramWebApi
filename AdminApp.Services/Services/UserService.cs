using AdminApp.Core.DTO.User;
using AdminApp.Core.Entities;
using AdminApp.Infrastructure.Interfaces;
using AdminApp.Services.Interfaces;
using AdminApp.Utils.Models;
using System.Linq.Expressions;

namespace AdminApp.Services.Services
{
    public class UserService : _BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _userRepository = repository;
        }

        public async Task<ResultOperation> LoginAsync(LoginDTO request)
        {
            ResultOperation result = new();
            try
            {
                var user = await GetByAsync(e => e.Username == request.Username);
                if (user.StateOperation)
                {
                    result.StateOperation = user.Result?.Password == request.Password;
                }
                else
                {
                    result.StateOperation = false;
                }
            }
            catch (Exception ex)
            {
                result.StateOperation = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
