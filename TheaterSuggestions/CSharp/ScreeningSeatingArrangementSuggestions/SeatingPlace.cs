namespace SeatsSuggestions;

public class SeatingPlace
{
    public SeatingPlace(string rowName, uint number, PricingCategory pricingCategory,
        SeatingPlaceAvailability seatingPlaceAvailability)
    {
        RowName = rowName;
        Number = number;
        PricingCategory = pricingCategory;
        SeatingPlaceAvailability = seatingPlaceAvailability;
    }

    public string RowName { get; }
    public uint Number { get; }
    public PricingCategory PricingCategory { get; }
    private SeatingPlaceAvailability SeatingPlaceAvailability { get; set; }

    public bool IsAvailable()
    {
        return SeatingPlaceAvailability == SeatingPlaceAvailability.Available;
    }

    public override string ToString()
    {
        return $"{RowName}{Number}";
    }

    public bool MatchCategory(PricingCategory pricingCategory)
    {
        if (pricingCategory == PricingCategory.Ignore) return true;
        return PricingCategory == pricingCategory;
    }

    public void Allocate()
    {
        if (SeatingPlaceAvailability == SeatingPlaceAvailability.Available) SeatingPlaceAvailability = SeatingPlaceAvailability.Allocated;
    }
}