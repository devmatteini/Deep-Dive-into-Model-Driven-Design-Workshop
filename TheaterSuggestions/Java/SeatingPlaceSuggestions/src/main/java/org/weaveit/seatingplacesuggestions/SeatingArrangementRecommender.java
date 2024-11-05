package org.weaveit.seatingplacesuggestions;

import java.util.ArrayList;
import java.util.List;

public class SeatingArrangementRecommender {
    private static final int NUMBER_OF_SUGGESTIONS = 3;
    private final AuditoriumSeatingArrangements auditoriumSeatingArrangements;

    public SeatingArrangementRecommender(AuditoriumSeatingArrangements auditoriumSeatingArrangements) {
        this.auditoriumSeatingArrangements = auditoriumSeatingArrangements;
    }

    public SuggestionsMade makeSuggestion(String showId, int partyRequested) {
        var auditoriumSeating = auditoriumSeatingArrangements.findByShowId(showId);

        var suggestionsMade = new SuggestionsMade(showId, partyRequested);

        suggestionsMade.add(giveMeSuggestionsFor(auditoriumSeating, partyRequested,
                PricingCategory.FIRST));
        suggestionsMade.add(giveMeSuggestionsFor(auditoriumSeating, partyRequested,
                PricingCategory.SECOND));
        suggestionsMade.add(giveMeSuggestionsFor(auditoriumSeating, partyRequested,
                PricingCategory.THIRD));

        if (suggestionsMade.matchExpectations())
            return suggestionsMade;

        return new SuggestionNotAvailable(showId, partyRequested);
    }

    private static List<SuggestionMade> giveMeSuggestionsFor(
            AuditoriumSeatingArrangement auditoriumSeatingArrangement, int partyRequested, PricingCategory pricingCategory) {
        var foundedSuggestions = new ArrayList<SuggestionMade>();

        for (int i = 0; i < NUMBER_OF_SUGGESTIONS; i++) {
            var seatingOptionSuggested = auditoriumSeatingArrangement.suggestSeatingOptionFor(partyRequested, pricingCategory);

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
