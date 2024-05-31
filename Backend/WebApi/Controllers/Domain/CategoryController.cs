using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Explorer.Contracts.Categories.Commands.Create;
using Module.Explorer.Contracts.Categories.Commands.Delete;
using Module.Explorer.Contracts.Categories.Commands.Update;
using WebApi.Models.Domain.Categories;

namespace WebApi.Controllers.Domain
{
    public class CategoryController : BaseController
    {
        private readonly IMapper _mapper;
        public CategoryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequestModel model)
        {
            var request = _mapper.Map<CreateCategoryCommand>(model);
            var id = await Mediator.Send(request);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequestModel model)
        {
            var request = _mapper.Map<UpdateCategoryCommand>(model);
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteCategoryCommand
            {
                Id = id
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
