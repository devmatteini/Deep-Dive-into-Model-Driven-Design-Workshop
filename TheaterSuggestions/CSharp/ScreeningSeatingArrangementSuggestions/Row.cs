﻿using Value;

namespace SeatsSuggestions;

public class Row(string name, List<SeatingPlace> seats) : Value.ValueType<Row>
{
    public string Name { get; } = name;
    public List<SeatingPlace> SeatingPlaces { get; } = seats;

    public SeatingOptionIsSuggested SuggestSeatingOption(int partyRequested, PricingCategory pricingCategory)
    {
        var seatAllocation = new SeatingOptionIsSuggested(partyRequested, pricingCategory);

        foreach (var seat in SeatingPlaces)
        {
            if (seat.IsAvailable() && seat.MatchCategory(pricingCategory))
            {
                seatAllocation.AddSeat(seat);
                if (seatAllocation.MatchExpectation()) return seatAllocation;
            }
        }

        return new SeatingOptionIsNotAvailable(partyRequested, pricingCategory);
    }

    public Row AddSeat(SeatingPlace seatingPlace)
    {
        return new(Name, new List<SeatingPlace>(SeatingPlaces) { seatingPlace });
    }

    public Row Allocate(SeatingPlace seatingPlace)
    {
        var otherSeatingPlaces = new List<SeatingPlace>(SeatingPlaces).Where(x => x != seatingPlace);
        var allSeatingPlaces = new List<SeatingPlace>(otherSeatingPlaces) { seatingPlace.Allocate() };
        return new(Name, allSeatingPlaces);
    }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return new object[] { Name, new ListByValue<SeatingPlace>(SeatingPlaces) };
    }
}