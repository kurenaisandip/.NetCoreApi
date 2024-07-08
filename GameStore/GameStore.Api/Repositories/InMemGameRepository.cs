using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public class InMemGameRepository : IGameRepository
{
    private readonly List<Games> games = new List<Games>
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

    public async Task<IEnumerable<Games>> GetAll()
    {
        return await Task.FromResult(games);
    }

    public async Task<Games?> GetAsync(int id)
    {
        return await Task.FromResult(games.Find(games => games.Id == id));
    }

    public async Task CreateAsync(Games game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Games updatedGames)
    {
        var index = games.FindIndex(games => games.Id == updatedGames.Id);
        games[index] = updatedGames;

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(games => games.Id == id);
        games.RemoveAt(index);

        await Task.CompletedTask;
    }
}