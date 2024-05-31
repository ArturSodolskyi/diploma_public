using MediatR;

namespace Module.Companies.Contracts.Companies.Commands.Update
{
    public class UpdateCompanyCommand : IRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}