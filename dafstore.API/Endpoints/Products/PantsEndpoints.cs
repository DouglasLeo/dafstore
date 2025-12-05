using dafstore.API.Infrastructure;
using dafstore.Application.Products.Commands.CreatePants;
using dafstore.Application.Products.Commands.UpdatePants;
using dafstore.Application.Products.Queries.GetPants;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dafstore.API.Endpoints.Products;

public class PantsEndpoints : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAllPantsAsync)
            .MapGet(GetPantsByIdAsync, "{id}")
            .MapPost(CreatePantsAsync)
            .MapPut(UpdatePants, "{id}");
    }

    public async Task<Results<Ok<IEnumerable<PantsDTO>>, ValidationProblem>> GetAllPantsAsync(
        [AsParameters] GetAllPantsQuery query,
        ISender sender,
        IValidator<GetAllPantsQuery> validator)
    {
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return TypedResults.Ok(await sender.Send(query));
    }

    //TODO:Fazer o get especifico por tipo para pants shirts e shorts

    public async Task<Results<Ok<PantsDTO>, NotFound>> GetPantsByIdAsync(
        [FromRoute] Guid id,
        ISender sender)
    {
        var result = await sender.Send(new GetPantsByIdQuery(id));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreatePantsAsync(
        [FromBody] CreatePantsCommand command, 
        ISender sender,
        CreatePantsRequestValidation validator,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{nameof(PantsEndpoints)}/{id}", id);
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreatePants(
        [FromBody] CreatePantsCommand command,
        ISender sender,
        IValidator<CreatePantsCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{nameof(PantsEndpoints)}/{id}", id);
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdatePants(
        [FromRoute] Guid id, 
        [FromBody] UpdatePantsCommand command,
        ISender sender,
        IValidator<UpdatePantsCommand> validator, 
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}