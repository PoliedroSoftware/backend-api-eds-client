using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Common.Interfaces;
using Poliedro.Client.Domain.ClientPos.DomainServices;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Application.Client.Commands.CreateClientPos;

public class CreateClientNaturalPosHandle(
    IClientDomainService _serverDomainService,
    IMapper _mapper,
    ICacheService _cacheService)
    : IRequestHandler<CreateClientNaturalPosCommand, Result<VoidResult, Error>>
{
    public async Task<Result<VoidResult, Error>> Handle(
        CreateClientNaturalPosCommand request,
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ClientNaturalPosEntity>(request);
        var result = await _serverDomainService.CreateClientNaturalAsync(entity, cancellationToken);
        if (!result.IsSuccess)
            return result.Error!;

        await _cacheService.RemoveByPrefixAsync("clients:natural", cancellationToken);

        return result.Value!;
    }
}
