namespace Trainer.Common
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class PasswordsHelper
    {
        public static bool IsMeetsRequirements(string password)
        {
            if (password == null)
            {
                return false;
            }

            if (password.Length < 8)
            {
                return false;
            }

            if (password.Length > 50)
            {
                return false;
            }

            if (!password.Any(x => char.IsUpper(x)))
            {
                return false;
            }

            if (!password.Any(x => char.IsLower(x)))
            {
                return false;
            }

            if (!password.Any(x => char.IsDigit(x)))
            {
                return false;
            }

            if (!IsSuitableCharacters(password))
            {
                return false;
            }

            return true;
        }

        private static bool IsSuitableCharacters(string password)
        {
            var regex = new Regex("^[A-Za-z0-9!@#$%^&*_-]*$");

            return regex.IsMatch(password);
        }
    }
}
