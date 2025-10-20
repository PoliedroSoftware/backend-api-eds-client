using Microsoft.EntityFrameworkCore;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Errors.Client;
using Poliedro.Client.Domain.ClientPos.DomainServices;
using Poliedro.Client.Domain.ClientPos.Entities;
using Poliedro.Client.Infraestructure.Persistence.Mysql.Context;

namespace Poliedro.Client.Infraestructure.Persistence.Mysql.Client.DomainServices
{
    public class DocumentTypeDomainService: IDocumentTypeDomainService
    {
        private readonly ClientDbContext _context;

        public DocumentTypeDomainService(ClientDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<DocumentTypeEntity>, Error>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _context.DocumentTypes.ToListAsync(cancellationToken);

            if (entities.Count == 0)
                return ClienErrorBuilder.NoDocumentTypeRecordsFoundException();

            return entities;
        }
    }
}
