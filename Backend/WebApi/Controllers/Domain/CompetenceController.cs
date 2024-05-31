using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Explorer.Contracts.Competencies.Commands.Create;
using Module.Explorer.Contracts.Competencies.Commands.Delete;
using Module.Explorer.Contracts.Competencies.Commands.Update;
using WebApi.Models.Domain.Competencies;

namespace WebApi.Controllers.Domain
{
    public class CompetenceController : BaseController
    {
        private readonly IMapper _mapper;
        public CompetenceController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompetenceRequestModel model)
        {
            var request = _mapper.Map<CreateCompetenceCommand>(model);
            var id = await Mediator.Send(request);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCompetenceRequestModel model)
        {
            var request = _mapper.Map<UpdateCompetenceCommand>(model);
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteCompetenceCommand
            {
                Id = id
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
