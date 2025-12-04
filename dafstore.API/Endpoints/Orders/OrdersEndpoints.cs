using dafstore.API.Infrastructure;
using dafstore.Application.Orders.Commands.CreateOrder;
using dafstore.Application.Orders.Commands.UpdateOrder;
using dafstore.Application.Orders.Queries.GetOrders;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dafstore.API.Endpoints.Orders;

public class OrdersEndpoints : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetOrderByIdAsync, "{id}")
            .MapGet(GetOrderByUserIdAsync, "user/{userId}")
            .MapPost(CreateOrder)
            .MapPut(UpdateOrder, "{id}");
    }

    public async Task<Results<Ok<OrderDTO>, NotFound>> GetOrderByIdAsync(
        [FromRoute]Guid id,
        ISender sender)
    {
        var result = await sender.Send(new GetOrderByIdQuery(id));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Ok<IEnumerable<OrderDTO>>, NotFound>> GetOrderByUserIdAsync(
        [FromRoute] Guid userId,
        ISender sender)
    {
        var result = await sender.Send(new GetOrderByUserIdQuery(userId));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreateOrder(
        [FromBody] CreateOrderCommand command,
        ISender sender,
        IValidator<CreateOrderCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{nameof(OrdersEndpoints)}/{id}", id);
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateOrder(
        [FromRoute] Guid id, 
        [FromBody] UpdateOrderCommand command,
        ISender sender,
        IValidator<UpdateOrderCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}