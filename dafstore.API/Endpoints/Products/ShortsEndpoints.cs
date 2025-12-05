using dafstore.API.Infrastructure;
using dafstore.Application.Products.Commands.CreateShorts;
using dafstore.Application.Products.Commands.UpdateShorts;
using dafstore.Application.Products.Queries.GetShorts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dafstore.API.Endpoints.Products;

public class ShortsEndpoints : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAllShortsAsync)
            .MapGet(GetShortByIdAsync,"{id}" )
            .MapPost(CreateShorts)
            .MapPut(UpdateShorts, "{id}");
    }

    public async Task<Results<Ok<IEnumerable<ShortsDTO>>, ValidationProblem>> GetAllShortsAsync(
        [AsParameters] GetAllShortsQuery query,
        ISender sender,
        IValidator<GetAllShortsQuery> validator)
    {
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return TypedResults.Ok(await sender.Send(query));
    }

    public async Task<Results<Ok<ShortsDTO>, NotFound>> GetShortByIdAsync(
        [FromRoute] Guid id,
        ISender sender)
    {
        var result = await sender.Send(new GetShortsByIdQuery(id));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreateShorts(
        [FromBody] CreateShortsCommand command,
        ISender sender,
        IValidator<CreateShortsCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{nameof(PantsEndpoints)}/{id}", id);
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateShorts(
        [FromRoute] Guid id,
        [FromBody] UpdateShortsCommand command,
        ISender sender,
        IValidator<UpdateShortsCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}