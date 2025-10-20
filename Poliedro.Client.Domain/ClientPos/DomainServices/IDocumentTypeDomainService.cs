using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Domain.ClientPos.DomainServices
{
    public interface IDocumentTypeDomainService
    {
        Task<Result<IEnumerable<DocumentTypeEntity>, Error>> GetAllAsync(CancellationToken cancellationToken);
    }
}
