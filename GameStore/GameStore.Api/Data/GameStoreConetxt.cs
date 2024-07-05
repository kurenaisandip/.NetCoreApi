using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {

    }

    public DbSet<Games> Games => Set<Games>();
}