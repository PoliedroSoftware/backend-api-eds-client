using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Domain.ClientPos.DomainServices;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Application.Client.Commands.CreateClientPos
{
    public class CreateClientLegalPosHandle(
        IClientDomainService _serverDomainService,
        IMapper _mapper
    ) : IRequestHandler<CreateClientLegalPosCommand, Result<VoidResult, Error>>
    {

        public async Task<Result<VoidResult, Error>> Handle(CreateClientLegalPosCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ClientLegalPosEntity>(request);
            var result = await _serverDomainService.CreateClientLegalAsync(entity, cancellationToken);
            if (!result.IsSuccess)
                return result.Error!;

            return result.Value!;
        }
    }
}
