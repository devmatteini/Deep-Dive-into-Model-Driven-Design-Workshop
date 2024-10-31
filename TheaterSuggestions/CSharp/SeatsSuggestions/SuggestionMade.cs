namespace SeatsSuggestions;

/// <summary>
///     Occurs when a Suggestion is made.
/// </summary>
public class SuggestionMade(SeatingOptionSuggested seatingOptionSuggested)
{
    private readonly List<Seat> _suggestedSeats = seatingOptionSuggested.Seats;

    public int PartyRequested { get; } = seatingOptionSuggested.PartyRequested;
    public PricingCategory PricingCategory { get; } = seatingOptionSuggested.PricingCategory;

    public IReadOnlyList<Seat> SuggestedSeats => _suggestedSeats;

    public IEnumerable<string> SeatNames()
    {
        return _suggestedSeats.Select(s => s.ToString());
    }

    public bool MatchExpectation()
    {
        return _suggestedSeats.Count == PartyRequested;
    }
}