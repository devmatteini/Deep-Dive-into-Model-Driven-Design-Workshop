package org.weaveit.seatingplacesuggestions;

import java.util.List;
import java.util.stream.Collectors;

public class SuggestionMade {

    private final List<SeatingPlace> suggestedSeats;
    private int partyRequested;
    private PricingCategory pricingCategory;

    public SuggestionMade(SeatingOptionSuggested seatingOptionSuggested) {
        this.suggestedSeats = seatingOptionSuggested.seats();
        this.partyRequested = seatingOptionSuggested.partyRequested();
        this.pricingCategory = seatingOptionSuggested.pricingCategory();
    }

    public List<String> seatNames() {
        return suggestedSeats.stream().map(SeatingPlace::toString).collect(Collectors.toList());
    }

    public boolean MatchExpectation() {
        return suggestedSeats.size() == partyRequested;
    }

    public PricingCategory pricingCategory() {
        return pricingCategory;
    }
}
