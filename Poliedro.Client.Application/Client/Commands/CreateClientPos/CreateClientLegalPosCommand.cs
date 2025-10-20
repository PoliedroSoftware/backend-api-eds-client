using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;

namespace Poliedro.Client.Application.Client.Commands.CreateClientPos
{
    public record CreateClientLegalPosCommand(
        string CompanyName,
        int? VerificationDigit,
        string DocumentNumber,
        string ElectronicInvoiceEmail,
        bool? VATResponsibleParty,
        bool? SelfRetainer,
        bool? WithholdingAgent,
        bool? SimpleTaxRegime,
        int DocumentTypeId,
        bool? LargeTaxpayer,
        string? DocumentCountry
    ) : IRequest<Result<VoidResult, Error>>;

}
