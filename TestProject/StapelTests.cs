using ClassLibrary;
using Xunit;

namespace TestProject
{
    public class StapelTests
    {
        Stapel<string> stapel = new Stapel<string>();

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("c")]
        public void TestPush(string x)
        {
            //Act
            stapel.Push(x);

            //Assert
            Assert.Equal(1, stapel.Zoek(x));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("c")]
        public void TestPop(string x)
        {
            //Act
            stapel.Push(x);
            stapel.Pop();

            //Assert
            Assert.Equal(-1, stapel.Zoek(x));
        }

        [Fact]
        public void TestZoek()
        {
            //Arrange
            string y = "a";
            string inhoud1 = "a";
            string inhoud2 = "b";

            //Act
            stapel.Push(y);

            //Assert
            Assert.Equal(1, stapel.Zoek(inhoud1));
            Assert.Equal(-1, stapel.Zoek(inhoud2));
        }

        [Fact]
        public void TestPrint()
        {
            //Arrange
            string x = "5";
            string y = "41";

            //Act
            //Assert
            stapel.Push(x);
            Assert.Equal("5", stapel.Print());
            stapel.Push(y);
            Assert.Equal("41 5", stapel.Print());

        }
    }
}
