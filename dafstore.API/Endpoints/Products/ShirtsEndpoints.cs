using dafstore.API.Infrastructure;
using dafstore.Application.Products.Commands.CreateShirts;
using dafstore.Application.Products.Commands.UpdateShirt;
using dafstore.Application.Products.Queries.GetShirt;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dafstore.API.Endpoints.Products
{
    public class ShirtsEndpoints : ProductsEndpoints<ShirtDTO>
    {
        public override void Map(WebApplication app)
        {
            base.Map(app);
            app.MapGroup(this)
                //.RequireAuthorization()
                .MapGet(GetAllShirtAsync)
                .MapGet(GetShirtByIdAsync, "{id}")
                .MapPost(CreateShirt)
                .MapPut(UpdateShirt, "{id}");
        }

        public async Task<Results<Ok<IEnumerable<ShirtDTO>>, ValidationProblem>> GetAllShirtAsync(ISender sender,
            IValidator<GetAllShirtsQuery> validator, [AsParameters] GetAllShirtsQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

            return TypedResults.Ok(await sender.Send(query));
        }

        public async Task<Results<Ok<ShirtDTO>, NotFound>> GetShirtByIdAsync(ISender sender, Guid id)
        {
            var result = await sender.Send(new GetShirtByIdQuery(id));

            return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        public async Task<Results<Created<Guid>, ValidationProblem>> CreateShirt(ISender sender,
            IValidator<CreateShirtCommand> validator, [FromBody] CreateShirtCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

            var id = await sender.Send(command, cancellationToken);
            //TODO:Verificar URL Final
            return TypedResults.Created($"/{nameof(ShirtsEndpoints)}/{id}", id);
        }

        public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateShirt(ISender sender,
            IValidator<UpdateShirtCommand> validator, Guid id, [FromBody] UpdateShirtCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id) return TypedResults.BadRequest();

            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

            var result = await sender.Send(command, cancellationToken);

            return result != Guid.Empty ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}