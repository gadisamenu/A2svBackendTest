
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class AppRoleEntityConfiguration : IEntityTypeConfiguration<AppRole>
{
    private const string UserRoleId = "5970d313-8ead-434b-a1cb-cacbc6b5c0e9";
    private const string AppUser = "User";
   
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {


        var User = new AppRole
        {
            Id = UserRoleId,
            Name = AppUser,
            NormalizedName = AppUser.ToUpperInvariant()
        };
        builder.HasData(User);
    }
}