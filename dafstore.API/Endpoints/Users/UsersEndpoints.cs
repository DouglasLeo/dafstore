using dafstore.API.Infrastructure;
using dafstore.Application.Users.Commands.CreateUser;
using dafstore.Application.Users.Commands.DeleteUser;
using dafstore.Application.Users.Commands.UpdateUser;
using dafstore.Application.Users.Queries.GetUsers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dafstore.API.Endpoints.Users;

public class UsersEndpoints : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetAllUsersAsync)
            .MapGet(GetUserByIdAsync, "{id}")
            .MapGet(GetUserByEmailAsync, "email/{email}")
            .MapGet(GetUserByPhoneAsync, "phone/{phone}")
            .MapPost(CreateUser)
            .MapPut(UpdateUser, "{id}")
            .MapDelete(DeleteUser, "{id}");
    }

    public async Task<Results<Ok<IEnumerable<UserDTO>>, ValidationProblem>> GetAllUsersAsync(ISender sender,
        IValidator<GetAllUsersQuery> validator, [AsParameters] GetAllUsersQuery query)
    {
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return TypedResults.Ok(await sender.Send(query));
    }

    public async Task<Results<Ok<UserDTO>, NotFound>> GetUserByIdAsync(ISender sender, Guid id)
    {
        var result = await sender.Send(new GetUserByIdQuery(id));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Ok<UserDTO>, NotFound, ValidationProblem>> GetUserByEmailAsync(ISender sender,
        IValidator<GetUserByEmailQuery> validator, string email)
    {
        var query = new GetUserByEmailQuery(email);
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(query);

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Ok<UserDTO>, NotFound, ValidationProblem>> GetUserByPhoneAsync(ISender sender,
        IValidator<GetUserByPhoneQuery> validator, string phone)
    {
        var query = new GetUserByPhoneQuery(phone);
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(query);

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreateUser(ISender sender,
        IValidator<CreateUserCommand> validator, [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{nameof(UsersEndpoints)}/{id}", id);
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateUser(ISender sender,
        IValidator<UpdateUserCommand> validator, Guid id, [FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    public async Task<Results<NoContent, NotFound, BadRequest>> DeleteUser(ISender sender, Guid id,
        [FromBody] DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}