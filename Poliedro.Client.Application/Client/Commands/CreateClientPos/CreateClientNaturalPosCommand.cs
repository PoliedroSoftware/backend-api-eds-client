using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;

namespace Poliedro.Client.Application.Client.Commands.CreateClientPos
{
    public record CreateClientNaturalPosCommand(
        string? Name,
        string? MiddleName,
        string? LastName,
        string? SecondSurname,
        string? DocumentNumber,
        string? ElectronicInvoiceEmail,
        int DocumentTypeId,
        string? DocumentCountry
        ) : IRequest<Result<VoidResult, Error>>;
}
