using dafstore.API.Infrastructure;
using dafstore.Application.Products.Commands.CreatePants;
using dafstore.Application.Products.Commands.UpdatePants;
using dafstore.Application.Products.Queries.GetPants;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dafstore.API.Endpoints.Products;

public class PantsEndpoints : ProductsEndpoints<PantsDTO>
{
    public override void Map(WebApplication app)
    {
        base.Map(app);
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetAllPantsAsync)
            .MapGet(GetPantsByIdAsync, "{id}")
            .MapPost(CreatePantsAsync)
            .MapPut(UpdatePants, "{id}");
    }

    public async Task<Results<Ok<IEnumerable<PantsDTO>>, ValidationProblem>> GetAllPantsAsync(ISender sender,
        IValidator<GetAllPantsQuery> validator, [AsParameters] GetAllPantsQuery query)
    {
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return TypedResults.Ok(await sender.Send(query));
    }

    //TODO:Fazer o get especifico por tipo para pants shirts e shorts

    public async Task<Results<Ok<PantsDTO>, NotFound>> GetPantsByIdAsync(ISender sender, Guid id)
    {
        var result = await sender.Send(new GetPantsByIdQuery(id));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreatePantsAsync(ISender sender,
        CreatePantsCommand command, CreatePantsRequestValidation validator,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{nameof(PantsEndpoints)}/{id}", id);
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreatePants(ISender sender,
        IValidator<CreatePantsCommand> validator, [FromBody] CreatePantsCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);
        //TODO:Verificar URL Final
        return TypedResults.Created($"/{nameof(PantsEndpoints)}/{id}", id);
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdatePants(ISender sender,
        IValidator<UpdatePantsCommand> validator, Guid id, [FromBody] UpdatePantsCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}