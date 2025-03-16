using Distance.Services;

namespace Distance.Tests.Services {
    public class DistanceService_TransportUnit {
        private readonly DistanceService _distanceService;

        public DistanceService_TransportUnit() {
            _distanceService = new DistanceService();
        }

        [Theory]
        [InlineData(-1.0)]
        [InlineData(-2.0)]
        [InlineData(-3.0)]
        public void Transport_GreutateNegativa(double val) {
            Action act = () => _distanceService.Transport(greutate: val, 0.0, DistanceService.Metoda.Standard, false);
            var exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Greutatea nu poate fi negativa. (Parameter 'greutate')", exception.Message);
        }

        [Fact]
        public void Transport_ValoriStandard() {
            var val = _distanceService.Transport(
                greutate: 10.0,
                distanta: 50.0,
                metoda: DistanceService.Metoda.Standard,
                fragil: false
            );

            Assert.Equal(53.0, val);
        }
    }
}