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
            double fuelNeeded = 0.0;
            double remaining = 600;
            double efficiency = 10.0;
            while (remaining > 0.0)
            {
                fuelNeeded += 1.0;
                remaining -= efficiency * (1.0 + (1.0 / fuelNeeded));
            }
            double fuelCost = fuelNeeded * 1.3;
            double subtotal = 438 + fuelCost;
            double expectedTotal = subtotal * 1.05;
            total = _distanceService.TotalTripCost(
                distanceInKm: 600,
                passengers: 6,
                includeRests: true
            );
            Assert.Equal(expectedTotal, total);    
        }
        [Fact]
        public void DecisionCoverage(){
            // DECIZIA 1: if (distanceInKm < 5.0)
            // - Ramura true: distanta invalida -> arunca exceptie.
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 4,
                passengers: 5,
                includeRests: false
            );
            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);
            
            // - Ramura false: distanta valida.
            // Folosim o distanta valida pentru urmatorul test(ex. 10 km).

            // DECIZIA 2: if (passengers <= 0) / else if (passengers > 25)
            // - Ramura true pentru pasageri <= 0
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 0,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);
            
            // - Ramura false: numar de pasageri mai mare decat 0.
            // Folosim un numar valid de pasageri pentru urmatorul test(ex. 30 pasageri)

            // - Ramura true pentru pasageri > 25
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 30,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);

            // - Ramura false: numar de pasageri mai mic decat 25.
            // Folosim un numar valid de pasageri pentru urmatorul test(ex. 1 pasager)

            // DECIZIA 3: if (passengers > MinimumPeopleForDiscount) / else if (passengers < MaximumPeopleForBase)
            // - Ramura true pentru passengers > 5
            double total = _distanceService.TotalTripCost(
                distanceInKm: 50,
                passengers: 6,
                includeRests: false
            );
            Assert.Equal(27.7, total);

            // - Ramura false: numar de pasageri mai mic sau egal cu 5.
            // Folosim un numar valid de pasageri pentru urmatorul test(ex. 1 pasager)

            // - Ramura true pentru passengers < 2
            total = _distanceService.TotalTripCost(
                distanceInKm: 50,
                passengers: 1,
                includeRests: false
            );
            Assert.Equal(32.7, total);

            // - Ramura false: numar de pasageri mai mare sau egal cu 2.
            // Folosim un numar valid de pasageri pentru urmatorul test(ex. 2 pasageri)

            // DECIZIA 4: if (includeRests)
            // - Ramura true pentru includeRests == true
            total = _distanceService.TotalTripCost(
                distanceInKm: 50,
                passengers: 2,
                includeRests: true
            );
            Assert.Equal(44.2, total);
            // - Ramura false : includeRests este false
            // Am facut deja asta mai inainte

            //DECIZIA 5: if ((passengers > DistanceService.MinimumPeopleForDiscount) && (distanceInKm > 500))
            // - Ramura true pentru passengers > 5 si distanceInKm > 500
            double fuelNeeded = 0.0;
            double remaining = 600;
            double efficiency = 10.0;
            while (remaining > 0.0)
            {
                fuelNeeded += 1.0;
                remaining -= efficiency * (1.0 + (1.0 / fuelNeeded));
            }
            double fuelCost = fuelNeeded * 1.3;
            double subtotal = 270 + fuelCost;
            double expectedTotal = subtotal * 1.05;
            total = _distanceService.TotalTripCost(
                distanceInKm: 600,
                passengers: 6,
                includeRests: false
            );
            Assert.Equal(expectedTotal, total);
            // - Ramura false : ori cand passengers <=5 sau cand distanceInKm <=500
            // Am facut deja asta mai inainte

        }

        [Fact]
        public void ConditionCoverage(){
            // 1. Conditia: distanceInKm < 5.0
            // a) True: distanța invalida -> se asteaptă exceptie
            Action act = () => _distanceService.TotalTripCost(
                distanceInKm: 4,
                passengers: 10,
                includeRests: false
            );
            var exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Distance should be positive and at least five kilometers. (Parameter 'distanceInKm')", exception.Message);

            // b) False: distanta valida folosim dupa

            // 2. Conditia: passengers <= 0
            // a) True: pasageri 0 -> se asteaptă exceptie
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 0,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be at least one. (Parameter 'passengers')", exception.Message);

            // b) False: numar de pasageri mai mare decat 0 folosim dupa

            // 3. Conditia: passengers > 25
            // a) True: prea multi pasageri -> se asteapta exceptie
            act = () => _distanceService.TotalTripCost(
                distanceInKm: 10,
                passengers: 30,
                includeRests: false
            );
            exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("Number of passengers should be maximum 25. (Parameter 'passengers')", exception.Message);

            // b) False: numar de pasageri mai mic sau egal decat 25 folosim dupa

            // 4. Conditia: passengers > MinimumPeopleForDiscount
            // a) True: ex. 6 pasageri (presupunand MinimumPeopleForDiscount=5) => se aplica discountul
            double totalWithDiscount = _distanceService.TotalTripCost(
                distanceInKm: 50,
                passengers: 6,
                includeRests: false
            );
            // b) False: ex. 3 pasageri => discountul nu se aplica
            double totalNoDiscount = _distanceService.TotalTripCost(
                distanceInKm: 50,
                passengers: 3,
                includeRests: false
            );
            // Verificam indirect ca valorile difera
            Assert.True(totalWithDiscount < totalNoDiscount);

            // 5. Conditia: passengers < MaximumPeopleForBase
            // a) True: ex. 1 pasager (presupunand MaximumPeopleForBase=2) => se aplica majorarea
            double totalWithMajoration = _distanceService.TotalTripCost(
                distanceInKm: 50,
                passengers: 1,
                includeRests: false
            );
            // b) False: ex. 2 pasageri => nu se aplica majorarea
            double totalNeutral = _distanceService.TotalTripCost(
                distanceInKm: 50,
                passengers: 2,
                includeRests: false
            );
            // Deoarece majorarea mareste costul, se asteapta ca totalul cu majorare sa fie mai mare
            Assert.True(totalWithMajoration > totalNeutral);

            // 6. Conditia: includeRests
            // a) True: includeRests=true
            double totalWithRests = _distanceService.TotalTripCost(
                distanceInKm: 60,
                passengers: 6,
                includeRests: true
            );
            // b) False: includeRests=false
            double totalWithoutRests = _distanceService.TotalTripCost(
                distanceInKm: 60,
                passengers: 6,
                includeRests: false
            );
            // Costul cu opriri incluse ar trebui sa fie mai mare
            Assert.True(totalWithRests > totalWithoutRests);

            // 7. Conditia compusa: (passengers > MinimumPeopleForDiscount) && (distanceInKm > 500)
            // Vom testa toate combinațiile:
            // a) Ambele adevarate: ex. pasageri = 6, distance = 600 -> se aplica taxa suplimentara
            double totalExtraTax = _distanceService.TotalTripCost(
                distanceInKm: 600,
                passengers: 6,
                includeRests: false
            );
            // b) passengers > MinimumPeopleForDiscount false, dar distanceInKm > 500 adevarat
            double totalNoExtraTax1 = _distanceService.TotalTripCost(
                distanceInKm: 600,
                passengers: 3,
                includeRests: false
            );
            // c) passengers > MinimumPeopleForDiscount adevarat, dar distanceInKm > 500 fals (ex. 500 km)
            double totalNoExtraTax2 = _distanceService.TotalTripCost(
                distanceInKm: 500,
                passengers: 6,
                includeRests: false
            );
            // d) Ambele false: ex. pasageri = 3, distance = 500
            double totalNoExtraTax3 = _distanceService.TotalTripCost(
                distanceInKm: 500,
                passengers: 3,
                includeRests: false
            );
            Assert.Equal(360,totalExtraTax,0.1);
            Assert.Equal(372.8,totalNoExtraTax1,0.1);
            Assert.Equal(totalNoExtraTax2,totalNoExtraTax2);
        }
    }
}