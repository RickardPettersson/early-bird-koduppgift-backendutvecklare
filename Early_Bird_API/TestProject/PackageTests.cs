using Early_Bird_API.Model;

namespace TestProject
{
    public class PackageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Valid_Package()
        {
            var p = new Package()
            {
                KolliId = "999123456789123456",
                Weight = 2,
                Length = 2,
                Height = 2,
                Width = 2
            };

            if (p.IsValid)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Not_Valid_Package()
        {
            var p = new Package()
            {
                KolliId = "999123456789123456",
                Weight = 999,
                Length = 999,
                Height = 999,
                Width = 999
            };

            if (p.IsValid)
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass(); 
            }
        }
    }
}