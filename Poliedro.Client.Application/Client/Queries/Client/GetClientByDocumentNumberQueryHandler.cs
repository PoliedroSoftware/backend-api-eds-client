using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Domain.ClientPos.DomainServices;
using Poliedro.Client.Application.Common.Interfaces;

namespace Poliedro.Client.Application.Client.Queries.Client;

public class GetClientByDocumentNumberQueryHandler(
IClientDomainService _serverDomainService,
      IMapper _mapper,
      ICacheService _cacheService)
      : IRequestHandler<GetClientByDocumentNumberQuery, Result<ClientDto, Error>>
{
    public async Task<Result<ClientDto, Error>> Handle(
        GetClientByDocumentNumberQuery request,
 CancellationToken cancellationToken)
    {
        var cacheKey = $"client:{request.Type}:document:{request.DocumentNumber}";

        var cachedClient = await _cacheService.GetAsync<ClientDto>(cacheKey, cancellationToken);
        if (cachedClient != null)
            return cachedClient;

        var result = await _serverDomainService.GetByDocumentNumberAsync(request.DocumentNumber, request.Type, cancellationToken);
        if (!result.IsSuccess)
            return result.Error!;

        var clientDto = _mapper.Map<ClientDto>(result.Value);

        await _cacheService.SetAsync(cacheKey, clientDto, null, cancellationToken);

        return clientDto;
    }
}
