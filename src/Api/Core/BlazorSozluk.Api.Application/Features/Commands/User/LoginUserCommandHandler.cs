using AutoMapper;
using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Common.Constants;
using BlazorSozluk.Common.Infrasturucture;
using BlazorSozluk.Common.Infrasturucture.Exceptions;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorSozluk.Api.Application.Features.Commands.User;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var password = PasswordEncryptor.Encrpt(request.Password);

        var user = await _userRepository.GetSingleAsync(i => i.EmailAdress.Equals(request.EmailAdress) && i.Password == password);

        if (user.Equals(null))
            throw new DatabaseValidatorException(ExceptionMessage.UserNotFound_EN);

        if (!user.EmailConfirmed)
            throw new DatabaseValidatorException(ExceptionMessage.EmailAdressIsNotConfirmedYet_EN);

        var result = _mapper.Map<LoginUserViewModel>(user);

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.EmailAdress),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
        };

        result.Token = GenerateJWTToken(claims);

        return result;
    }

    /// <summary>
    /// Generate Jwt Token Method
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    private string GenerateJWTToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiredDate = DateTime.Now.AddDays(10);

        var token = new JwtSecurityToken(claims: claims, expires: expiredDate, signingCredentials: credentials, notBefore: DateTime.Now);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
