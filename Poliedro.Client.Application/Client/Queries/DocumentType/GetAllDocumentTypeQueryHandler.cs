using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Domain.ClientPos.DomainServices;
using Poliedro.Client.Domain.ClientPos.Entities;
using Poliedro.Client.Application.Common.Interfaces;

namespace Poliedro.Client.Application.Client.Queries.DocumentType;

public class GetAllDocumentTypeQueryHandler(
 IDocumentTypeDomainService _serverDomainService,
ICacheService _cacheService)
    : IRequestHandler<GetAllDocumentTypeQuery, Result<ApiResponseDto<IEnumerable<DocumentTypeEntity>>, Error>>
{
    public async Task<Result<ApiResponseDto<IEnumerable<DocumentTypeEntity>>, Error>> Handle(
    GetAllDocumentTypeQuery request,
    CancellationToken cancellationToken)
    {
        var cacheKey = "documenttypes:all";

        var cachedDocumentTypes = await _cacheService.GetAsync<IEnumerable<DocumentTypeEntity>>(cacheKey, cancellationToken);
        if (cachedDocumentTypes != null)
            return new ApiResponseDto<IEnumerable<DocumentTypeEntity>>(cachedDocumentTypes.ToList());

        var result = await _serverDomainService.GetAllAsync(cancellationToken);
        if (!result.IsSuccess && result.Value != null)
            return result.Error!;

        var documentTypes = result.Value.ToList();

        await _cacheService.SetAsync(cacheKey, documentTypes, null, cancellationToken);

        return new ApiResponseDto<IEnumerable<DocumentTypeEntity>>(documentTypes);
    }
}

