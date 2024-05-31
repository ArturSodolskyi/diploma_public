using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Explorer.Contracts.Jobs.Commands.Create;
using Module.Explorer.Contracts.Jobs.Commands.Delete;
using Module.Explorer.Contracts.Jobs.Commands.Update;
using WebApi.Models.Domain.Jobs;

namespace WebApi.Controllers.Domain
{
    public class JobController : BaseController
    {
        private readonly IMapper _mapper;
        public JobController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobRequestModel model)
        {
            var request = _mapper.Map<CreateJobCommand>(model);
            var id = await Mediator.Send(request);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateJobRequestModel model)
        {
            var request = _mapper.Map<UpdateJobCommand>(model);
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteJobCommand
            {
                Id = id
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
