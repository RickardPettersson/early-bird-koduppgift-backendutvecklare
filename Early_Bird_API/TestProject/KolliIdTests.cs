using Early_Bird_API.Model;

namespace TestProject
{
    public class KolliIdTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Valid_KolliId()
        {
            List<string> errors = new List<string>();

            Early_Bird_API.Misc.ValidationHelper.ValidateKolliId("999123456789123456", errors);

            if (errors.Count == 0)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Not_Valid_KolliId()
        {
            List<string> errors = new List<string>();

            Early_Bird_API.Misc.ValidationHelper.ValidateKolliId("123", errors);

            if (errors.Count == 0)
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