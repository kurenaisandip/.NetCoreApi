using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Data.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Games>
{
    public void Configure(EntityTypeBuilder<Games> builder)
    {
        builder.Property(Games => Games.Price).HasPrecision(5, 2);
    }
}