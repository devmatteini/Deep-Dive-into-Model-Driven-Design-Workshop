using NFluent;
using NUnit.Framework;

namespace SeatsSuggestions.Tests.UnitTests;

[TestFixture]
public class SeatingPlaceShould
{
    [Test]
    public void Be_a_Value_Type()
    {
        // Two different instances with same values should be equals
        var firstInstance = new SeatingPlace("A", 1, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var secondInstance = new SeatingPlace("A", 1, PricingCategory.Second, SeatingPlaceAvailability.Available);
        Check.That(secondInstance).IsEqualTo(firstInstance);

        // Should not mutate existing instance
        firstInstance.Allocate();
        Check.That(secondInstance).IsEqualTo(firstInstance);
    }
}