namespace SeatsSuggestions;

public class SeatingPlace : Value.ValueType<SeatingPlace>
{
    public SeatingPlace(string rowName, int number, PricingCategory pricingCategory,
        SeatingPlaceAvailability seatingPlaceAvailability)
    {
        RowName = rowName;
        Number = number;
        PricingCategory = pricingCategory;
        SeatingPlaceAvailability = seatingPlaceAvailability;
    }

    public string RowName { get; }
    public int Number { get; }
    public PricingCategory PricingCategory { get; }
    public SeatingPlaceAvailability SeatingPlaceAvailability { get; }

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

    public SeatingPlace Allocate()
    {
        if (SeatingPlaceAvailability == SeatingPlaceAvailability.Available)
            return new(RowName, Number, PricingCategory, SeatingPlaceAvailability.Allocated);
        return this;
    }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return new object[] { RowName, Number, PricingCategory, SeatingPlaceAvailability };
    }
}