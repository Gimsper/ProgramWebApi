using AdminApp.Core.DTO.User;
using AdminApp.Core.Entities;
using AdminApp.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.WebAPI.Controllers
{
    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper) : base(userService, mapper)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserReadDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            if (result.StateOperation)
            {
                var items = _mapper.Map<List<UserReadDTO>>(result.Results);
                return Ok(items);
            }
            return Ok(result.Results);
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadDTO))]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _userService.GetByIdAsync(id);
            var item = _mapper.Map<UserReadDTO>(result.Result);
            return Ok(item);
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Add([FromBody] UserAddDTO request)
        {
            var item = _mapper.Map<User>(request);
            var result = await _userService.AddAsync(item);
            return Ok(result.StateOperation);
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var result = await _userService.LoginAsync(request);
            return Ok(result.StateOperation);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Update([FromBody] UserUpdateDTO request)
        {
            var item = _mapper.Map<User>(request);
            var result = await _userService.UpdateAsync(item);
            return Ok(result.StateOperation);
        }
    }
}
