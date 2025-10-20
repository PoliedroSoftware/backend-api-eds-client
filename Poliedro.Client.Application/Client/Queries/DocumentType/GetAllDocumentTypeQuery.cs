using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Application.Client.Queries.DocumentType
{
    public record GetAllDocumentTypeQuery : IRequest<Result<ApiResponseDto<IEnumerable<DocumentTypeEntity>>, Error>>
    {
    }
}
