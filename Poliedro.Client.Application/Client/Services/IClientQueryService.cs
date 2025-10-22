using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;

namespace Poliedro.Client.Application.Client.Services;

public interface IClientQueryService
{
    Task<Result<IEnumerable<ClientDto>, Error>> GetAllLegalClientsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<ClientDto>, Error>> GetAllNaturalClientsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<Result<ClientDto, Error>> GetClientByIdAsync(
        int id,
        string clientType,
        CancellationToken cancellationToken = default);

    Task<Result<ClientDto, Error>> GetClientByDocumentNumberAsync(
        string documentNumber,
        string clientType,
        CancellationToken cancellationToken = default);
}
