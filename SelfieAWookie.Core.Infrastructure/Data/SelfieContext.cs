using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Domain;
using SelfieAWookie.core.Framework;
using SelfieAWookie.Core.Infrastructure.Data.TypeConfiguration;

namespace SelfieAWookie.Core.Infrastructure.Data
{
    public class SelfieContext : DbContext, IUnitOfWork
    {
        #region Internal methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SelfieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WookieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoEntityTypeConfiguration());
        }

        #endregion

        #region constructors

        public SelfieContext(DbContextOptions<SelfieContext> options) : base(options)
        {
        }

        public SelfieContext()
        {
        }

        #endregion

        #region Properties

        public DbSet<Selfie> Selfies { get; set; }

        public DbSet<Wookie> Wookies { get; set; }

        public DbSet<Photo> Photos { get; set; }

        #endregion
    }
}