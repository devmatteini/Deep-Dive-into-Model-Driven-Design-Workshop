package org.weaveit.externaldependencies;

import org.weaveit.externaldependencies.auditoriumlayoutrepository.AuditoriumDto;
import org.weaveit.externaldependencies.auditoriumlayoutrepository.AuditoriumLayoutRepository;
import org.weaveit.externaldependencies.auditoriumlayoutrepository.SeatDto;
import org.weaveit.externaldependencies.reservationsprovider.ReservationsProvider;
import org.weaveit.externaldependencies.reservationsprovider.ReservedSeatsDto;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

import java.io.IOException;

class ExternalDependenciesTest {

    @Test
    void should_allow_us_to_retrieve_reserved_seats_for_a_given_ShowId() throws IOException {
        ReservationsProvider seatsRepository = new ReservationsProvider();
        ReservedSeatsDto reservedSeatsDto = seatsRepository.getReservedSeats("1");
        Assertions.assertEquals(19, reservedSeatsDto.reservedSeats().size(), "Expected 19 reserved seats");
    }

    @Test
    void should_allow_us_to_retrieve_AuditoriumLayout_for_a_given_ShowId() throws IOException {

        AuditoriumLayoutRepository eventRepository = new AuditoriumLayoutRepository();
        AuditoriumDto theaterDto = eventRepository.getAuditoriumLayoutFor("2");

        // JUnit 5 Assertions
        Assertions.assertEquals(6, theaterDto.rows().size(), "Expected 6 rows in the auditorium");
        Assertions.assertEquals(2, theaterDto.corridors().size(), "Expected 2 corridors in the auditorium");
        SeatDto firstSeatOfFirstRow = theaterDto.rows().get("A").get(0);
        System.out.println(firstSeatOfFirstRow);
        Assertions.assertEquals(2, firstSeatOfFirstRow.category(), "Expected category 2 for the first seat of the first row");
    }
}
