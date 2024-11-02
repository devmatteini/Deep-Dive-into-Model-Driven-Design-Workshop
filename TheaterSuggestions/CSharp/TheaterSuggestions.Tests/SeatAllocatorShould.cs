using ExternalDependencies.AuditoriumLayoutRepository;
using ExternalDependencies.ReservationsProvider;
using NFluent;
using NUnit.Framework;

namespace SeatsSuggestions.Tests
{
    [TestFixture]
    public class SeatAllocatorShould
    {
        /*
         *  Business Rule - Only Suggest available seats
         */
        
        [Test]
        public void Suggest_two_seats_when_Auditorium_contains_all_available_seats()
        {
            // Lincoln-17
            //     1   2   3   4   5   6   7   8   9  10
            //  A: 2   2   1   1   1   1   1   1   2   2
            //  B: 2   2   1   1   1   1   1   1   2   2
            const string showId = "17";
            var auditoriumLayoutAdapter =
                new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());
            
            // Remove this assertion to the expected one with outcome : A1, A2
            var auditoriumSeating = auditoriumLayoutAdapter.GetAuditoriumSeating(showId);
            Check.That(auditoriumSeating.Rows).HasSize(2);
        }

        [Test]
        public void Suggest_one_seat_when_Auditorium_contains_one_available_seat_only()
        {
            // Ford Auditorium-1
            //       1   2   3   4   5   6   7   8   9  10
            //  A : (2) (2)  1  (1) (1) (1) (1) (1) (2) (2)
            //  B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
            const string showId = "1";
            var auditoriumLayoutAdapter =
                new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());
            // Remove this assertion to the expected one with outcome : A3
            var auditoriumSeating = auditoriumLayoutAdapter.GetAuditoriumSeating(showId);
            Check.That(auditoriumSeating.Rows).HasSize(2);
        }

        [Test]
        public void Return_SuggestionNotAvailable_when_Auditorium_has_all_its_seats_already_reserved()
        {
            // Madison Auditorium-5
            //      1   2   3   4   5   6   7   8   9  10
            // A : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
            // B : (2) (2) (1) (1) (1) (1) (1) (1) (2) (2)
            const string showId = "5";
            var auditoriumLayoutAdapter =
                new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());
            
            // Remove this assertion to the expected one with outcome : SuggestionNotAvailable
            var auditoriumSeating = auditoriumLayoutAdapter.GetAuditoriumSeating(showId);
            Check.That(auditoriumSeating.Rows).HasSize(2);
        }
        
        /*
         *  Business Rule - Suggest several suggestions ie 1_per_PricingCategory
         */
        
        [Test]
        public void Offer_several_suggestions_ie_1_per_PricingCategory()
        {
            // New Amsterdam-18
            //     1   2   3   4   5   6   7   8   9  10
            //  A: 2   2   1   1   1   1   1   1   2   2
            //  B: 2   2   1   1   1   1   1   1   2   2
            //  C: 2   2   2   2   2   2   2   2   2   2
            //  D: 2   2   2   2   2   2   2   2   2   2
            //  E: 3   3   3   3   3   3   3   3   3   3
            //  F: 3   3   3   3   3   3   3   3   3   3
            const string showId = "18";
            var auditoriumLayoutAdapter =
                new AuditoriumSeatingAdapter(new AuditoriumLayoutRepository(), new ReservationsProvider());
            // Remove this assertion to the expected one with outcome :
            // PricingCategory.First => "A3", "A4", "A5"
            // PricingCategory.Second => "A1", "A2", "A9"
            // PricingCategory.Third => "E1", "E2", "E3"
            var auditoriumSeating = auditoriumLayoutAdapter.GetAuditoriumSeating(showId);
            Check.That(auditoriumSeating.Rows).HasSize(6);
        }
    }
}