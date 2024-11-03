package org.weaveit.seatingplacesuggestions;

import java.util.ArrayList;
import java.util.List;

public class SeatingOptionSuggested {

    private PricingCategory pricingCategory;
    private List<SeatingPlace> seats = new ArrayList<>();
    private int partyRequested;

    public SeatingOptionSuggested(int partyRequested, PricingCategory pricingCategory) {
        this.pricingCategory = pricingCategory;
        this.partyRequested = partyRequested;
    }

    public void addSeat(SeatingPlace seat) {
        seats.add(seat);
    }

    public boolean matchExpectation() {
        return seats.size() == partyRequested;
    }

    public List<SeatingPlace> seats() {
        return seats;
    }

    public PricingCategory pricingCategory() { return pricingCategory; }

    public int partyRequested() { return partyRequested; }
}
