using Distance.Services;

namespace Distance.Tests.Services {
    public class DistanceService_TripTests {
        private readonly DistanceService _distanceService;

        public DistanceService_TripTests() {
            _distanceService = new DistanceService();
        }

        [Fact]
        public void TripTest() {
            Action act = () => _distanceService.TotalTripCost(-1.0, 1, true);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and non-zero. (Parameter 'distanceInKm')", exception.Message);
        }
    }
}