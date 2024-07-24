using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
   
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
           }
}