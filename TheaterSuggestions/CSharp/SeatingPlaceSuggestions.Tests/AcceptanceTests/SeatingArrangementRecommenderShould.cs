using ExternalDependencies.AuditoriumLayoutRepository;
using ExternalDependencies.ReservationsProvider;
using NFluent;
using NUnit.Framework;

namespace SeatsSuggestions.Tests.AcceptanceTests
{
    [TestFixture]
    public class SeatingArrangementRecommenderShould
    {
        
        [Test]
        public void Suggest_one_seatingPlace_when_Auditorium_contains_one_available_seatingPlace()
        {
            // Ford Auditorium-1
            //
            //       1   2   3   4   5   6   7   8   9  10
            //  A : (2) (2)  1  (1) (1) (1) (1) (1) (2) (2)
            //  B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
            const string showId = "1";
            const int partyRequested = 1;

            var auditoriumLayoutAdapter =
                new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());

            var seatingPlaceRecommender = new SeatingArrangementRecommender(auditoriumLayoutAdapter);

            var suggestionsMade = seatingPlaceRecommender.MakeSuggestions(showId, partyRequested);

            Check.That(suggestionsMade.SeatNames(PricingCategory.First)).ContainsExactly("A3");
        }
        
        [Test]
        public void Return_SuggestionNotAvailable_when_Auditorium_has_all_its_seatingPlaces_reserved()
        {
            // Madison Auditorium-5
            //
            //      1   2   3   4   5   6   7   8   9  10
            // A : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
            // B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
            const string showId = "5";
            const int partyRequested = 1;

            var auditoriumLayoutAdapter =
                new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());

            var seatingPlaceRecommender = new SeatingArrangementRecommender(auditoriumLayoutAdapter);

            var suggestionsMade = seatingPlaceRecommender.MakeSuggestions(showId, partyRequested);
            Check.That(suggestionsMade.PartyRequested).IsEqualTo(partyRequested);
            Check.That(suggestionsMade.ShowId).IsEqualTo(showId);

            Check.That(suggestionsMade).IsInstanceOf<SuggestionsAreNotAvailable>();
        }
        
        [Test]
        public void Suggest_two_seatingPlaces_when_Auditorium_contains_all_available_seatingPlaces()
        {
            // Lincoln Auditorium-17
            //     1   2   3   4   5   6   7   8   9  10
            //  A: 2   2   1   1   1   1   1   1   2   2
            //  B: 2   2   1   1   1   1   1   1   2   2
            const string showId = "17";
            const int partyRequested = 2;
            
            var auditoriumSeatingAdapter = new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());
            var seatingPlaceRecommender = new SeatingArrangementRecommender(auditoriumSeatingAdapter);
            var suggestionsMade = seatingPlaceRecommender.MakeSuggestions(showId, partyRequested);

            var seatNames = suggestionsMade.SeatNames(PricingCategory.Second);
            Check.That(seatNames).ContainsExactly("A1", "A2", "A9", "A10", "B1", "B2");
        }

        [Test]
        public void Suggest_three_availabilities_per_PricingCategory()
        {
            // New Amsterdam-18
            //
            //     1   2   3   4   5   6   7   8   9  10
            //  A: 2   2   1   1   1   1   1   1   2   2
            //  B: 2   2   1   1   1   1   1   1   2   2
            //  C: 2   2   2   2   2   2   2   2   2   2
            //  D: 2   2   2   2   2   2   2   2   2   2
            //  E: 3   3   3   3   3   3   3   3   3   3
            //  F: 3   3   3   3   3   3   3   3   3   3
            const string eventId = "18";
            const int partyRequested = 1;

            var auditoriumLayoutAdapter =
                new AuditoriumSeatingArrangements(new AuditoriumLayoutRepository(), new ReservationsProvider());

            var seatingPlaceRecommender = new SeatingArrangementRecommender(auditoriumLayoutAdapter);

            var suggestionsMade = seatingPlaceRecommender.MakeSuggestions(eventId, partyRequested);

            Check.That(suggestionsMade.SeatNames(PricingCategory.First)).ContainsExactly("A3", "A4", "A5");
            Check.That(suggestionsMade.SeatNames(PricingCategory.Second)).ContainsExactly("A1", "A2", "A9");
            Check.That(suggestionsMade.SeatNames(PricingCategory.Third)).ContainsExactly("E1", "E2", "E3");
        }
    }
}