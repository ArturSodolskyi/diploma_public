using MediatR;

namespace Module.Companies.Contracts.Companies.Commands.Delete
{
    public class DeleteCompanyCommand : IRequest
    {
        public int Id { get; set; }
    }
}
