namespace SeatsSuggestions;

public class Row(string name, List<Seat> seats)
{
    public string Name { get; } = name;
    public List<Seat> Seats { get; } = seats;

    public SeatingOptionSuggested SuggestSeatingOption(int partyRequested, PricingCategory pricingCategory)
    { 
        var seatAllocation = new SeatingOptionSuggested(partyRequested, pricingCategory);
        
        foreach (var seat in Seats)
        {
            if (seat.IsAvailable() && seat.MatchCategory(pricingCategory))
            {
                seatAllocation.AddSeat(seat);
                if (seatAllocation.MatchExpectation()) return seatAllocation;
            }
        }
        return new SeatingOptionNotAvailable(partyRequested, pricingCategory);
    }
}