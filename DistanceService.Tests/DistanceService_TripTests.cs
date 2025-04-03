using Distance.Services;

namespace Distance.Tests.Services {
    public class DistanceService_TripTests {
        private readonly DistanceService _distanceService;

        public DistanceService_TripTests() {
            _distanceService = new DistanceService();
        }

        [Fact]
        public void EquivalencePartitioning() {
            /* Testul T1. */
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 4,
                passengers: 0,
                includeRests: false
            );

            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            /* Testul T211. */
            var total = _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 3,
                includeRests: true
            );

            Assert.Equal(88.4, total);

            /* Testul T212. */
            total = _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 3,
                includeRests: false
            );

            Assert.Equal(60.4, total);

            /* Testul T22. */
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 0,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);

            /* Testul T23. */
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 30,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);
        }

        [Fact]
        public void BoundaryValueAnalysis() {
            /* Testul pentru Eq1. */
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 5 - 1e-4 /* epsilon */,
                passengers: 0,
                includeRests: false
            );
            
            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            /* Testele pentru Eq211. */
            var total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 1,
                includeRests: true
            );

            Assert.Equal(4.05, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 25,
                includeRests: true
            );

            Assert.Equal(3.55, total);

            /* Testele pentru Eq212. */
            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 1,
                includeRests: false
            );

            Assert.Equal(4.05, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 25,
                includeRests: false
            );

            Assert.Equal(3.55, total);

            /* Testul pentru Eq22. */
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 0,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);

            /* Testul pentru Eq23. */
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 26,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);
        }

        [Fact]
        public void CategoryPartitioning() {
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 5 - 1e-4 /* epsilon */,
                passengers: 0,
                includeRests: false
            );

            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            act = () => _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 0,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);

            act = () => _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 0,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);

            act = () => _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 0,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);

            act = () => _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 26,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);

            act = () => _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 26,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);

            act = () => _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 26,
                includeRests: false
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);

            var total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 1,
                includeRests: true
            );

            Assert.Equal(4.05, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 1,
                includeRests: false
            );

            Assert.Equal(4.05, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 3,
                includeRests: true
            );

            Assert.Equal(3.8, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 3,
                includeRests: false
            );

            Assert.Equal(3.8, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 6,
                includeRests: true
            );

            Assert.Equal(3.55, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 6,
                includeRests: false
            );

            Assert.Equal(3.55, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 25,
                includeRests: true
            );

            Assert.Equal(3.55, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 25,
                includeRests: false
            );

            Assert.Equal(3.55, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 1,
                includeRests: true
            );

            Assert.Equal(714.8, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 1,
                includeRests: false
            );

            Assert.Equal(504.8, total, 1e-3);

            total = _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 3,
                includeRests: true
            );

            Assert.Equal(677.3, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 3,
                includeRests: false
            );

            Assert.Equal(467.3, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 6,
                includeRests: true
            );

            Assert.Equal(671.79, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 6,
                includeRests: false
            );

            Assert.Equal(451.29, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 25,
                includeRests: true
            );

            Assert.Equal(671.79, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 750,
                passengers: 25,
                includeRests: false
            );

            Assert.Equal(451.29, total);
        }
    }
}