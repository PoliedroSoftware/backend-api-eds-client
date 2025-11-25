using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Api.Common.Extensions;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Application.Client.Queries.DocumentType;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Api.Endpoints;

public static class DocumentTypeEndpoints
{
    public static void MapDocumentTypeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/client")
            .WithTags("Client");

        group.MapGet("document-type", GetAllDocumentTypes)
            .WithName("GetAllDocumentTypes")
            .WithSummary("Get all document types")
            .Produces<ApiResponseDto<IEnumerable<DocumentTypeEntity>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> GetAllDocumentTypes(IMediator mediator)
    {
        var result = await mediator.Send(new GetAllDocumentTypeQuery());
        return result.Match(
            onSuccess => TypedResults.Ok(onSuccess),
            onFailure => TypedResults.BadRequest(onFailure)
        );
    }
}
