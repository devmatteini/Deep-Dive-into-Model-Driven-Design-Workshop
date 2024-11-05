namespace SeatsSuggestions;

public class SeatingArrangementRecommender
{
    private const int NumberOfSuggestions = 3;
    private readonly AuditoriumSeatingArrangements _auditoriumSeatingArrangements;

    public SeatingArrangementRecommender(AuditoriumSeatingArrangements auditoriumSeatingArrangements)
    {
        _auditoriumSeatingArrangements = auditoriumSeatingArrangements;
    }

    public SuggestionsMade MakeSuggestions(string showId, int partyRequested)
    {
        var auditoriumSeating = _auditoriumSeatingArrangements.FindByShowId(showId);

        var suggestionsMade = new SuggestionsMade(showId, partyRequested);

        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeating, partyRequested, PricingCategory.First));
        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeating, partyRequested, PricingCategory.Second));
        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeating, partyRequested, PricingCategory.Third));

        if (suggestionsMade.MatchExpectations()) return suggestionsMade;

        return new SuggestionNotAvailable(showId, partyRequested);
    }

    private static IEnumerable<SuggestionMade> GiveMeSuggestionsFor(AuditoriumSeatingArrangement auditoriumSeatingArrangement,
        int partyRequested,
        PricingCategory pricingCategory)
    {
        var foundedSuggestions = new List<SuggestionMade>();

        for (var i = 0; i < NumberOfSuggestions; i++)
        {
            var seatingOptionSuggested = auditoriumSeatingArrangement.SuggestSeatingOptionFor(partyRequested, pricingCategory);

            if (seatingOptionSuggested.MatchExpectation())
            {
                foreach (var seat in seatingOptionSuggested.Seats)
                {
                    seat.Allocate();
                }

                foundedSuggestions.Add(new SuggestionMade(seatingOptionSuggested));
            }
        }

        return foundedSuggestions;
    }
}