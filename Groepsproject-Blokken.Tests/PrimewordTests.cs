namespace Groepsproject_Blokken.Tests
{
    public class PrimewordTests
    {
        [Test]
        public void PrimewordTest()
        {
            //Arrange
            PrimeWord eenPrimeword = new PrimeWord();
            eenPrimeword.Primeword = "Banaan";
            eenPrimeword.Hint = "Fruit";
            //Act
            bool iets = eenPrimeword.CheckAnswerIfPrimeWord("Banaan");

            //Assert
            Assert.IsTrue(iets);
        }

    }
}

