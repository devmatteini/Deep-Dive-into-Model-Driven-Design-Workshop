package org.weaveit.seatingplacesuggestions;

import java.util.ArrayList;
import java.util.List;

public class SeatingPlaceRecommender {
    private static final int NUMBER_OF_SUGGESTIONS = 3;
    private final AuditoriumSeatings auditoriumSeatings;

    public SeatingPlaceRecommender(AuditoriumSeatings auditoriumSeatings) {
        this.auditoriumSeatings = auditoriumSeatings;
    }

    public SuggestionsMade makeSuggestion(String showId, int partyRequested) {
        var auditoriumSeating = auditoriumSeatings.findByShowId(showId);

        var suggestionsMade = new SuggestionsMade(showId, partyRequested);

        suggestionsMade.add(giveMeSuggestionsFor(auditoriumSeating, partyRequested,
                PricingCategory.FIRST));
        suggestionsMade.add(giveMeSuggestionsFor(auditoriumSeating,partyRequested,
                PricingCategory.SECOND));
        suggestionsMade.add(giveMeSuggestionsFor(auditoriumSeating,partyRequested,
                PricingCategory.THIRD));

        if (suggestionsMade.matchExpectations())
            return suggestionsMade;

        return new SuggestionNotAvailable(showId, partyRequested);
    }

    private static List<SuggestionMade> giveMeSuggestionsFor(
            AuditoriumSeating auditoriumSeating, int partyRequested,  PricingCategory pricingCategory) {
        var foundedSuggestions = new ArrayList<SuggestionMade>();

        for (int i = 0; i < NUMBER_OF_SUGGESTIONS; i++) {
            var seatingOptionSuggested = auditoriumSeating.suggestSeatingOptionFor(partyRequested, pricingCategory);

            if (seatingOptionSuggested.matchExpectation()) {
                for (var seatingPlace : seatingOptionSuggested.seats()) {
                    seatingPlace.allocate();
                }

                foundedSuggestions.add(new SuggestionMade(seatingOptionSuggested));
            }
        }

        return foundedSuggestions;
    }
}
