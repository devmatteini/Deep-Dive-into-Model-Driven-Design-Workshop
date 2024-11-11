using System.Linq;
using ExternalDependencies.AuditoriumLayoutRepository;
using ExternalDependencies.ReservationsProvider;
using NFluent;
using NUnit.Framework;

namespace SeatsSuggestions.Tests.UnitTests;

[TestFixture]
public class AuditoriumSeatingArrangementShould
{
    [Test]
    public void Be_a_Value_Type()
    {
        var auditoriumSeatingArrangements =
            new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());
        var showIdWithoutReservationYet = "18";
        var auditoriumSeatingArrangementFirstInstance =
            auditoriumSeatingArrangements.FindByShowId(showIdWithoutReservationYet);
        var auditoriumSeatingArrangementSecondInstance =
            auditoriumSeatingArrangements.FindByShowId(showIdWithoutReservationYet);

        // Two different instances with same values should be equals
        Check.That(auditoriumSeatingArrangementSecondInstance).IsEqualTo(auditoriumSeatingArrangementFirstInstance);

        // Should not mutate existing instance
        auditoriumSeatingArrangementSecondInstance.Rows.Values.First().SeatingPlaces.First().Allocate();
        Check.That(auditoriumSeatingArrangementSecondInstance).IsEqualTo(auditoriumSeatingArrangementFirstInstance);
    }
}