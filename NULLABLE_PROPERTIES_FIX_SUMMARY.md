# Fix for InvalidCastException: Unable to cast object of type 'System.DBNull' to type 'System.String'

## Problem
The application was throwing `System.InvalidCastException` when trying to retrieve client data from the database. This occurred because:

1. The Entity Framework entities had string properties declared as non-nullable (without `?`)
2. The database contained NULL values for some of these string columns  
3. Entity Framework couldn't convert `DBNull` values to non-nullable string properties

## Root Cause
Database schema and entity models were mismatched in terms of nullability expectations.

## Solution Applied
Made the following string properties nullable across the entire application stack:

### Domain Entities
**ClientEntity.cs:**
- `DocumentNumber` ? `string?`
- `ElectronicInvoiceEmail` ? `string?`

**ClientNaturalPosEntity.cs:**
- `Name` ? `string?`
- `MiddleName` ? `string?`  
- `LastName` ? `string?`
- `SecondSurname` ? `string?`

**ClientLegalPosEntity.cs:**
- `CompanyName` ? `string?`

### Application Layer
**Commands:**
- `CreateClientNaturalPosCommand` - Updated all string parameters to nullable
- `CreateClientLegalPosCommand` - Updated all string parameters to nullable

**DTOs:**
- `ClientDto` - Updated all string properties to nullable to match entities

### Affected Components
? **Fixed:** All query operations (`GetAllNaturalAsync`, `GetAllLegalAsync`, `GetByIdAsync`, `GetByDocumentNumberAsync`)
? **Fixed:** All create operations (`CreateClientNaturalAsync`, `CreateClientLegalAsync`)  
? **Fixed:** AutoMapper mappings between Commands and Entities
? **Fixed:** DTO mappings from Entities to response objects

## Current Behavior
- The application can now handle NULL values in the database gracefully
- Create operations accept nullable string values in requests
- Query operations return nullable string values in responses
- No runtime exceptions occur when processing client data with NULL string fields

## Validation Impact
- Existing FluentValidation rules for query parameters remain unchanged
- No create command validators were found, so no validation updates were needed
- The application maintains proper validation for required query fields while allowing flexible data storage

## Notes
- This fix addresses the immediate data access issue
- Consider adding database constraints and business logic validation if certain fields should be required
- Monitor for any business logic that might depend on non-null string values