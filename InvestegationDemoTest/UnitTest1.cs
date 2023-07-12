namespace InvestegationDemoTest
{
    public class UnitTest1
    {

        [Fact]
        public void Add()
        {
            //arrange
            double a = 5;
            double b = 3;
            double expected = 18;

            //act
            var actual = InvestegationDemoCICDMigration.Helpers.CommonHelpers.AddTwoNumbers(a, b);

            //Assert
            Assert.Equal(expected, actual, 0);
        }
    }
}
