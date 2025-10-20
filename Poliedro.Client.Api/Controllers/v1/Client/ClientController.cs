using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Api.Common.Extensions;
using Poliedro.Client.Application.Client.Commands.CreateClientPos;
using Poliedro.Client.Application.Client.Dtos;
using Poliedro.Client.Application.Client.Queries.Client;
using Poliedro.Client.Application.Client.Queries.DocumentType;
using Poliedro.Client.Domain.ClientPos.Entities;
using Poliedro.Client.Domain.ClientPos.Models.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Poliedro.Client.Api.Controllers.v1.Client
{
    [Route("api/v1/client")]
    [ApiController]
    public class ClientController(IMediator mediator) : ControllerBase
    {
        [SwaggerOperation(Summary = "Create new Client")]
        [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(CreateClientNaturalPosCommand))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
        [HttpPost]
        [Route("natural")]
        public async Task<IResult> Create(
            [FromBody] CreateClientNaturalPosCommand createClientCommand)
        {
            var result = await mediator.Send(createClientCommand);
            return result.Match(
                onSuccess => TypedResults.Ok(onSuccess),
                onFailure => TypedResults.BadRequest(onFailure)
            );
        }

        [SwaggerOperation(Summary = "Create new Client")]
        [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(CreateClientLegalPosCommand))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
        [HttpPost]
        [Route("legal")]
        public async Task<IResult> Create(
            [FromBody] CreateClientLegalPosCommand createClientCommand)
        {
            var result = await mediator.Send(createClientCommand);
            return result.Match(
                onSuccess => TypedResults.Ok(onSuccess),
                onFailure => TypedResults.BadRequest(onFailure)
            );
        }

        [SwaggerOperation(Summary = "Get all legal clients")]
        [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(IEnumerable<ClientLegalPosEntity>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No found clients billing electronic.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
        [HttpGet]
        [Route("legal")]
        public async Task<IResult> GetAllLegal([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await mediator.Send(new GetAllClientLegalQuery() { PageNumber = pageNumber, PageSize = pageSize });
            return result.Match(
               onSuccess => TypedResults.Ok(onSuccess),
               onFailure => TypedResults.BadRequest(onFailure)
           );
        }

        [SwaggerOperation(Summary = "Get all natural clients")]
        [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(IEnumerable<ClientNaturalPosEntity>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No found clients billing electronic.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
        [HttpGet]
        [Route("natural")]
        public async Task<IResult> GetAllNatural([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await mediator.Send(new GetAllClientNaturalQuery() { PageNumber = pageNumber, PageSize = pageSize });
            return result.Match(
               onSuccess => TypedResults.Ok(onSuccess),
               onFailure => TypedResults.BadRequest(onFailure)
           );
        }

        [SwaggerOperation(Summary = "Get natural client by Id")]
        [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ClientDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The specified client billing electronic does not exist.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
        [HttpGet("natural/{id}")]
        public async Task<IResult> GetNaturalClientById(int id)
        {
            var getClientQuery = new GetClientByIdQuery { Id = id, Type = ClientType.Natural };
            var result = await mediator.Send(getClientQuery);
            return result.Match(onSuccess => TypedResults.Ok(result.Value));
        }

        [SwaggerOperation(Summary = "Get legal client by Id")]
        [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ClientDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The specified client billing electronic does not exist.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
        [HttpGet("legal/{id}")]
        public async Task<IResult> GetLegalClientById(int id)
        {
            var getClientQuery = new GetClientByIdQuery { Id = id, Type = ClientType.Legal };
            var result = await mediator.Send(getClientQuery);
            return result.Match(onSuccess => TypedResults.Ok(result.Value));
        }

        [SwaggerOperation(Summary = "Get natural client by document number")]
        [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ClientDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The specified client billing electronic does not exist.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
        [HttpGet("natural/{number}/document-number")]
        public async Task<IResult> GetNaturalClientByDocumentNumber(string number)
        {
            var getClientQuery = new GetClientByDocumentNumberQuery { DocumentNumber = number, Type = ClientType.Natural };
            var result = await mediator.Send(getClientQuery);
            return result.Match(onSuccess => TypedResults.Ok(result.Value));
        }

        [SwaggerOperation(Summary = "Get legal client by document number")]
        [SwaggerResponse(StatusCodes.Status200OK, "The operation was successful.", typeof(ClientDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Incorrect request parameters.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "The request lacks valid authentication credentials.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The specified client billing electronic does not exist.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error processing the request.", typeof(ProblemDetails))]
        [HttpGet("legal/{number}/document-number")]
        public async Task<IResult> GetLegalClientByDocumentNumber(string number)
        {
            var getClientQuery = new GetClientByDocumentNumberQuery { DocumentNumber = number, Type = ClientType.Legal };
            var result = await mediator.Send(getClientQuery);
            return result.Match(onSuccess => TypedResults.Ok(result.Value));
        }
    }
}
