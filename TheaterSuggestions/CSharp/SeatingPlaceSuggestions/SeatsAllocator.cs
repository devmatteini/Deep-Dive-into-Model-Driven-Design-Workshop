namespace SeatsSuggestions;

public class SeatingPlaceRecommender
{
    private const int NumberOfSuggestions = 3;
    private readonly AuditoriumSeatingAdapter _auditoriumSeatingAdapter;

    public SeatingPlaceRecommender(AuditoriumSeatingAdapter auditoriumSeatingAdapter)
    {
        _auditoriumSeatingAdapter = auditoriumSeatingAdapter;
    }

    public SuggestionsMade MakeSuggestions(string showId, int partyRequested, int numberOfSuggestions = NumberOfSuggestions)
    {
        var auditoriumSeating = _auditoriumSeatingAdapter.GetAuditoriumSeating(showId);

        var suggestionsMade = new SuggestionsMade(showId, partyRequested);

        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeating, partyRequested, PricingCategory.First, numberOfSuggestions));
        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeating, partyRequested, PricingCategory.Second, numberOfSuggestions));
        suggestionsMade.Add(GiveMeSuggestionsFor(auditoriumSeating, partyRequested, PricingCategory.Third, numberOfSuggestions));

        if (suggestionsMade.MatchExpectations()) return suggestionsMade;

        return new SuggestionNotAvailable(showId, partyRequested);
    }

    private static IEnumerable<SuggestionMade> GiveMeSuggestionsFor(AuditoriumSeating auditoriumSeating,
        int partyRequested,
        PricingCategory pricingCategory, int numberOfSuggestions)
    {
        var foundedSuggestions = new List<SuggestionMade>();

        for (var i = 0; i < numberOfSuggestions; i++)
        {
            var seatingOptionSuggested = auditoriumSeating.SuggestSeatingOptionFor(partyRequested, pricingCategory);

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