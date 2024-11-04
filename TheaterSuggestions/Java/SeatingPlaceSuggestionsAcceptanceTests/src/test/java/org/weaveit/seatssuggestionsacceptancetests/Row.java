package org.weaveit.seatssuggestionsacceptancetests;

import java.util.ArrayList;
import java.util.List;

public class Row {
    private String name;
    private List<SeatingPlace> seatingPlace = new ArrayList<>();

    public Row(String name, List<SeatingPlace> seatingPlace) {
        this.name = name;
        this.seatingPlace = seatingPlace;
    }

    public List<SeatingPlace> seatingPlaces() {
        return seatingPlace;
    }
}
