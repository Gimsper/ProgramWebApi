using AdminApp.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly _IBaseService<T> _service;
        protected readonly IMapper _mapper;

        public BaseController(_IBaseService<T> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result.StateOperation);
        }
    }
}
