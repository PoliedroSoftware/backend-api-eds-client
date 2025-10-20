﻿using Poliedro.Billing.Domain.Common.Results.Errors;
using System.Net;

namespace Poliedro.Client.Application.Client.Errors.Client
{
    public class ClienErrorBuilder : IError
    {
        public const string CLIENT_CREATION_ERROR = "ClientCreationErrorException";
        public const string NO_DOCUMENT_TYPE_RECORDS_FOUND = "NoDocumentTypeRecordsFoundErrorException";
        public const string CLIENT_NOT_FOUND_ERROR = "ClientNotFoundErrorException";

        public static Error ClientLegalCreationException() => Error.CreateInstance(
            CLIENT_CREATION_ERROR,
            "Failed to create Client Billing Electronic due to an internal error.",
            HttpStatusCode.InternalServerError);

        public static Error ClientNaturalCreationException() => Error.CreateInstance(
            CLIENT_CREATION_ERROR,
            "Failed to create Client Billing Electronic due to an internal error.",
            HttpStatusCode.InternalServerError);

        public static Error NoDocumentTypeRecordsFoundException() => Error.CreateInstance(
            NO_DOCUMENT_TYPE_RECORDS_FOUND,
            "No document type records were found.",
            HttpStatusCode.NotFound);

        public static Error ClienNotFoundException(int id) => Error.CreateInstance(
            CLIENT_NOT_FOUND_ERROR,
            $"Client Billing Electronic with ID {id} was not found.",
            HttpStatusCode.NotFound);

        public static Error ClienNotFoundException(string id) => Error.CreateInstance(
            CLIENT_NOT_FOUND_ERROR,
            $"Client Billing Electronic with document number {id} was not found.",
            HttpStatusCode.NotFound);
    }
}
