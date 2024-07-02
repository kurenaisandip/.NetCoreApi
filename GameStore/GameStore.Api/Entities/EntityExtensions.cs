using GameStore.Api.Dtos;

namespace GameStore.Api.Entities;

public static class EntityExtensions
{
    //Extention method to convert Game Entity into GameDto.
    //We could also use Auto mapper or other mapping frameworks 
    public static GameDto AsDto(this Games games)
    {
        return new GameDto(
            games.Id,
            games.Name,
            games.Genre,
            games.Price,
            games.ReleaseDate,
            games.ImageUri
        );
    }

}