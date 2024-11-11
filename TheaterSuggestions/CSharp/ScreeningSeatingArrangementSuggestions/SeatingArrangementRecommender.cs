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
        var auditoriumSeatingArrangement = _auditoriumSeatingArrangements.FindByShowId(showId);

        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeatingArrangement, partyRequested, PricingCategory.First));
        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeatingArrangement, partyRequested, PricingCategory.Second));
        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeatingArrangement, partyRequested, PricingCategory.Third));
        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeatingArrangement, partyRequested, PricingCategory.Ignore));

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
                auditoriumSeatingArrangement = auditoriumSeatingArrangement.Allocate(seatingOptionSuggested.Seats);
                foundedSuggestions.Add(new SuggestionIsMade(seatingOptionSuggested));
            }
        }

        return foundedSuggestions;
    }
}