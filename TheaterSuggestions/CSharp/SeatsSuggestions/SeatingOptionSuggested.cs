using SeatsSuggestions;

public class SeatingOptionSuggested
{
    public SeatingOptionSuggested(int partyRequested, PricingCategory priceCategory)
    {
        PartyRequested = partyRequested;
        PricingCategory = priceCategory;
    }

    public PricingCategory PricingCategory { get; }
    public List<Seat> Seats { get; } = new();
    public int PartyRequested { get; }

    public void AddSeat(Seat seat)
    {
        Seats.Add(seat);
    }

    public bool MatchExpectation()
    {
        return Seats.Count == PartyRequested;
    }
}