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


        group.MapGet("/", (IGameRepository repository) =>
        repository.GetAll().Select(Games => Games.AsDto()));
        group.MapGet("/{id}", (IGameRepository repository, int id) =>
        {
            Games? game = repository.Get(id);
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
        }).WithName(GetGameEndPointName);

        routes.MapPost("/game", (IGameRepository repository, CreateGameDto gameDto) =>
        {
            Games games = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            repository.Create(games);
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = games.Id }, games);
        });

        routes.MapPut("/game/{id}", (IGameRepository repository, int id, UpdateGameDto updatedGameDto) =>
        {
            // Validate input
            if (updatedGameDto == null)
            {
                return Results.BadRequest("Invalid game data provided.");
            }

            Games? existingGame = repository.Get(id);
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

            repository.Update(existingGame);

            // Return success
            return Results.NoContent();
        });


        routes.MapDelete("/game/{id}", (IGameRepository repository, int id) =>
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