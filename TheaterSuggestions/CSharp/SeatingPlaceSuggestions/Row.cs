namespace SeatsSuggestions;

public class Row(string name, List<SeatingPlace> seats)
{
    public string Name { get; } = name;
    public List<SeatingPlace> SeatingPlaces { get; } = seats;

    public SeatingOptionSuggested SuggestSeatingOption(int partyRequested, PricingCategory pricingCategory)
    { 
        var seatAllocation = new SeatingOptionSuggested(partyRequested, pricingCategory);
        
        foreach (var seat in SeatingPlaces)
        {
            if (seat.IsAvailable() && seat.MatchCategory(pricingCategory))
            {
                seatAllocation.AddSeat(seat);
                if (seatAllocation.MatchExpectation()) return seatAllocation;
            }
        }
        return new SeatingOptionNotAvailable(partyRequested, pricingCategory);
    }

    public void AddSeat(SeatingPlace seatingPlace)
    {
        SeatingPlaces.Add(seatingPlace);
    }
}