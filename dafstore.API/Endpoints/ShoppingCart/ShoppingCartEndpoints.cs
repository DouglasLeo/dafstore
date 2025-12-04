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
            .RequireAuthorization()
            .MapGet(GetShoppingCartByUserAsync, "{userId}")
            .MapPost(CreateShoppingCart)
            .MapPut(UpdateShoppingCart, "{id}")
            .MapDelete(DeleteShoppingCart, "{id}");
    }

    public async Task<Results<Ok<ShoppingCartDTO>, NotFound>> GetShoppingCartByUserAsync(
        [FromRoute] Guid userId,
        ISender sender) 
    {
        var result = await sender.Send(new GetShoppingCartByUserIdQuery(userId));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Created<Guid>,BadRequest, ValidationProblem>> CreateShoppingCart(
        [FromBody] CreateShoppingCartCommand command,
        ISender sender,
        IValidator<CreateShoppingCartCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        //TODO: Criar endpoint para adicionar items no carrinho
        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return id == Guid.Empty ? TypedResults.BadRequest() :TypedResults.Created($"/{nameof(ShoppingCartEndpoints)}/{id}", id);
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateShoppingCart(
        [FromRoute] Guid id, 
        [FromBody] UpdateShoppingCartCommand command,
        ISender sender,
        IValidator<UpdateShoppingCartCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    public async Task<Results<NoContent, NotFound, BadRequest>> DeleteShoppingCart(
        [FromRoute] Guid id,
        [FromBody] DeleteUserCommand command,
        ISender sender, 
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}