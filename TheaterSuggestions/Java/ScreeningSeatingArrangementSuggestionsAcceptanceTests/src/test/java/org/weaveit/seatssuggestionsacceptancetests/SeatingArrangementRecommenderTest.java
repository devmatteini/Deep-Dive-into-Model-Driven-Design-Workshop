package org.weaveit.seatssuggestionsacceptancetests;

import org.weaveit.externaldependencies.auditoriumlayoutrepository.AuditoriumLayoutRepository;
import org.weaveit.externaldependencies.reservationsprovider.ReservationsProvider;
import org.junit.jupiter.api.Test;

import java.io.IOException;


import static org.assertj.core.api.Assertions.assertThat;

class SeatingArrangementRecommenderTest {/*
     *  Business Rule - Only Suggest available seats
     */

    @Test
    void suggest_one_seatingPlace_when_Auditorium_contains_one_available_seatingPlace() throws IOException {
        // Ford Auditorium-1
        //       1   2   3   4   5   6   7   8   9  10
        //  A : (2) (2)  1  (1) (1) (1) (1) (1) (2) (2)
        //  B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        final String showId = "1";
        var auditoriumSeatingArrangements =
                new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());

        // Remove this assertion to the expected one with outcome : A3
        var auditoriumSeatingArrangement = auditoriumSeatingArrangements.findByShowId(showId);
        assertThat(auditoriumSeatingArrangement.rows()).hasSize(2);
    }

    @Test
    void return_SuggestionNotAvailable_when_Auditorium_has_all_its_seatingPlaces_reserved() throws IOException {
        // Madison Auditorium-5
        //      1   2   3   4   5   6   7   8   9  10
        // A : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        // B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        final String showId = "5";
        var auditoriumSeatingArrangements =
                new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());

        // Remove this assertion to the expected one with outcome : SuggestionNotAvailable
        var auditoriumSeatingArrangement = auditoriumSeatingArrangements.findByShowId(showId);
        assertThat(auditoriumSeatingArrangement.rows()).hasSize(2);
    }

    @Test
    void suggest_two_seatingPlaces_when_Auditorium_contains_all_available_seatingPlaces() throws IOException {
        // Lincoln-17
        //
        //     1   2   3   4   5   6   7   8   9  10
        //  A: 2   2   1   1   1   1   1   1   2   2
        //  B: 2   2   1   1   1   1   1   1   2   2
        final String showId = "17";
        var auditoriumSeatingArrangements =
                new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());

        // Remove this assertion to the expected one with outcome : A1, A2
        var auditoriumSeatingArrangement = auditoriumSeatingArrangements.findByShowId(showId);
        assertThat(auditoriumSeatingArrangement.rows()).hasSize(2);
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
       var auditoriumSeatingArrangements =
               new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());

       // Remove this assertion to the expected one with outcome :
       // PricingCategory.First => "A3", "A4", "A5"
       // PricingCategory.Second => "A1", "A2", "A9"
       // PricingCategory.Third => "E1", "E2", "E3"
       var auditoriumSeatingArrangement = auditoriumSeatingArrangements.findByShowId(showId);
       assertThat(auditoriumSeatingArrangement.rows()).hasSize(6);
    }
}


