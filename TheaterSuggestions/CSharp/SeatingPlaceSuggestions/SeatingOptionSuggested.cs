using SeatsSuggestions;

public class SeatingOptionSuggested
{
    public SeatingOptionSuggested(int partyRequested, PricingCategory priceCategory)
    {
        PartyRequested = partyRequested;
        PricingCategory = priceCategory;
    }

    public PricingCategory PricingCategory { get; }
    public List<SeatingPlace> Seats { get; } = new();
    public int PartyRequested { get; }

    public void AddSeat(SeatingPlace seatingPlace)
    {
        Seats.Add(seatingPlace);
    }

    public bool MatchExpectation()
    {
        return Seats.Count == PartyRequested;
    }
}