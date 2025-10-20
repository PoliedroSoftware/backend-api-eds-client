using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Domain.ClientPos.Models.Enums;

namespace Poliedro.Client.Application.Client.Queries.Client
{
    public record GetClientByDocumentNumberQuery : IRequest<Result<ClientDto, Error>>
    {
        public string DocumentNumber { get; init; }
        public ClientType Type { get; init; }
    }
}
