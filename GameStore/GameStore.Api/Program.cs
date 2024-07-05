using GameStore.Api.EndPoints;
using GameStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGameRepository, InMemGameRepository>();

var connString = builder.Configuration.GetConnectionString("GameStoreContext");

var app = builder.Build();


app.MapGamesEndPoints();

app.Run();
