﻿namespace SeatsSuggestions;

public class AuditoriumSeatingArrangement(Dictionary<string, Row> rows)
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
}