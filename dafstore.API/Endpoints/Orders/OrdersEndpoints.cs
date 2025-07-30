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
            //.RequireAuthorization()
            .MapGet(GetOrderByIdAsync, "{id}")
            .MapGet(GetOrderByIdAsync, "{userId}")
            .MapPost(CreateOrder)
            .MapPut(UpdateOrder, "{id}");
    }

    public async Task<Results<Ok<OrderDTO>, NotFound>> GetOrderByIdAsync(ISender sender, Guid id)
    {
        var result = await sender.Send(new GetOrderByIdQuery(id));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Ok<OrderDTO>, NotFound>> GetOrderByUserIdAsync(ISender sender, Guid userId)
    {
        var result = await sender.Send(new GetOrderByUserIdQuery(userId));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreateOrder(ISender sender,
        IValidator<CreateOrderCommand> validator, [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{nameof(OrdersEndpoints)}/{id}", id);
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateOrder(ISender sender,
        IValidator<UpdateOrderCommand> validator, Guid id, [FromBody] UpdateOrderCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}