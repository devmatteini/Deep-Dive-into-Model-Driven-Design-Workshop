package org.weaveit.seatingplacesuggestions;

public class SeatingPlace {

    private String rowName;
    private int number;
    private PricingCategory pricingCategory;
    private SeatingPlaceAvailability seatingPlaceAvailability;

    public SeatingPlace(String rowName, int number, PricingCategory pricingCategory, SeatingPlaceAvailability seatingPlaceAvailability) {
        this.rowName = rowName;
        this.number = number;
        this.pricingCategory = pricingCategory;
        this.seatingPlaceAvailability = seatingPlaceAvailability;
    }
    public boolean isAvailable() {
        return seatingPlaceAvailability == SeatingPlaceAvailability.AVAILABLE;
    }

    public boolean matchCategory(PricingCategory pricingCategory) {
        return this.pricingCategory == pricingCategory;
    }

    public void allocate() {
        if (seatingPlaceAvailability == SeatingPlaceAvailability.AVAILABLE)
            seatingPlaceAvailability = SeatingPlaceAvailability.ALLOCATED;
    }

    @Override
    public String toString() {
        return rowName + number;
    }
}
