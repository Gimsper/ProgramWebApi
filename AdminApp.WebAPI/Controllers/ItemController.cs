using AdminApp.Core.DTO.Item;
using AdminApp.Core.Entities;
using AdminApp.Services.Interfaces;
using AdminApp.Utils.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.WebAPI.Controllers
{
    public class ItemController : BaseController<Item>
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService, IMapper mapper) : base(itemService, mapper)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ItemReadDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _itemService.GetAllAsync();
            if (result.StateOperation)
            {
                var items = _mapper.Map<List<ItemReadDTO>>(result.Results);
                return Ok(items);
            }
            return Ok(result.Results);
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemReadDTO))]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _itemService.GetByIdAsync(id);
            var item = _mapper.Map<ItemReadDTO>(result.Result);
            return Ok(item);
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Add([FromBody] ItemAddDTO request)
        {
            var item = _mapper.Map<Item>(request);
            var result = await _itemService.AddAsync(item);
            return Ok(result.StateOperation);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Update([FromBody] ItemUpdateDTO request)
        {
            var item = _mapper.Map<Item>(request);
            var result = await _itemService.UpdateAsync(item);
            return Ok(result.StateOperation);
        }
    }
}
