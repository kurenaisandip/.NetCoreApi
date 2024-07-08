using GameStore.Api.Data;
using GameStore.Api.EndPoints;
using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGameRepository, InMemGameRepository>();

var connString = builder.Configuration.GetConnectionString("GameStoreContext");
builder.Services.AddSqlServer<GameStoreContext>(connString);

var app = builder.Build();

//instead of this a new extension method is created, to keep the code clean 
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
//     dbContext.Database.Migrate();
// }

app.Services.InitializeDb();

app.MapGamesEndPoints();

app.Run();
