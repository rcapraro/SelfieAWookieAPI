using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookie.Core.Domain;

namespace SelfieAWookie.Core.Infrastructure.Data.TypeConfiguration
{
    public class SelfieEntityTypeConfiguration : IEntityTypeConfiguration<Selfie>
    {
        #region Public methods

        public void Configure(EntityTypeBuilder<Selfie> builder)
        {
            builder.ToTable("Selfie");
            builder.HasKey(item => item.Id);
            builder.HasOne(item => item.Wookie)
                .WithMany(item => item.Selfies);
        }

        #endregion
    }
}