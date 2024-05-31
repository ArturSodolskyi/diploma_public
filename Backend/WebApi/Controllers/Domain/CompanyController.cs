using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Companies.Contracts.Companies.Commands.Create;
using Module.Companies.Contracts.Companies.Commands.Delete;
using Module.Companies.Contracts.Companies.Commands.Update;
using WebApi.Models.Domain.Companies;

namespace WebApi.Controllers.Domain
{
    public class CompanyController : BaseController
    {
        private readonly IMapper _mapper;
        public CompanyController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyRequestModel model)
        {
            var request = _mapper.Map<CreateCompanyCommand>(model);
            var id = await Mediator.Send(request);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyRequestModel model)
        {
            var request = _mapper.Map<UpdateCompanyCommand>(model);
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteCompanyCommand
            {
                Id = id
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
