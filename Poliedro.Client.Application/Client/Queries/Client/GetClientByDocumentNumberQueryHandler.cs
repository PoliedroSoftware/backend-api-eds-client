using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Domain.ClientPos.DomainServices;

namespace Poliedro.Client.Application.Client.Queries.Client
{
    public class GetClientByDocumentNumberQueryHandler(
        IClientDomainService _serverDomainService,
        IMapper _mapper)
        : IRequestHandler<GetClientByDocumentNumberQuery, Result<ClientDto, Error>>
    {
        public async Task<Result<ClientDto, Error>> Handle(GetClientByDocumentNumberQuery request, CancellationToken cancellationToken)
        {
            var result = await _serverDomainService.GetByDocumentNumberAsync(request.DocumentNumber, request.Type, cancellationToken);
            if (!result.IsSuccess)
                return result.Error!;

            return _mapper.Map<ClientDto>(result.Value);
        }
    }
}
