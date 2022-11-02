using InlineCalculator;

namespace TestProject1;

public class Tests
{
    [Test]
    public void Test()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Calculator.Solve("(2+2)*(3+3)"), Is.EqualTo(24f));
        });
    }
}