using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Domain.ClientPos.Entities;
using Poliedro.Client.Domain.ClientPos.Models.Enums;

namespace Poliedro.Client.Domain.ClientPos.DomainServices
{
    public interface IClientDomainService
    {
        Task<Result<VoidResult, Error>> CreateClientLegalAsync(ClientLegalPosEntity request, CancellationToken cancellationToken);
        Task<Result<VoidResult, Error>> CreateClientNaturalAsync(ClientNaturalPosEntity request, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ClientLegalPosEntity>, Error>> GetAllLegalAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ClientNaturalPosEntity>, Error>> GetAllNaturalAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<Result<ClientEntity, Error>> GetByIdAsync(int id, ClientType type, CancellationToken cancellationToken);
        Task<Result<ClientEntity, Error>> GetByDocumentNumberAsync(string documentNumber, ClientType type, CancellationToken cancellationToken);
    }
}
