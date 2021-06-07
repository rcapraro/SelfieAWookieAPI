using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookie.Core.Domain;

namespace SelfieAWookie.Core.Infrastructure.Data.TypeConfiguration
{
    public class WookieEntityTypeConfiguration : IEntityTypeConfiguration<Wookie>
    {
        #region Public methods

        public void Configure(EntityTypeBuilder<Wookie> builder)
        {
            builder.ToTable("Wookie");
        }

        #endregion
    }
}