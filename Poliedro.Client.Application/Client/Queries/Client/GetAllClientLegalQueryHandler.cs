using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Domain.ClientPos.DomainServices;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Application.Client.Queries.Client
{
    public class GetAllClientLegalQueryHandler
    (
        IClientDomainService _serverDomainService
    ) : IRequestHandler<GetAllClientLegalQuery, Result<IEnumerable<ClientLegalPosEntity>, Error>>
    {

        public async Task<Result<IEnumerable<ClientLegalPosEntity>, Error>>
            Handle(GetAllClientLegalQuery request, CancellationToken cancellationToken)
        {
            var result = await _serverDomainService.GetAllLegalAsync(request.PageNumber, request.PageSize, cancellationToken);
            if (!result.IsSuccess && result.Value != null)
                return result.Error!;

            return result.Value.ToList();
        }
    }
}
