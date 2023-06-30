using Early_Bird_API.Model;

namespace Early_Bird_API.Misc
{
    public class DefaultPackages
    {
        // A default list of a valid package and a not valid package, if memory cache not found
        public static List<Package> List()
        {
            return new List<Package>() { 
                // Valid package
                new Package() {
                    KolliId = "999123456789123456",
                    Weight = 1,
                    Length = 2,
                    Height = 3,
                    Width = 4
                },
                // Not valid package
                new Package() {
                    KolliId = "999123456789987654",
                    Weight = 99,
                    Length = 27,
                    Height = 54,
                    Width = 74
                }
            };
        }
    }
}
