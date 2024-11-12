using System.Collections.Generic;
using System.Linq;
using NFluent;
using NUnit.Framework;

namespace SeatsSuggestions.Tests.UnitTests;

[TestFixture]
public class RowShould
{
    [Test]
    public void Be_a_Value_Type()
    {
        var a1 = new SeatingPlace("A", 1, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var a2 = new SeatingPlace("A", 2, PricingCategory.Second, SeatingPlaceAvailability.Available);

        // Two different instances with same values should be equals
        var rowFirstInstance = new Row("A", new List<SeatingPlace> { a1, a2 });
        var rowSecondInstance = new Row("A", new List<SeatingPlace> { a1, a2 });
        Check.That(rowSecondInstance).IsEqualTo(rowFirstInstance);

        // Should not mutate existing instance
        var a3 = new SeatingPlace("A", 2, PricingCategory.Second, SeatingPlaceAvailability.Available);
        rowSecondInstance.AddSeat(a3);
        Check.That(rowSecondInstance).IsEqualTo(rowFirstInstance);
    }

    [Test]
    public void
        Offer_seating_places_from_the_middle_of_the_row_when_the_row_size_is_even_and_party_size_is_greater_than_one()
    {
        var partySize = 2;

        var a1 = new SeatingPlace("A", 1, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var a2 = new SeatingPlace("A", 2, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var a3 = new SeatingPlace("A", 3, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a4 = new SeatingPlace("A", 4, PricingCategory.First, SeatingPlaceAvailability.Reserved);
        var a5 = new SeatingPlace("A", 5, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a6 = new SeatingPlace("A", 6, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a7 = new SeatingPlace("A", 7, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a8 = new SeatingPlace("A", 8, PricingCategory.First, SeatingPlaceAvailability.Reserved);
        var a9 = new SeatingPlace("A", 9, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var a10 = new SeatingPlace("A", 10, PricingCategory.Second, SeatingPlaceAvailability.Available);

        var row = new Row("A", new List<SeatingPlace> { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10 });

        Check.That(MakeSeatingPlacesWithDistance(row).Select(s => s.DistanceFromMiddleOfRow))
            .ContainsExactly(4, 3, 2, 1, 0, 0, 1, 2, 3, 4);

        var seatingPlaces = OfferSeatsNearerTheMiddleOfTheRow(row).OrderBy(s => s.DistanceFromMiddleOfRow)
            .Select(s => s.SeatingPlace)
            .Where(s => s.IsAvailable())
            .Where(s => s.MatchCategory(PricingCategory.Ignore))
            .Take(partySize);

        Check.That(seatingPlaces)
            .ContainsExactly(a5, a6);
    }

    [Test]
    public void
        Offer_seating_places_from_the_middle_of_the_row_when_the_row_size_is_odd_and_party_size_is_greater_than_one()
    {
        var partySize = 2;

        var a1 = new SeatingPlace("A", 1, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var a2 = new SeatingPlace("A", 2, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var a3 = new SeatingPlace("A", 3, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a4 = new SeatingPlace("A", 4, PricingCategory.First, SeatingPlaceAvailability.Reserved);
        var a5 = new SeatingPlace("A", 5, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a6 = new SeatingPlace("A", 6, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a7 = new SeatingPlace("A", 7, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a8 = new SeatingPlace("A", 8, PricingCategory.First, SeatingPlaceAvailability.Reserved);
        var a9 = new SeatingPlace("A", 9, PricingCategory.Second, SeatingPlaceAvailability.Available);

        var row = new Row("A", new List<SeatingPlace> { a1, a2, a3, a4, a5, a6, a7, a8, a9 });

        Check.That(MakeSeatingPlacesWithDistance(row).Select(s => s.DistanceFromMiddleOfRow))
            .ContainsExactly(4, 3, 2, 1, 0, 1, 2, 3, 4);

        var seatingPlaces = OfferSeatsNearerTheMiddleOfTheRow(row).OrderBy(s => s.DistanceFromMiddleOfRow)
            .Select(s => s.SeatingPlace)
            .Where(s => s.IsAvailable())
            .Where(s => s.MatchCategory(PricingCategory.Ignore))
            .Take(partySize);

        Check.That(seatingPlaces)
            .ContainsExactly(a5, a6);
    }

    private bool IsEven(Row row)
    {
        return row.SeatingPlaces.Count % 2 == 0;
    }

    private IEnumerable<SeatingPlaceWithDistance> MakeSeatingPlacesWithDistance(Row row)
    {
        var middleOfRow = row.SeatingPlaces.Count / 2;

        var leftPart = row.SeatingPlaces.Take(middleOfRow).Select(s =>
            new SeatingPlaceWithDistance(s, (IsEven(row) ? middleOfRow : middleOfRow + 1) - s.Number));
        var rightPart = row.SeatingPlaces.Skip(middleOfRow).Select(s =>
            new SeatingPlaceWithDistance(s, s.Number - middleOfRow - 1));
        return leftPart.Concat(rightPart);
    }

    private IEnumerable<SeatingPlaceWithDistance> OfferSeatsNearerTheMiddleOfTheRow(Row row)
    {
        return MakeSeatingPlacesWithDistance(row);
    }

    [Test]
    public void Offer_adjacent_seats_nearer_the_middle_of_the_row_when_the_middle_is_not_reserved()
    {
        const int partySize = 3;

        var a1 = new SeatingPlace("A", 1, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var a2 = new SeatingPlace("A", 2, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var a3 = new SeatingPlace("A", 3, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a4 = new SeatingPlace("A", 4, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a5 = new SeatingPlace("A", 5, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a6 = new SeatingPlace("A", 6, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a7 = new SeatingPlace("A", 7, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a8 = new SeatingPlace("A", 8, PricingCategory.First, SeatingPlaceAvailability.Available);
        var a9 = new SeatingPlace("A", 9, PricingCategory.Second, SeatingPlaceAvailability.Available);
        var a10 = new SeatingPlace("A", 10, PricingCategory.Second, SeatingPlaceAvailability.Available);

        var row = new Row("A", [a1, a2, a3, a4, a5, a6, a7, a8, a9, a10]);

        var seatsWithDistance = OfferSeatsNearerTheMiddleOfTheRow(row);

        var seatingPlaces = OfferAdjacentSeats(seatsWithDistance, partySize);

        Check.That(seatingPlaces).ContainsExactly(a3, a4, a5);
    }

    // Deep Modeling: probing the code should start with a prototype.
    private static IEnumerable<SeatingPlace> OfferAdjacentSeats(IEnumerable<SeatingPlaceWithDistance> seatsWithDistance,
        int partySize)
    {
        var potentiallyAdjacentSeats = new List<SeatingPlaceWithDistance>();
        var adjacentSeats = new List<AdjacentSeatingPlaces>();
        var neverBeAdjacentSeat = new SeatingPlaceWithDistance(
            new SeatingPlace("Z", -1, PricingCategory.Ignore, SeatingPlaceAvailability.Available), -1);
        var previousSeatingPlaceWithDistance = neverBeAdjacentSeat;
        foreach (var seatingPlaceWithDistance in seatsWithDistance.OrderBy(s => s.SeatingPlace.Number))
        {
            if (previousSeatingPlaceWithDistance.SeatingPlace.Number + 1 ==
                seatingPlaceWithDistance.SeatingPlace.Number)
            {
                if (potentiallyAdjacentSeats.Count == 0)
                {
                    potentiallyAdjacentSeats.AddRange(new[]
                        { previousSeatingPlaceWithDistance, seatingPlaceWithDistance });
                }
                else
                {
                    potentiallyAdjacentSeats.Add(seatingPlaceWithDistance);
                }

                if (potentiallyAdjacentSeats.Count == partySize)
                {
                    adjacentSeats.Add(new AdjacentSeatingPlaces(potentiallyAdjacentSeats));
                    potentiallyAdjacentSeats = new List<SeatingPlaceWithDistance>();
                }
            }

            previousSeatingPlaceWithDistance = seatingPlaceWithDistance;
        }

        return adjacentSeats.OrderBy(a => a.SeatingPlaces.Select(s => s.DistanceFromMiddleOfRow).Min()).First()
            .SeatingPlaces.Select(s => s.SeatingPlace);
    }
}

// TIP: create a DeepModelling directory for this type
record SeatingPlaceWithDistance(SeatingPlace SeatingPlace, int DistanceFromMiddleOfRow);

record AdjacentSeatingPlaces(IEnumerable<SeatingPlaceWithDistance> SeatingPlaces);