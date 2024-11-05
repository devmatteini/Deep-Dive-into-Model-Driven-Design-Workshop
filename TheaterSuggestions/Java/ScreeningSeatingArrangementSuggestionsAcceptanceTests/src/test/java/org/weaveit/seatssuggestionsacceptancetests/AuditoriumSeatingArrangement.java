package org.weaveit.seatssuggestionsacceptancetests;

import java.util.Map;

public class AuditoriumSeatingArrangement {

    private final Map<String, Row> rows;

    public AuditoriumSeatingArrangement(Map<String, Row> rows) {
        this.rows = rows;
    }

    public Map<String, Row> rows() {
        return rows;
    }
}
