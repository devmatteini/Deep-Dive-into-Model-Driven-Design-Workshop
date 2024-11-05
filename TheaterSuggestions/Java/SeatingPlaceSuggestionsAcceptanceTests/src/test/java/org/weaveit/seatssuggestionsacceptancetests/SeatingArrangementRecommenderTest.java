package org.weaveit.seatssuggestionsacceptancetests;

import org.weaveit.externaldependencies.auditoriumlayoutrepository.AuditoriumLayoutRepository;
import org.weaveit.externaldependencies.reservationsprovider.ReservationsProvider;
import org.weaveit.seatingplacesuggestions.*;
import org.junit.jupiter.api.Test;

import java.io.IOException;

import static org.junit.jupiter.api.Assertions.*;
import static org.assertj.core.api.Assertions.assertThat;

class SeatingArrangementRecommenderTest {
    /*
     *  Business Rule - Only Suggest available seats
     */

    @Test
    void suggest_one_seatingPlace_when_Auditorium_contains_one_available_seatingPlace() throws IOException {
        // Ford Auditorium-1
        //       1   2   3   4   5   6   7   8   9  10
        //  A : (2) (2)  1  (1) (1) (1) (1) (1) (2) (2)
        //  B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        final String showId = "1";
        final int partyRequested = 1;

        var auditoriumSeatings =
                new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());
        var seatingPlaceRecommender = new SeatingArrangementRecommender(auditoriumSeatings);
        var suggestionsMade = seatingPlaceRecommender.makeSuggestion(showId, partyRequested);

        assertThat(suggestionsMade.seatNames(PricingCategory.FIRST)).containsExactly("A3");
    }

    @Test
    void return_SuggestionNotAvailable_when_Auditorium_has_all_its_seatingPlaces_reserved() throws IOException {
        // Madison Auditorium-5
        //      1   2   3   4   5   6   7   8   9  10
        // A : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        // B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        final String showId = "5";
        final int partyRequested = 1;

        var auditoriumSeatings =
                new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());
        var seatingPlaceRecommender = new SeatingArrangementRecommender(auditoriumSeatings);
        var suggestionsMade = seatingPlaceRecommender.makeSuggestion(showId, partyRequested);

        assertEquals(partyRequested, suggestionsMade.partyRequested(), "Party requested should match");
        assertEquals(showId, suggestionsMade.showId(), "Show ID should match");
        assertInstanceOf(SuggestionsAreAreNotAvailable.class, suggestionsMade, "Suggestions made should be an instance of SuggestionNotAvailable");
    }

    @Test
    void suggest_two_seatingPlaces_when_Auditorium_contains_all_available_seatingPlaces() throws IOException {
        // Lincoln-17
        //
        //     1   2   3   4   5   6   7   8   9  10
        //  A: 2   2   1   1   1   1   1   1   2   2
        //  B: 2   2   1   1   1   1   1   1   2   2
        final String showId = "17";
        final int partyRequested = 2;

        var auditoriumSeatings =
                new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());
        var seatingPlaceRecommender = new SeatingArrangementRecommender(auditoriumSeatings);
        var suggestionsMade = seatingPlaceRecommender.makeSuggestion(showId, partyRequested);

        assertThat(suggestionsMade.seatNames(PricingCategory.SECOND)).containsExactly("A1", "A2", "A9", "A10", "B1", "B2");
    }

   @Test
   void suggest_three_availabilities_per_PricingCategory() throws IOException {
        // New Amsterdam-18
        //     1   2   3   4   5   6   7   8   9  10
        //  A: 2   2   1   1   1   1   1   1   2   2
        //  B: 2   2   1   1   1   1   1   1   2   2
        //  C: 2   2   2   2   2   2   2   2   2   2
        //  D: 2   2   2   2   2   2   2   2   2   2
        //  E: 3   3   3   3   3   3   3   3   3   3
        //  F: 3   3   3   3   3   3   3   3   3   3
       final String showId = "18";
       final int partyRequested = 1;

       var auditoriumSeatings =
               new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());
       var seatingPlaceRecommender = new SeatingArrangementRecommender(auditoriumSeatings);
       var suggestionsMade = seatingPlaceRecommender.makeSuggestion(showId, partyRequested);

       assertThat(suggestionsMade.seatNames(PricingCategory.FIRST)).containsExactly("A3","A4","A5");
       assertThat(suggestionsMade.seatNames(PricingCategory.SECOND)).containsExactly("A1", "A2", "A9");
       assertThat(suggestionsMade.seatNames(PricingCategory.THIRD)).containsExactly("E1", "E2", "E3");
    }
}


