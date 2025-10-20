using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Application.Client.Queries.Client
{
    public record GetAllClientNaturalQuery : IRequest<Result<IEnumerable<ClientNaturalPosEntity>, Error>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
