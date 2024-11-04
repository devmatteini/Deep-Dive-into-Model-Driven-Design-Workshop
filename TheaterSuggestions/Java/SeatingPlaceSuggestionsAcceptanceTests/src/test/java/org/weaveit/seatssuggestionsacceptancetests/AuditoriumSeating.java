package org.weaveit.seatssuggestionsacceptancetests;

import java.util.Map;

public class AuditoriumSeating {

    private final Map<String, Row> rows;

    public AuditoriumSeating(Map<String, Row> rows) {
        this.rows = rows;
    }

    public Map<String, Row> rows() {
        return rows;
    }
}
