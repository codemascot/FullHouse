using Moq;
using FullHouse.Interface;


namespace FullHouse.Tests
{
    public class FullHouseTests
    {
        [Theory]
        [InlineData(new string[]{"K;S","4;D","K;C","K;H","K;D"}, "FOUR_OF_A_KIND")]
        [InlineData(new string[]{"5;C","6;D","6;H","5;D","6;C"}, "FULL_HOUSE")]
        [InlineData(new string[]{"A;S","2;D","3;H","4;C","8;H"}, "NEITHER")]
        [InlineData(new string[]{"A;D","A;H","Q;S","A;S","Q;D"}, "FULL_HOUSE")]
        [InlineData(new string[]{"5;S","6;S","7;S","8;S","9;S"}, "NEITHER")]
        [InlineData(new string[]{"10;S","A;C","10;H","10;D","10;C"}, "FOUR_OF_A_KIND")]
        [InlineData(new string[]{"J;S","K;D","J;H","K;C","9;S"}, "NEITHER")]
        [InlineData(new string[]{"8;S","8;D","10;H","8;C","8;H"}, "FOUR_OF_A_KIND")]
        [InlineData(new string[]{"5;H","6;H","2;H","K;H","Q;H"}, "NEITHER")]
        public void FullHouse_Hand_ReturnsCorrectOutput(string[] cards, string expResult)
        {
            // Arrange
            IFullHouse hand = new FullHouse();

            // Act
            string result = hand.Hand(cards);

            // Assert
            Assert.Equal(expResult, result);
        }

        [Theory]
        [InlineData(new string[]{"2;S","2;C","3;S","2;S","3;D","4;D"}, "Exactly 5 cards required!")]
        [InlineData(new string[]{"2;S","2;C","3;S","2;S","3;D"}, "Contains a duplicate card!")]
        [InlineData(new string[]{"2;S","2;C","3;S","2;H","3;D;D"}, "3;D;D is invalid!")]
        [InlineData(new string[]{"2;S","2;C","3;S","2;H","3D"}, "3D is invalid!")]
        public void FullHouse_Hand_ThrowsException(string[] cards, string expResult)
        {
            // Arrange
            IFullHouse hand = new FullHouse();

            // Act
            Action act = () => hand.Hand(cards);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal(exception.Message, expResult);
        }
    }   
}
