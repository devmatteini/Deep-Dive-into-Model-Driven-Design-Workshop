namespace SeatsSuggestions;

public class SeatingArrangementRecommender
{
    private const int NumberOfSuggestions = 3;
    private readonly AuditoriumSeatingArrangements _auditoriumSeatingArrangements;

    public SeatingArrangementRecommender(AuditoriumSeatingArrangements auditoriumSeatingArrangements)
    {
        _auditoriumSeatingArrangements = auditoriumSeatingArrangements;
    }

    public SuggestionsAreMade MakeSuggestions(string showId, int partyRequested)
    {
        var suggestionsMade = new SuggestionsAreMade(showId, partyRequested);

        suggestionsMade.Add(GiveMeSuggestionsFor(_auditoriumSeatingArrangements.FindByShowId(showId), partyRequested, PricingCategory.First));
        suggestionsMade.Add(GiveMeSuggestionsFor(_auditoriumSeatingArrangements.FindByShowId(showId), partyRequested, PricingCategory.Second));
        suggestionsMade.Add(GiveMeSuggestionsFor(_auditoriumSeatingArrangements.FindByShowId(showId), partyRequested, PricingCategory.Third));
        suggestionsMade.Add(GiveMeSuggestionsFor(_auditoriumSeatingArrangements.FindByShowId(showId), partyRequested, PricingCategory.Ignore));

        if (suggestionsMade.MatchExpectations()) return suggestionsMade;

        return new SuggestionsAreNotAvailable(showId, partyRequested);
    }

    private static IEnumerable<SuggestionIsMade> GiveMeSuggestionsFor(AuditoriumSeatingArrangement auditoriumSeatingArrangement,
        int partyRequested,
        PricingCategory pricingCategory)
    {
        var foundedSuggestions = new List<SuggestionIsMade>();

        for (var i = 0; i < NumberOfSuggestions; i++)
        {
            var seatingOptionSuggested = auditoriumSeatingArrangement.SuggestSeatingOptionFor(partyRequested, pricingCategory);

            if (seatingOptionSuggested.MatchExpectation())
            {
                foreach (var seat in seatingOptionSuggested.Seats)
                {
                    seat.Allocate();
                }

                foundedSuggestions.Add(new SuggestionIsMade(seatingOptionSuggested));
            }
        }

        return foundedSuggestions;
    }
}