using AutoMapper;
using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Common.Constants;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrasturucture.ConsumerFactory;
using BlazorSozluk.Common.Infrasturucture.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await _userRepository.GetSingleAsync(i => i.EmailAdress.Equals(request.EmailAdress));

        if (existsUser is not null)
            throw new DatabaseValidatorException(ExceptionMessage.UserAlreadyExists_EN);

        var dbUser = _mapper.Map<Domain.Entities.User>(request);

        var row = await _userRepository.AddAsync(dbUser);

        //email gonderme prosesi
        if (row > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = request.EmailAdress
            };
            QueueFactory.SendMessageToExchange(exchangeName: RabbitMQConstant.UserExchangeName, exchangeType: RabbitMQConstant.DefaultExchangeType, queueName: RabbitMQConstant.UserEmailChangedQueueName, obj: @event);
        }

        return row > 0;
    }
}
