using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.EndPoints;

public static class GameEndPOints
{
    const string GetGameEndPointName = "GetGameById";

    public static RouteGroupBuilder MapGamesEndPoints(this IEndpointRouteBuilder routes)
    {
        InMemGameRepository repository = new();

        var group = routes.MapGroup("/games")
            .WithParameterValidation();


        group.MapGet("/", () => repository.GetAll());
        group.MapGet("/{id}", (int id) =>
        {
            Games? game = repository.Get(id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        }).WithName(GetGameEndPointName);

        routes.MapPost("/game", (Games game) =>
        {
            repository.Create(game);
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        routes.MapPut("/game/{id}", (int id, Games updatedGame) =>
        {
            // Validate input
            if (updatedGame == null)
            {
                return Results.BadRequest("Invalid game data provided.");
            }

            Games? existingGame = repository.Get(id);
            if (existingGame == null)
            {
                return Results.NotFound("Game not found with the provided ID.");
            }

            // Update existing game
            existingGame.Name = updatedGame.Name;
            existingGame.Genre = updatedGame.Genre;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
            existingGame.ImageUri = updatedGame.ImageUri;

            repository.Update(existingGame);

            // Return success
            return Results.NoContent();
        });


        routes.MapDelete("/game/{id}", (int id) =>
        {

            Games? game = repository.Get(id);
            if (game is not null)
            {
                repository.Delete(id);
            }



            // Return success
            return Results.NoContent();
        });

        return group;
    }
}