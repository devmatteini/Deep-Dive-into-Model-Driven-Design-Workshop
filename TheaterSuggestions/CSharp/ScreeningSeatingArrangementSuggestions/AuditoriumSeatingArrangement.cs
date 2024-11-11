using Value.Shared;

namespace SeatsSuggestions;

public class AuditoriumSeatingArrangement(Dictionary<string, Row> rows): Value.ValueType<AuditoriumSeatingArrangement>
{
    public IReadOnlyDictionary<string, Row> Rows => rows;

    public SeatingOptionIsSuggested SuggestSeatingOptionFor(int partyRequested, PricingCategory pricingCategory)
    {
        foreach (var row in rows.Values)
        {
            var seatOptionsSuggested = row.SuggestSeatingOption(partyRequested, pricingCategory);

            if (seatOptionsSuggested.MatchExpectation()) return seatOptionsSuggested;
        }

        return new SeatingOptionIsNotAvailable(partyRequested, pricingCategory);
    }

    public AuditoriumSeatingArrangement Allocate(List<SeatingPlace> allocatedSeatingPlaces)
    {
        var newVersionOfRows = new Dictionary<string, Row>(Rows);
        foreach (var seatingPlace in allocatedSeatingPlaces)
        {
            newVersionOfRows[seatingPlace.RowName] = newVersionOfRows[seatingPlace.RowName].Allocate(seatingPlace);
        }
        return new(newVersionOfRows);
    }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return new object[] { new DictionaryByValue<string, Row>(new Dictionary<string, Row>(Rows)) };
    }
}