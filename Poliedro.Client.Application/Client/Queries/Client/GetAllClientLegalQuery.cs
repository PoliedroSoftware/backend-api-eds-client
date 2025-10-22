using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;

namespace Poliedro.Client.Application.Client.Queries.Client;

public record GetAllClientLegalQuery : IRequest<Result<IEnumerable<ClientDto>, Error>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
