using Early_Bird_API.Model;

namespace Early_Bird_API.Misc
{
    public class ValidationHelper
    {
        // Max limits of the package values
        public static uint MaxWeight = 20000;
        public static uint MaxLength = 60;
        public static uint MaxHeight = 60;
        public static uint MaxWidth = 60;

        public static void ValidateKolliId(string KolliId, List<string> errors)
        {
            // Check that the KollId is exact 18 characthers long
            if (KolliId.Length != 18)
            {
                errors.Add("The \"KolliId\" value need to be exact 18 numbers long");
            }

            // Check that the KolliId starting with 999
            if (!KolliId.StartsWith("999"))
            {
                errors.Add("The \"KolliId\" value need to start with 999");
            }

            // Check if we only got numbers in the string
            if (!IsDigitsOnly(KolliId))
            {
                errors.Add("The \"KolliId\" value need to be numbers long");
            }
        }

        public static void CheckPackageValues(Package package, List<string> errors)
        {
            // Check if package weight is over 20 kilogram, the value Weight is saved in gram
            if (package.Weight > MaxWeight)
            {
                errors.Add("Package Weight is over 20 kilogram");
            }

            if (package.Length > MaxLength)
            {
                errors.Add("Package Length is over 60 centimeter");
            }

            if (package.Height > MaxHeight)
            {
                errors.Add("Package Height is over 60 centimeter");
            }

            if (package.Width > MaxWidth)
            {
                errors.Add("Package Width is over 60 centimeter");
            }
        }

        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
