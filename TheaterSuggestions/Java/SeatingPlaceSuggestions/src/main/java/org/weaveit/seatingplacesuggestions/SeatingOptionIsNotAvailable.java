package org.weaveit.seatingplacesuggestions;

public class SeatingOptionIsNotAvailable extends SeatingOptionIsSuggested {
    public SeatingOptionIsNotAvailable(int partyRequested, PricingCategory pricingCategory) {
        super(partyRequested, pricingCategory);
    }
}
