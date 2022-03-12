namespace Trainer.Common
{
    using System;

    public static class CodeGenerator
    {
        public static string GenerateCode()
        {
            var generator = new Random();

            return generator.Next(0, 9999).ToString("D4");
        }
    }
}
