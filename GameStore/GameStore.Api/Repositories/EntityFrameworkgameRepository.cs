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

    public async Task<IEnumerable<Games>> GetAll()
    {
        return await dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task<Games?> GetAsync(int id)
    {
        return await dbContext.Games.FindAsync(id);
    }

    public async Task CreateAsync(Games game)
    {
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Games updatedGames)
    {
        dbContext.Games.Update(updatedGames);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
    }


}