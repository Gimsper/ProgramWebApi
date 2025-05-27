using AdminApp.Core.DTO.Item;
using AdminApp.Core.Entities;
using AdminApp.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace AdminApp.WebAPI.Controllers
{
    public class ItemController : BaseController<Item>
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService, IMapper mapper) : base(itemService, mapper)
        {
            _itemService = itemService;
        }

        private async Task<string> ConvertImageToBase64Async(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
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
        public async Task<IActionResult> Add([FromForm] ItemAddDTO request)
        {
            request.Image = request.File != null
                ? await ConvertImageToBase64Async(request.File)
                : string.Empty;
            request.ImageType = Path.GetExtension(request.File.FileName);
            var item = _mapper.Map<Item>(request);
            var result = await _itemService.AddAsync(item);
            return Ok(result.StateOperation);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Update([FromForm] ItemUpdateDTO request)
        {
            request.Image = request.File != null
                ? await ConvertImageToBase64Async(request.File)
                : request.Image;
            request.ImageType = request.File != null
                ? Path.GetExtension(request.File.FileName)
                : request.ImageType;
            var item = _mapper.Map<Item>(request);
            var result = await _itemService.UpdateAsync(item);
            return Ok(result.StateOperation);
        }
    }
}
