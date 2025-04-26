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

    [Fact]
        public void StatementCoverage(){
            // 1. Test pentru distanta invalida (< 5 km)
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 4,
                passengers: 5,
                includeRests: false
            );
            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            // 2. Test pentru numar de pasageri invalid (0 pasageri)
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 0,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);

            // 3. Test pentru numar de pasageri invalid (> 25 pasageri)
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 30,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);

            // 4. Test pentru ramura care aplică majorare (pasageri < MaximumPeopleForBase)
            //    Pentru pasageri=1 (1 < 2), se aplică un factor de 1.1
            //    Sa folosim o distanta de 50 km si sa nu includem opririle.
            //    Calcul:
            //      - Cost de baza: 50 * 0.5 = 25
            //      - Majorare: 25 * 1.1 = 27.5
            //      - Calcul combustibil:
            //           Iteratia 1: fuelNeeded = 1, remaining = 50 - 10*(1+1)=50-20=30.
            //           Iteratia 2: fuelNeeded = 2, remaining = 30 - 10*(1+0.5)=30-15=15.
            //           Iteratia 3: fuelNeeded = 3, remaining = 15 - 10*(1+0.3333)=15-13.33≈1.67.
            //           Iteratia 4: fuelNeeded = 4, remaining = 1.67 - 10*(1+0.25)=1.67-12.5≈-10.83 → fuelNeeded=4.
            //      - Cost combustibil: 4 * 1.3 = 5.2
            //      - Total asteptat: 27.5 + 5.2 = 32.7
            double total = _distanceService.TotalTripCost(
                distanceInKm: 50,
                passengers: 1,
                includeRests: false
            );
            Assert.Equal(32.7, total);

            // 5. Test pentru ramura de discount (pasageri > MinimumPeopleForDiscount) si pentru includerea oprirlor (includeRests true) si pentru taxa suplimentara
            // la calatorii lungi
            //    Pentru 600 km, pasageri = 6:
            //      - Cost de bază: 300
            //      - Discount: 300 * 0.9 = 270
            //      - Stops: 600/25=24
            //      - Total cu taxa de stopuri: 270 + 7 * 24 = 438
            //      - Calcul combustibil:
            //            Se simulează calculul iterativ pentru 600 km.
            //      - Total intermediar: 438 + (fuelNeeded * 1.3)
            //      - Aplicare taxa suplimentara: total * 1.05
            total = _distanceService.TotalTripCost(
                distanceInKm: 600,
                passengers: 6,
                includeRests: true
            );
            Assert.Equal(536.34, total);    
        }
        [Fact]
        public void DecisionCoverage(){
            // Testarea 1: D1-true
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 4,
                passengers: 5,
                includeRests: false
            );
            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            // Testarea 2: D2-true, D1-false
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 0,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);

            // Testarea 3: D3-true, D1-false, D2-false
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 26,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);

            // Testarea 4: D5-true, D1-false, D2-false, D3-false, D4-false, D6-false, D9-false
            double total = _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 1,
                includeRests: false
            );
            Assert.Equal(6.8, total);

            // Testarea 5: D1-false, D2-false, D3-false, D4-false, D5-false, D6-false, D9-false
            total = _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 2,
                includeRests: false
            );
            Assert.Equal(6.3, total);

            //Testarea 6: D4-true, D6-true, D9-true, D1-false, D2-false, D3-false, D5-false
            total = _distanceService.TotalTripCost(
                distanceInKm: 600,
                passengers: 6,
                includeRests: true
            );
            Assert.Equal(536.34, total);
       }

        [Fact]
        public void ConditionCoverage(){
            // Testarea 1: D1-true
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 4,
                passengers: 5,
                includeRests: false
            );
            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            // Testarea 2: D2-true, D1-false
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 0,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);

            // Testarea 3: D3-true, D1-false, D2-false
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 26,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);

            // Testarea 4: D5-true, D1-false, D2-false, D3-false, D4-false, D6-false, D9-false
            double total = _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 1,
                includeRests: false
            );
            Assert.Equal(6.8, total);

            // Testarea 5: D1-false, D2-false, D3-false, D4-false, D5-false, D6-false, D9-false
            total = _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 2,
                includeRests: false
            );
            Assert.Equal(6.3, total);

            //Testarea 6: D4-true, D6-true, D9-true, D1-false, D2-false, D3-false, D5-false
            total = _distanceService.TotalTripCost(
                distanceInKm: 600,
                passengers: 6,
                includeRests: true
            );
            Assert.Equal(536.34, total);
        }
        [Fact]
        public void IndependentCircuit(){
        }
    }
}