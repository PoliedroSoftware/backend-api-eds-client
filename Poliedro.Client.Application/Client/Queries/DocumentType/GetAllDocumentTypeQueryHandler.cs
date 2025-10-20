using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Domain.ClientPos.DomainServices;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Application.Client.Queries.DocumentType
{
    public class GetAllDocumentTypeQueryHandler
    (
        IDocumentTypeDomainService _serverDomainService
    ) : IRequestHandler<GetAllDocumentTypeQuery, Result<ApiResponseDto<IEnumerable<DocumentTypeEntity>>, Error>>
    {

        public async Task<Result<ApiResponseDto<IEnumerable<DocumentTypeEntity>>, Error>>
            Handle(GetAllDocumentTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _serverDomainService.GetAllAsync(cancellationToken);
            if (!result.IsSuccess && result.Value != null)
                return result.Error!;
            
            return new ApiResponseDto<IEnumerable<DocumentTypeEntity>>(result.Value.ToList()); ;
        }
    }
}

