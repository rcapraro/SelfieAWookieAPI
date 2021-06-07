using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookie.Core.Domain;

namespace SelfieAWookie.Core.Infrastructure.Data.TypeConfiguration
{
    public class PhotoEntityTypeConfiguration : IEntityTypeConfiguration<Photo>
    {
        #region Public methods

        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photo");
        }

        #endregion
    }
}