namespace SeatsSuggestions;

internal class SeatingOptionIsNotAvailable : SeatingOptionIsSuggested
{
    public SeatingOptionIsNotAvailable(int partyRequested, PricingCategory pricingCategory) : base(partyRequested,
        pricingCategory)
    {
    }
}