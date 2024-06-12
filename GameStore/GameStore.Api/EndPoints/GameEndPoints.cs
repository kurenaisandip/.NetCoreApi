using GameStore.Api.Entities;

namespace GameStore.Api.EndPoints;

public static class GameEndPOints
{

    const string GetGameEndPointName = "GetGameById";



    static List<Games> games = new List<Games>
{
    new Games
    {
        Id = 1,
        Name = "Super Mario 64",
        Genre = "Platformer",
        Price = 59.99M,
        ReleaseDate = new DateTime(1996, 6, 23),
        ImageUri = "https://placehold.co/100"
    },
      new Games
    {
        Id = 2,
        Name = "Genshin Impact",
        Genre = "Anime",
        Price = 0M,
        ReleaseDate = new DateTime(1996, 6, 23),
        ImageUri = "https://placehold.co/100"
    },
      new Games
    {
        Id = 3,
        Name = "Fifa 22",
        Genre = "Sports",
        Price = 59.99M,
        ReleaseDate = new DateTime(1996, 6, 23),
        ImageUri = "https://placehold.co/100"
    }
};

    public static RouteGroupBuilder MapGamesEndPoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games")
            .WithParameterValidation();


        group.MapGet("/", () => games);
        group.MapGet("/{id}", (int id) =>
        {
            Games? game = games.Find(games => games.Id == id);
            if (game == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(game);
        }).WithName(GetGameEndPointName);

        routes.MapPost("/game", (Games game) =>
        {
            game.Id = games.Max(game => game.Id) + 1;
            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        routes.MapPut("/game/{id}", (int id, Games updatedGame) =>
        {
            // Validate input
            if (updatedGame == null)
            {
                return Results.BadRequest("Invalid game data provided.");
            }

            Games? existingGame = games.Find(games => games.Id == id);
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

            // Handle concurrency or other potential errors

            // Return success
            return Results.NoContent();
        });


        routes.MapDelete("/game/{id}", (int id) =>
        {

            Games? game = games.Find(games => games.Id == id);
            if (game is not null)
            {
                games.Remove(game);
            }



            // Return success
            return Results.NoContent();
        });

        return group;
    }
}