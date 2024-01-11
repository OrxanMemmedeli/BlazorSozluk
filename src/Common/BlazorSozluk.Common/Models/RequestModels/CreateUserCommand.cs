using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels;

public class CreateUserCommand : IRequest<bool>
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAdress { get; set; }
    public string Password { get; set; }
}
