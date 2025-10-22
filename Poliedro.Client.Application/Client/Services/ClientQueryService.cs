using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Application.Client.Queries.Client;
using Poliedro.Client.Domain.ClientPos.Models.Enums;

namespace Poliedro.Client.Application.Client.Services;

public class ClientQueryService(IMediator mediator) : IClientQueryService
{
    public async Task<Result<IEnumerable<ClientDto>, Error>> GetAllLegalClientsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllClientLegalQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<IEnumerable<ClientDto>, Error>> GetAllNaturalClientsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllClientNaturalQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<ClientDto, Error>> GetClientByIdAsync(
        int id,
        string clientType,
        CancellationToken cancellationToken = default)
    {
        var type = clientType.ToLower() == "natural" ? ClientType.Natural : ClientType.Legal;
        var query = new GetClientByIdQuery
        {
            Id = id,
            Type = type
        };
        return await mediator.Send(query, cancellationToken);
    }

    public async Task<Result<ClientDto, Error>> GetClientByDocumentNumberAsync(
        string documentNumber,
        string clientType,
        CancellationToken cancellationToken = default)
    {
        var type = clientType.ToLower() == "natural" ? ClientType.Natural : ClientType.Legal;
        var query = new GetClientByDocumentNumberQuery
        {
            DocumentNumber = documentNumber,
            Type = type
        };
        return await mediator.Send(query, cancellationToken);
    }
}
