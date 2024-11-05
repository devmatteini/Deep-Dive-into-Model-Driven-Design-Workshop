
using System.Collections.Generic;

namespace SeatsSuggestions.Tests;

public class AuditoriumSeatingArrangement
{
    public readonly Dictionary<string, Row> Rows;

    public AuditoriumSeatingArrangement(Dictionary<string, Row> rows)
    {
        Rows = rows;
    }
}