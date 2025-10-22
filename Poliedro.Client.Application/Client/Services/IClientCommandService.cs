using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Client.Application.Client.Commands.CreateClientPos;

namespace Poliedro.Client.Application.Client.Services;

public interface IClientCommandService
{
    Task<Result<VoidResult, Error>> CreateNaturalClientAsync(
        CreateClientNaturalPosCommand command,
        CancellationToken cancellationToken = default);

    Task<Result<VoidResult, Error>> CreateLegalClientAsync(
        CreateClientLegalPosCommand command,
        CancellationToken cancellationToken = default);
}
