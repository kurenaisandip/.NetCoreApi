using GameStore.Api.EndPoints;
using GameStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGameRepository, InMemGameRepository>();

var app = builder.Build();


app.MapGamesEndPoints();

app.Run();
