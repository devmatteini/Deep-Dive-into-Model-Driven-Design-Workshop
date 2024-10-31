package com.baasie.SeatsSuggestionsAcceptanceTests;

import com.baasie.ExternalDependencies.auditoriumlayoutrepository.AuditoriumLayoutRepository;
import com.baasie.ExternalDependencies.reservationsprovider.ReservationsProvider;
import com.baasie.SeatsSuggestions.*;
import org.junit.Test;

import java.io.IOException;

import static org.assertj.core.api.AssertionsForInterfaceTypes.assertThat;

public class SeatsAllocatorTest {
    /*
     *  Business Rule - Only Suggest available seats
     */

    @Test
    public void Suggest_two_seats_when_Auditorium_contains_all_available_seats() throws IOException {
        // New Amsterdam-8
        //
        //     1   2   3   4   5   6   7   8   9  10
        //  A: 2   2   1   1   1   1   1   1   2   2
        //  B: 2   2   1   1   1   1   1   1   2   2
        final String showId = "8";
        final int partyRequested = 2;
        final int numberOfSuggestions = 1;

        AuditoriumSeatingAdapter auditoriumLayoutAdapter =
                new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());

        SeatsAllocator seatsAllocator = new SeatsAllocator(auditoriumLayoutAdapter);

        SuggestionsMade suggestionsMade = seatsAllocator.makeSuggestion(showId, partyRequested, numberOfSuggestions);

        assertThat(suggestionsMade.partyRequested()).isEqualTo(partyRequested);
        assertThat(suggestionsMade.showId()).isEqualTo(showId);
        assertThat(suggestionsMade.seatNames(PricingCategory.Second)).contains("A1", "A2");
    }
    @Test
    public void should_suggest_one_seat_when_Auditorium_contains_one_available_seat_only() throws IOException {
        // Ford Auditorium-1
        //       1   2   3   4   5   6   7   8   9  10
        //  A : (2) (2)  1  (1) (1) (1) (1) (1) (2) (2)
        //  B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        final String showId = "1";
        final int partyRequested = 1;

        AuditoriumSeatingAdapter auditoriumLayoutAdapter =
                new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());

        SeatsAllocator seatsAllocator = new SeatsAllocator(auditoriumLayoutAdapter);

        SuggestionsMade suggestionsMade = seatsAllocator.makeSuggestion(showId, partyRequested);

        assertThat(suggestionsMade.partyRequested()).isEqualTo(partyRequested);
        assertThat(suggestionsMade.showId()).isEqualTo(showId);
        assertThat(suggestionsMade.seatNames(PricingCategory.First)).containsExactly("A3");
    }

    @Test
    public void should_return_SeatsNotAvailable_when_Auditorium_has_all_its_seats_already_reserved() throws IOException {
        // Madison Auditorium-5
        //      1   2   3   4   5   6   7   8   9  10
        // A : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        // B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
        final String showId = "5";
        final int partyRequested = 1;

        AuditoriumSeatingAdapter auditoriumLayoutAdapter =
                new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());

        SeatsAllocator seatsAllocator = new SeatsAllocator(auditoriumLayoutAdapter);

        SuggestionsMade suggestionsMade = seatsAllocator.makeSuggestion(showId, partyRequested);
        assertThat(suggestionsMade.partyRequested()).isEqualTo(partyRequested);
        assertThat(suggestionsMade.showId()).isEqualTo(showId);
        assertThat(suggestionsMade).isInstanceOf(SuggestionNotAvailable.class);
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
       final int partyRequested = 1;


       AuditoriumSeatingAdapter auditoriumLayoutAdapter =
               new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());

       SeatsAllocator seatsAllocator = new SeatsAllocator(auditoriumLayoutAdapter);

       SuggestionsMade suggestionsMade = seatsAllocator.makeSuggestion(showId, partyRequested);

       assertThat(suggestionsMade.seatNames(PricingCategory.First)).containsExactly("A3", "A4", "A5");
       assertThat(suggestionsMade.seatNames(PricingCategory.Second)).containsExactly("A1", "A2", "A9");
       assertThat(suggestionsMade.seatNames(PricingCategory.Third)).containsExactly("E1", "E2", "E3");
    }
}


