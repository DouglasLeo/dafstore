using dafstore.API.Infrastructure;
using dafstore.Application.ShoppingCarts.Commands.CreateShoppingCart;
using dafstore.Application.ShoppingCarts.Commands.DeleteShoppingCart;
using dafstore.Application.ShoppingCarts.Commands.UpdateShoppingCart;
using dafstore.Application.ShoppingCarts.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dafstore.API.Endpoints.ShoppingCart;

public class ShoppingCartEndpoints : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetShoppingCartByUserAsync, "{userId}")
            .MapPost(CreateShoppingCart)
            .MapPut(UpdateShoppingCart, "{id}")
            .MapDelete(DeleteShoppingCart, "{id}");
    }

    public async Task<Results<Ok<ShoppingCartDTO>, NotFound>> GetShoppingCartByUserAsync(ISender sender, Guid userId)
    {
        var result = await sender.Send(new GetShoppingCartByUserIdQuery(userId));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreateShoppingCart(ISender sender,
        IValidator<CreateShoppingCartCommand> validator, [FromBody] CreateShoppingCartCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{nameof(ShoppingCartEndpoints)}/{id}", id);
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateShoppingCart(ISender sender,
        IValidator<UpdateShoppingCartCommand> validator, Guid id, [FromBody] UpdateShoppingCartCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    public async Task<Results<NoContent, NotFound, BadRequest>> DeleteShoppingCart(ISender sender, Guid id,
        [FromBody] DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}