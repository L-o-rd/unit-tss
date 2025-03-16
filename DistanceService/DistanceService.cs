namespace Distance.Services {
    public class DistanceService {
        private readonly double Maximum = 500.0;
        private readonly double Minimum =  10.0;

        public enum Metoda {
            Standard,
            Express,
            Urgent
        }

        public double Transport(double greutate, double distanta, Metoda metoda, bool fragil) {
            if (greutate <= 0.0)
                throw new ArgumentException("Greutatea nu poate fi negativa.", nameof(greutate));

            double rata;
            switch (metoda) {
                case Metoda.Standard: rata = 0.1; break;
                case Metoda.Express:  rata = 0.2; break;
                case Metoda.Urgent:   rata = 0.5; break;
                default: throw new ArgumentException("Metoda invalida!", nameof(metoda));
            }

            double total = greutate * distanta * rata;
            if (fragil)
                total += greutate * 0.5;

            if (greutate > 100.0)
                total += greutate * 0.1;

            int bucati = (int) Math.Floor(distanta / 50.0);
            for (int i = 0; i < bucati; ++i) {
                total += 3.0;
            }

            if (total < Minimum) {
                total = Minimum;
            } else if (total > Maximum) {
                total = Maximum;
            }

            return total;
        }
    }
}