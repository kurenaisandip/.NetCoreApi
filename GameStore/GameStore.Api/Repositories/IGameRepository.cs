using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public interface IGameRepository
{
    IEnumerable<Games> GetAll();
    Games? Get(int id);
    void Create(Games game);
    void Update(Games updatedGames);
    void Delete(int id);
}