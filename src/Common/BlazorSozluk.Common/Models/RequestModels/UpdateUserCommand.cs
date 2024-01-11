using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels;

public class UpdateUserCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAdress { get; set; }
}
