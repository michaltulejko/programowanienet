using DatingApp.Models.Database.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatingApp.Models.Database.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>

{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.User)
            .WithMany(b => b.Photos)
            .HasForeignKey(b => b.UserId);
    }
}