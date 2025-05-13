using Distance.Services;

namespace Distance.Tests.Services {
    public class DistanceService_AITests {
        private readonly DistanceService _distanceService;
        public DistanceService_AITests() {
            _distanceService = new DistanceService();
        }

        [Fact]
        public void EquivalencePartitioning() {
            /* Testul T1. */
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 4.9,
                passengers: 3,
                includeRests: false
            );

            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            /* Testul T211. */
            var total = _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 1,
                includeRests: true
            );

            Assert.Equal(93.4, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 500,
                passengers: 25,
                includeRests: true
            );

            Assert.Equal(424.8, total);

            /* Testul T212. */
            total = _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 5,
                includeRests: false
            );

            Assert.Equal(6.3, total);

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
                passengers: 26,
                includeRests: true
            );

            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);
        }

        [Fact]
        public void BoundaryValueAnalysis() {
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 4.999,
                passengers: 1,
                includeRests: false
            );

            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            var total = _distanceService.TotalTripCost(
                distanceInKm: 5,
                passengers: 1,
                includeRests: false
            );

            Assert.Equal(4.05, total);
        }

        [Fact]
        public void CategoryPartitioning() {
            var total = _distanceService.TotalTripCost(
                distanceInKm: 20,
                passengers: 1,
                includeRests: true
            );

            Assert.Equal(12.3, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 200,
                passengers: 3,
                includeRests: false
            );

            Assert.Equal(122.1, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 600,
                passengers: 6,
                includeRests: true
            );

            Assert.Equal(536.34, total);
        }

        [Fact]
        public void KillMutants() {
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 0,
                passengers: 1,
                includeRests: false
            );

            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            var total = _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 5,
                includeRests: false
            );

            Assert.Equal(60.4, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 6,
                includeRests: false
            );

            Assert.Equal(55.4, total);

            total = _distanceService.TotalTripCost(
                distanceInKm: 100,
                passengers: 6,
                includeRests: true
            );

            Assert.Equal(83.4, total);
        }
    }
}