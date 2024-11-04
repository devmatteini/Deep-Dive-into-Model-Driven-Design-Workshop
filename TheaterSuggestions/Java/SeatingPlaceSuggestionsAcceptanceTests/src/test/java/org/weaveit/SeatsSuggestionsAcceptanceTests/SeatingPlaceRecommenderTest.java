package org.weaveit.SeatsSuggestionsAcceptanceTests;

import org.weaveit.ExternalDependencies.auditoriumlayoutrepository.AuditoriumLayoutRepository;
import org.weaveit.ExternalDependencies.reservationsprovider.ReservationsProvider;
import org.junit.jupiter.api.Test;

import java.io.IOException;


import static org.junit.jupiter.api.Assertions.*;
import static org.assertj.core.api.Assertions.assertThat;

public class SeatingPlaceRecommenderTest {
    /*
     *  Business Rule - Only Suggest available seats
     */

    @Test
    public void Suggest_two_seats_when_Auditorium_contains_all_available_seats() throws IOException {
        // Lincoln-17
        //
        //     1   2   3   4   5   6   7   8   9  10
        //  A: 2   2   1   1   1   1   1   1   2   2
        //  B: 2   2   1   1   1   1   1   1   2   2
        final String showId = "17";
        AuditoriumSeatings auditoriumSeatings =
                new AuditoriumSeatings(new AuditoriumLayoutRepository(), new ReservationsProvider());

        // Remove this assertion to the expected one with outcome : A1, A2
        AuditoriumSeating auditoriumSeating = auditoriumSeatings.findByShowId(showId);
       assertThat(auditoriumSeating.rows()).hasSize(2);
    }

    @Test
    public void should_suggest_one_seat_when_Auditorium_contains_one_available_seat_only() throws IOException {
        // Ford Auditorium-1
        //       1   2   3   4   5   6   7   8   9  10
        //  A : (2) (2)  1  (1) (1) (1) (1) (1) (2) (2)
        //  B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        final String showId = "1";
        AuditoriumSeatings auditoriumSeatings =
                new AuditoriumSeatings(new AuditoriumLayoutRepository(), new ReservationsProvider());

        // Remove this assertion to the expected one with outcome : A3
        AuditoriumSeating auditoriumSeating = auditoriumSeatings.findByShowId(showId);
        assertThat(auditoriumSeating.rows()).hasSize(2);
    }

    @Test
    public void should_return_SeatsNotAvailable_when_Auditorium_has_all_its_seats_already_reserved() throws IOException {
        // Madison Auditorium-5
        //      1   2   3   4   5   6   7   8   9  10
        // A : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        // B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        final String showId = "5";
        AuditoriumSeatings auditoriumSeatings =
                new AuditoriumSeatings(new AuditoriumLayoutRepository(), new ReservationsProvider());

        // Remove this assertion to the expected one with outcome : SuggestionNotAvailable
        AuditoriumSeating auditoriumSeating = auditoriumSeatings.findByShowId(showId);
        assertThat(auditoriumSeating.rows()).hasSize(2);
    }

   @Test
    public void Offer_several_suggestions_ie_1_per_PricingCategory() throws IOException {
        // New Amsterdam-18
        //     1   2   3   4   5   6   7   8   9  10
        //  A: 2   2   1   1   1   1   1   1   2   2
        //  B: 2   2   1   1   1   1   1   1   2   2
        //  C: 2   2   2   2   2   2   2   2   2   2
        //  D: 2   2   2   2   2   2   2   2   2   2
        //  E: 3   3   3   3   3   3   3   3   3   3
        //  F: 3   3   3   3   3   3   3   3   3   3
        final String showId = "18";
       AuditoriumSeatings auditoriumSeatings =
               new AuditoriumSeatings(new AuditoriumLayoutRepository(), new ReservationsProvider());

       // Remove this assertion to the expected one with outcome :
       // PricingCategory.First => "A3", "A4", "A5"
       // PricingCategory.Second => "A1", "A2", "A9"
       // PricingCategory.Third => "E1", "E2", "E3"
        AuditoriumSeating auditoriumSeating = auditoriumSeatings.findByShowId(showId);
       assertThat(auditoriumSeating.rows()).hasSize(6);
    }
}


