using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.EndPoints;

public static class GameEndPOints
{
    const string GetGameEndPointName = "GetGameById";

    public static RouteGroupBuilder MapGamesEndPoints(this IEndpointRouteBuilder routes)
    {

        var group = routes.MapGroup("/games")
            .WithParameterValidation();


        group.MapGet("/", async (IGameRepository repository) =>
        (await repository.GetAll()).Select(Games => Games.AsDto()));
        group.MapGet("/{id}", async (IGameRepository repository, int id) =>
        {
            Games? game = await repository.GetAsync(id);
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
        }).WithName(GetGameEndPointName);

        routes.MapPost("/game", async (IGameRepository repository, CreateGameDto gameDto) =>
        {
            Games games = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            await repository.CreateAsync(games);
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = games.Id }, games);
        });

        routes.MapPut("/game/{id}", async (IGameRepository repository, int id, UpdateGameDto updatedGameDto) =>
        {
            // Validate input
            if (updatedGameDto == null)
            {
                return Results.BadRequest("Invalid game data provided.");
            }

            Games? existingGame = await repository.GetAsync(id);
            if (existingGame == null)
            {
                return Results.NotFound("Game not found with the provided ID.");
            }

            // Update existing game
            existingGame.Name = updatedGameDto.Name;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.Price = updatedGameDto.Price;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
            existingGame.ImageUri = updatedGameDto.ImageUri;

            await repository.UpdateAsync(existingGame);

            // Return success
            return Results.NoContent();
        });


        routes.MapDelete("/game/{id}", async (IGameRepository repository, int id) =>
        {

            Games? game = await repository.GetAsync(id);
            if (game is not null)
            {
                await repository.DeleteAsync(id);
            }



            // Return success
            return Results.NoContent();
        });

        return group;
    }
}