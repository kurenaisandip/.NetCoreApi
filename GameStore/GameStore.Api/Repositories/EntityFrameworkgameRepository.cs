using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class EntityFrameworkgameRepository : IGameRepository
{

    private readonly GameStoreContext dbContext;

    public EntityFrameworkgameRepository(GameStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<Games> GetAll()
    {
        return dbContext.Games.AsNoTracking().ToList();
    }

    public Games? Get(int id)
    {
        return dbContext.Games.Find(id);
    }

    public void Create(Games game)
    {
        dbContext.Games.Add(game);
        dbContext.SaveChanges();
    }

    public void Update(Games updatedGames)
    {
        dbContext.Games.Update(updatedGames);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
    }


}