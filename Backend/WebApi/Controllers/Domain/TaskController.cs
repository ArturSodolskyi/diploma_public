using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Explorer.Contracts.Tasks.Commands.Create;
using Module.Explorer.Contracts.Tasks.Commands.Delete;
using Module.Explorer.Contracts.Tasks.Commands.Update;
using WebApi.Models.Domain.Tasks;

namespace WebApi.Controllers.Domain
{
    public class TaskController : BaseController
    {
        private readonly IMapper _mapper;
        public TaskController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequestModel model)
        {
            var request = _mapper.Map<CreateTaskCommand>(model);
            var id = await Mediator.Send(request);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskRequestModel model)
        {
            var request = _mapper.Map<UpdateTaskCommand>(model);
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteTaskCommand
            {
                Id = id
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
