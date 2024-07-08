using GameStore.Api.Data;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

//instead of this a new extension method is created, to keep the code clean 
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
//     dbContext.Database.Migrate();
// }

await app.Services.InitializeDbAsync();

app.MapGamesEndPoints();

app.Run();
