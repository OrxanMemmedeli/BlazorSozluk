using AutoMapper;
using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Common.Constants;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrasturucture.ConsumerFactory;
using BlazorSozluk.Common.Infrasturucture.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if(user is not null)
            throw new DatabaseValidatorException(ExceptionMessage.UserNotFound_EN);

        var dbEmailAddress = user.EmailAdress;
        var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAdress) != 0;

        //var dbUser = _mapper.Map<Domain.Entities.User>(request);
        _mapper.Map(request, user);

        var row = await _userRepository.UpdateAsync(user);

        if (emailChanged && row > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = request.EmailAdress
            };
            QueueFactory.SendMessageToExchange(exchangeName: RabbitMQConstant.UserExchangeName, exchangeType: RabbitMQConstant.DefaultExchangeType, queueName: RabbitMQConstant.UserEmailChangedQueueName, obj: @event);

            user.EmailConfirmed = false;
            await _userRepository.UpdateAsync(user);
        }

        return user.Id;
    }
}
