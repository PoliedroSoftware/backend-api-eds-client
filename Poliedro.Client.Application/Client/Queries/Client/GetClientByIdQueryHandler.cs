using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Domain.ClientPos.DomainServices;

namespace Poliedro.Client.Application.Client.Queries.Client
{
    public class GetClientByIdQueryHandler(
        IClientDomainService _serverDomainService,
        IMapper _mapper)
        : IRequestHandler<GetClientByIdQuery, Result<ClientDto, Error>>
    {
        public async Task<Result<ClientDto, Error>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _serverDomainService.GetByIdAsync(request.Id, request.Type, cancellationToken);
            if (!result.IsSuccess)
                return result.Error!;

            return _mapper.Map<ClientDto>(result.Value);
        }
    }
}
