namespace SeatsSuggestions.Tests;

public class SeatingPlace
{
    public SeatingPlace(string rowName, uint number, PricingCategory pricingCategory, SeatingPlaceAvailability seatingPlaceAvailability)
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

    public bool IsAvailable => SeatingPlaceAvailability == SeatingPlaceAvailability.Available;

    public override string ToString()
    {
        return $"{RowName}{Number}";
    }

    public void UpdateCategory(SeatingPlaceAvailability seatingPlaceAvailability)
    {
        SeatingPlaceAvailability = seatingPlaceAvailability;
    }
}