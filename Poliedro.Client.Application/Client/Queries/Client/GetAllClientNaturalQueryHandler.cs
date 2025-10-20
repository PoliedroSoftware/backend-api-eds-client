using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Domain.ClientPos.DomainServices;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Application.Client.Queries.Client
{
    public class GetAllClientNaturalQueryHandler
    (
        IClientDomainService _serverDomainService
    ) : IRequestHandler<GetAllClientNaturalQuery, Result<IEnumerable<ClientNaturalPosEntity>, Error>>
    {

        public async Task<Result<IEnumerable<ClientNaturalPosEntity>, Error>>
            Handle(GetAllClientNaturalQuery request, CancellationToken cancellationToken)
        {
            var result = await _serverDomainService.GetAllNaturalAsync(request.PageNumber, request.PageSize, cancellationToken);
            if (!result.IsSuccess && result.Value != null)
                return result.Error!;

            return result.Value.ToList();
        }
    }
}
