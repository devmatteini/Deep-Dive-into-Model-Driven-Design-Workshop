package org.weaveit.seatingplacesuggestions;

import java.util.Map;

public class AuditoriumSeating {
    private final Map<String, Row> rows;

    public AuditoriumSeating(Map<String, Row> rows) {
        this.rows = rows;
    }

    public SeatingOptionSuggested suggestSeatingOptionFor(int partyRequested, PricingCategory pricingCategory) {
        for (Row row : rows.values()) {
            var seatingOptionSuggested = row.suggestSeatingOption(partyRequested, pricingCategory);

            if (seatingOptionSuggested.matchExpectation()) {
                // Cool, we mark the seat as Allocated (that we turns into a SuggestionMode)
                return seatingOptionSuggested;
            }
        }

        return new SeatingOptionNotAvailable(partyRequested, pricingCategory);
    }
}
