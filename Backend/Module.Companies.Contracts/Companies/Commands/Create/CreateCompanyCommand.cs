using MediatR;

namespace Module.Companies.Contracts.Companies.Commands.Create
{
    public class CreateCompanyCommand : IRequest<int>
    {
        public required string Name { get; set; }
    }
}
