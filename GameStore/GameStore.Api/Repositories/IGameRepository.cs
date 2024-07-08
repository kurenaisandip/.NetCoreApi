using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Games>> GetAll();
    Task<Games?> GetAsync(int id);
    Task CreateAsync(Games game);
    Task UpdateAsync(Games updatedGames);
    Task DeleteAsync(int id);
}