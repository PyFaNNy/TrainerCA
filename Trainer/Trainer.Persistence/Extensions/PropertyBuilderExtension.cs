namespace Trainer.Persistence.Extensions
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal static class PropertyBuilderExtension
    {
        private const int PhoneLength = 16;
        private const int MediumLength = 512;
        private const int LongLength = 2048;

        public static PropertyBuilder<string> HasMediumMaxLength(this PropertyBuilder<string> propertyBuilder)
        {
            return propertyBuilder.HasMaxLength(MediumLength);
        }

        public static PropertyBuilder<string> HasPhoneMaxLength(this PropertyBuilder<string> propertyBuilder)
        {
            return propertyBuilder.HasMaxLength(PhoneLength);
        }

        public static PropertyBuilder<string> HasLongMaxLength(this PropertyBuilder<string> propertyBuilder)
        {
            return propertyBuilder.HasMaxLength(LongLength);
        }

    }
}
