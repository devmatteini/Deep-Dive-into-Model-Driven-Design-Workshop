package com.baasie.ExternalDependencies;

import com.baasie.ExternalDependencies.auditoriumlayoutrepository.AuditoriumDto;
import com.baasie.ExternalDependencies.auditoriumlayoutrepository.AuditoriumLayoutRepository;
import com.baasie.ExternalDependencies.auditoriumlayoutrepository.SeatDto;
import com.baasie.ExternalDependencies.reservationsprovider.ReservationsProvider;
import com.baasie.ExternalDependencies.reservationsprovider.ReservedSeatsDto;
import org.junit.Test;

import java.io.IOException;

import static org.assertj.core.api.Assertions.assertThat;

public class ExternalDependenciesTest {

    @Test
    public void should_allow_us_to_retrieve_reserved_seats_for_a_given_ShowId() throws IOException {
        ReservationsProvider seatsRepository = new ReservationsProvider();
        ReservedSeatsDto reservedSeatsDto = seatsRepository.getReservedSeats("1");

        //AssertJ
        assertThat(reservedSeatsDto.getReservedSeats()).hasSize(19);
    }

    @Test
    public void should_allow_us_to_retrieve_AuditoriumLayout_for_a_given_ShowId() throws IOException {

        AuditoriumLayoutRepository eventRepository = new AuditoriumLayoutRepository();
        AuditoriumDto theaterDto = eventRepository.GetAuditoriumLayoutFor("2");

        assertThat(theaterDto.getRows()).hasSize(6);
        assertThat(theaterDto.getCorridors()).hasSize(2);
        SeatDto firstSeatOfFirstRow = theaterDto.getRows().get("A").get(0);
        assertThat(firstSeatOfFirstRow.getCategory()).isEqualTo(2);
    }
}