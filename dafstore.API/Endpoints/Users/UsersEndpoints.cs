using dafstore.API.Infrastructure;
using dafstore.Application.Users.Commands.AuthenticateUser;
using dafstore.Application.Users.Commands.CreateRole;
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
        var group = app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAllUsersAsync)
            .MapGet(GetUserByIdAsync, "{id}")
            .MapGet(GetUserByEmailAsync, "email/{email}")
            .MapGet(GetUserByPhoneAsync, "phone/{phone}")
            .MapPost(CreateRole, "role")
            .MapPut(UpdateUser, "{id}")
            .MapDelete(DeleteUser, "{id}");
        
            group.MapPostAllowAnonymous(AuthenticateUser, "authenticate");
            group.MapPostAllowAnonymous(CreateUser);
    }

    public async Task<Results<Ok<IEnumerable<UserDTO>>, ValidationProblem>> GetAllUsersAsync(
        [AsParameters] GetAllUsersQuery query,
        ISender sender,
        IValidator<GetAllUsersQuery> validator)
    {
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return TypedResults.Ok(await sender.Send(query));
    }

    public async Task<Results<Ok<UserDTO>, NotFound>> GetUserByIdAsync(
        [FromRoute] Guid id,
        ISender sender)
    {
        var result = await sender.Send(new GetUserByIdQuery(id));

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Ok<UserDTO>, NotFound, ValidationProblem>> GetUserByEmailAsync(
        [FromRoute] string email,
        ISender sender,
        IValidator<GetUserByEmailQuery> validator)
    {
        var query = new GetUserByEmailQuery(email);
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(query);

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Ok<UserDTO>, NotFound, ValidationProblem>> GetUserByPhoneAsync(
        [FromRoute] string phone,
        ISender sender,
        IValidator<GetUserByPhoneQuery> validator)
    {
        var query = new GetUserByPhoneQuery(phone);
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(query);

        return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Results<Ok<AuthenticateDTO>, BadRequest, ValidationProblem>> AuthenticateUser(
        [FromBody] AuthenticateUserCommand command,
        ISender sender,
        IValidator<AuthenticateUserCommand> validator)
    {
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command);

        return result != null ? TypedResults.Ok(result) : TypedResults.BadRequest();
    }

    public async Task<Results<Created<Guid>, ValidationProblem>> CreateUser(
        [FromBody] CreateUserCommand command,
        ISender sender,
        IValidator<CreateUserCommand> validator, 
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{nameof(UsersEndpoints)}/{id}", id);
    }

    public async Task<Results<Created, InternalServerError, ValidationProblem>> CreateRole(
        [FromBody] CreateRoleCommand command,
        ISender sender,
        IValidator<CreateRoleCommand> validator, 
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return id != Guid.Empty ? TypedResults.Created() : TypedResults.InternalServerError();
    }

    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateUser(
        [FromRoute] Guid id, 
        [FromBody] UpdateUserCommand command,
        ISender sender,
        IValidator<UpdateUserCommand> validator, 
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    public async Task<Results<NoContent, NotFound, BadRequest>> DeleteUser(
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