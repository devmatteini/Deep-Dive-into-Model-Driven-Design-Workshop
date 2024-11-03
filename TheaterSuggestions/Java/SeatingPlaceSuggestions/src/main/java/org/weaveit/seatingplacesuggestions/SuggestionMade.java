package org.weaveit.seatingplacesuggestions;

import java.util.List;

public class SuggestionMade {

    private final List<SeatingPlace> suggestedSeats;
    private final int partyRequested;
    private final PricingCategory pricingCategory;

    public SuggestionMade(SeatingOptionSuggested seatingOptionSuggested) {
        this.suggestedSeats = seatingOptionSuggested.seats();
        this.partyRequested = seatingOptionSuggested.partyRequested();
        this.pricingCategory = seatingOptionSuggested.pricingCategory();
    }

    public List<String> seatNames() {
        return suggestedSeats.stream().map(SeatingPlace::toString).toList();
    }

    public boolean matchExpectation() {
        return suggestedSeats.size() == partyRequested;
    }

    public PricingCategory pricingCategory() {
        return pricingCategory;
    }
}
