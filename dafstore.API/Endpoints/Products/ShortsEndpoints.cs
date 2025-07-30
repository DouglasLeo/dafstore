using dafstore.API.Infrastructure;
using dafstore.Application.Products.Commands.CreateShorts;
using dafstore.Application.Products.Commands.UpdateShorts;
using dafstore.Application.Products.Queries.GetShorts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dafstore.API.Endpoints.Products;

public class ShortsEndpoints : ProductsEndpoints<ShortsDTO>
{
    public override void Map(WebApplication app)
    {
        base.Map(app);
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetAllShortsAsync)
            .MapGet(GetProductByIdAsync)
            .MapPost(CreateShorts)
            .MapPut(UpdateShorts, "{id}");
    }

    public async Task<Results<Ok<IEnumerable<ShortsDTO>>, ValidationProblem>> GetAllShortsAsync(ISender sender,
        IValidator<GetAllShortsQuery> validator, [AsParameters] GetAllShortsQuery query)
    {
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return TypedResults.Ok(await sender.Send(query));
    }

    public async Task<Results<Ok<ShortsDTO>, NotFound>> GetProductByIdAsync(ISender sender, Guid id)
    {
        var result = await sender.Send(new GetShortsByIdQuery(id));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreateShorts(ISender sender,
        IValidator<CreateShortsCommand> validator, [FromBody] CreateShortsCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);
        //TODO:Verificar URL Final
        return TypedResults.Created($"/{nameof(PantsEndpoints)}/{id}", id);
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateShorts(ISender sender,
        IValidator<UpdateShortsCommand> validator, Guid id, [FromBody] UpdateShortsCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}