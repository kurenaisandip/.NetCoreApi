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

    public IEnumerable<Games> GetAll()
    {
        return games;
    }

    public Games? Get(int id)
    {
        return games.Find(games => games.Id == id);
    }

    public void Create(Games game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
    }

    public void Update(Games updatedGames)
    {
        var index = games.FindIndex(games => games.Id == updatedGames.Id);
        games[index] = updatedGames;
    }

    public void Delete(int id)
    {
        var index = games.FindIndex(games => games.Id == id);
        games.RemoveAt(index);
    }
}