using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Client.Api.Common.Wrappers;
using Poliedro.Client.Application.Client.Commands.CreateClientPos;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Application.Client.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Poliedro.Client.Api.Controllers.v1.Client;

[Route("api/v1/client")]
[ApiController]
public class ClientController(
    IClientQueryService queryService,
    IClientCommandService commandService) : ControllerBase
{
    [SwaggerOperation(Summary = "Create new natural client")]
    [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ApiResponse<object>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ApiResponse<object>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
    [HttpPost]
    [Route("natural")]
    public async Task<IResult> CreateNaturalClient([FromBody] CreateClientNaturalPosCommand createClientCommand)
    {
        var result = await commandService.CreateNaturalClientAsync(createClientCommand);

        if (result.IsSuccess)
            return TypedResults.Ok(ApiResponse<object>.SuccessResponse(new { }, "Natural client created successfully"));

        return TypedResults.BadRequest(ApiResponse<object>.ErrorResponse(result.Error!.Description));
    }

    [SwaggerOperation(Summary = "Create new legal client")]
    [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ApiResponse<object>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ApiResponse<object>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
    [HttpPost]
    [Route("legal")]
    public async Task<IResult> CreateLegalClient([FromBody] CreateClientLegalPosCommand createClientCommand)
    {
        var result = await commandService.CreateLegalClientAsync(createClientCommand);

        if (result.IsSuccess)
            return TypedResults.Ok(ApiResponse<object>.SuccessResponse(new { }, "Legal client created successfully"));

        return TypedResults.BadRequest(ApiResponse<object>.ErrorResponse(result.Error!.Description));
    }

    [SwaggerOperation(Summary = "Get all legal clients")]
    [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(PagedResponse<IEnumerable<ClientDto>>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No found clients billing electronic.", typeof(ApiResponse<IEnumerable<ClientDto>>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
    [HttpGet]
    [Route("legal")]
    public async Task<IResult> GetAllLegal([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await queryService.GetAllLegalClientsAsync(pageNumber, pageSize);

        if (result.IsSuccess)
            return TypedResults.Ok(new PagedResponse<IEnumerable<ClientDto>>(result.Value!, pageNumber, pageSize));

        return TypedResults.BadRequest(ApiResponse<IEnumerable<ClientDto>>.ErrorResponse(result.Error!.Description));
    }

    [SwaggerOperation(Summary = "Get all natural clients")]
    [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(PagedResponse<IEnumerable<ClientDto>>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No found clients billing electronic.", typeof(ApiResponse<IEnumerable<ClientDto>>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
    [HttpGet]
    [Route("natural")]
    public async Task<IResult> GetAllNatural([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await queryService.GetAllNaturalClientsAsync(pageNumber, pageSize);

        if (result.IsSuccess)
            return TypedResults.Ok(new PagedResponse<IEnumerable<ClientDto>>(result.Value!, pageNumber, pageSize));

        return TypedResults.BadRequest(ApiResponse<IEnumerable<ClientDto>>.ErrorResponse(result.Error!.Description));
    }

    [SwaggerOperation(Summary = "Get natural client by Id")]
    [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The specified client billing electronic does not exist.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
    [HttpGet("natural/{id}")]
    public async Task<IResult> GetNaturalClientById(int id)
    {
        var result = await queryService.GetClientByIdAsync(id, "natural");

        if (result.IsSuccess)
            return TypedResults.Ok(ApiResponse<ClientDto>.SuccessResponse(result.Value!, "Client retrieved successfully"));

        return TypedResults.BadRequest(ApiResponse<ClientDto>.ErrorResponse(result.Error!.Description));
    }

    [SwaggerOperation(Summary = "Get legal client by Id")]
    [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The specified client billing electronic does not exist.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
    [HttpGet("legal/{id}")]
    public async Task<IResult> GetLegalClientById(int id)
    {
        var result = await queryService.GetClientByIdAsync(id, "legal");

        if (result.IsSuccess)
            return TypedResults.Ok(ApiResponse<ClientDto>.SuccessResponse(result.Value!, "Client retrieved successfully"));

        return TypedResults.BadRequest(ApiResponse<ClientDto>.ErrorResponse(result.Error!.Description));
    }

    [SwaggerOperation(Summary = "Get natural client by document number")]
    [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The specified client billing electronic does not exist.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
    [HttpGet("natural/{number}/document-number")]
    public async Task<IResult> GetNaturalClientByDocumentNumber(string number)
    {
        var result = await queryService.GetClientByDocumentNumberAsync(number, "natural");

        if (result.IsSuccess)
            return TypedResults.Ok(ApiResponse<ClientDto>.SuccessResponse(result.Value!, "Client retrieved successfully"));

        return TypedResults.BadRequest(ApiResponse<ClientDto>.ErrorResponse(result.Error!.Description));
    }

    [SwaggerOperation(Summary = "Get legal client by document number")]
    [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The specified client billing electronic does not exist.", typeof(ApiResponse<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
    [HttpGet("legal/{number}/document-number")]
    public async Task<IResult> GetLegalClientByDocumentNumber(string number)
    {
        var result = await queryService.GetClientByDocumentNumberAsync(number, "legal");

        if (result.IsSuccess)
            return TypedResults.Ok(ApiResponse<ClientDto>.SuccessResponse(result.Value!, "Client retrieved successfully"));

        return TypedResults.BadRequest(ApiResponse<ClientDto>.ErrorResponse(result.Error!.Description));
    }
}
