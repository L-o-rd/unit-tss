namespace Distance.Services {
    public class DistanceService {
        private static readonly double CostPerStop = 7.0 /* units */;
        private static readonly int DistancePerStop = 25 /* km */;
        private static readonly int MinimumPeopleForDiscount = 5;
        private static readonly int MaximumPeopleForBase = 2;
        private static readonly double BasePerKm = 0.5;
        private static readonly double Epsilon = 1e-7;

        /// <summary>
        /// Computes the total cost of a trip based on the @distance, number of @people and expenses.
        /// </summary>
        /// 
        /// <param name="distanceInKm">
        ///     The distance in kilometers.
        ///     Should be > 0.
        /// </param>
        /// 
        /// <param name="passengers">
        ///     The number of passengers.
        ///     Should be in the range (0, 25].
        /// </param>
        /// 
        /// <param name="includeRests">
        ///     Tax in based on scheduled rests.
        ///     Should be True or False.
        /// </param>
        /// 
        /// <returns>Total cost of the trip.</returns>
        public double TotalTripCost(double distanceInKm, int passengers, bool includeRests) {
            /* Standard checks. */
            if (distanceInKm <= DistanceService.Epsilon)
                throw new ArgumentOutOfRangeException(nameof(distanceInKm), "Distance should be positive and non-zero.");

            if (passengers <= 0)
                throw new ArgumentOutOfRangeException(nameof(passengers), "Number of passengers should be at least one.");
            else if (passengers > 25)
                throw new ArgumentOutOfRangeException(nameof(passengers), "Number of passengers should be maximum 25.");

            /* Base cost for the trip. */
            double total = distanceInKm * DistanceService.BasePerKm;

            /* Apply discounts based on the number of people. */
            if (passengers > DistanceService.MinimumPeopleForDiscount) {
                total *= 0.9;
            } else {
                if (passengers < DistanceService.MaximumPeopleForBase) {
                    total *= 1.1;
                }
            }

            /* Tax in the scheduled rests. */
            if (includeRests) {
                int stops = (int) Math.Floor(distanceInKm / DistanceService.DistancePerStop);
                for (int i = 0; i < stops; ++i) {
                    total += DistanceService.CostPerStop;
                }
            }

            /* Take fuel into consideration. */
            double efficiency = 10 /* l/km */;
            double remaining = distanceInKm;
            double fuelNeeded = 0.0;
            while (remaining > 0.0) {
                fuelNeeded += 1.0;
                remaining -= efficiency * (1.0 + (1.0 / fuelNeeded));
            }

            total += fuelNeeded * 1.3;

            /* If the trip is long, apply an additional charge. */
            if ((passengers > DistanceService.MinimumPeopleForDiscount)
                && (distanceInKm > 500)) {
                total *= 1.05;
            }

            return total;
        }
    }
}