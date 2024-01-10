using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Common.Models.RequestModels;

public class LoginUserCommand : IRequest<LoginUserViewModel>
{
    public LoginUserCommand(string emailAdress, string password)
    {
        EmailAdress = emailAdress;
        Password = password;
    }

    public LoginUserCommand()
    {

    }

    public string EmailAdress { get; init; }
    public string Password { get; init; }

}
