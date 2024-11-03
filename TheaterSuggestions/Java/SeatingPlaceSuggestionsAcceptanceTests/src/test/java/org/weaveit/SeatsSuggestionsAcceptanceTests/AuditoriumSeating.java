package org.weaveit.SeatsSuggestionsAcceptanceTests;

import java.util.HashMap;
import java.util.Map;

public class AuditoriumSeating {

    private Map<String, Row> rows;

    public AuditoriumSeating(Map<String, Row> rows) {
        this.rows = rows;
    }

    public Map<String, Row> rows() {
        return rows;
    }
}
