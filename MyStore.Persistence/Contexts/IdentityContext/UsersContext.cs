using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyStore.Application.Contexts;
using MyStore.Common.Enums.RoleNamesEnum;
using MyStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Persistence.Contexts
{
    public class UsersContext : IdentityDbContext<User, Role, string>, IUsersDbContext
    {
        public UsersContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });
            builder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId, p.LoginProvider, p.Name });

            builder.Entity<Role>().HasData(new Role { Name = RoleName.Admin.ToString(),NormalizedName = RoleName.Admin.ToString().ToUpper() });
            builder.Entity<Role>().HasData(new Role { Name = RoleName.Operator.ToString(),NormalizedName = RoleName.Operator.ToString().ToUpper() });
            builder.Entity<Role>().HasData(new Role { Name = RoleName.User.ToString(),NormalizedName = RoleName.User.ToString().ToUpper() });
        }


    }
}
