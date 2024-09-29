using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Atak.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Atak.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<UsuarioAplicacao, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UsuarioAplicacao>(entidade =>
            {
                entidade.HasKey(x => x.Id);
                entidade.HasIndex(x => x.Email).IsUnique();
            });
        }
    }
}
