using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Api.Common.Extensions;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Application.Client.Queries.DocumentType;
using Poliedro.Client.Domain.ClientPos.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Poliedro.Client.Api.Controllers.v1.Billing;

[Route("api/v1/client")]
[ApiController]
public class DocumentTypeController(IMediator mediator) : ControllerBase
{
    [SwaggerOperation(Summary = "Get all document types")]
    [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ApiResponseDto<IEnumerable<DocumentTypeEntity>>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No found clients billing electronic.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
    [HttpGet]
    [Route("document-type")]
    public async Task<IResult> GetAll()
    {
        var result = await mediator.Send(new GetAllDocumentTypeQuery());
        return result.Match(
           onSuccess => TypedResults.Ok(onSuccess),
           onFailure => TypedResults.BadRequest(onFailure)
       );
    }
}
