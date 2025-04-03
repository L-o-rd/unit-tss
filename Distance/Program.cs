using System.Globalization;
using Distance.Services;

public class Program {
    private static readonly DistanceService Service = new DistanceService();
    private static double Distance = 0.0;
    private static String? Line = null;
    private static int Passengers = 1;
    private static bool Rests = false;
    private static int Choice = 0;

    public static void Main(string[] args) {
        var changed = false;
        while (true) {
            Console.Clear();
            Console.WriteLine("Distance Services: `Trip`");
            Console.WriteLine($"    1. Distance: {Distance}.");
            Console.WriteLine($"    2. Passengers: {Passengers}.");
            Console.WriteLine($"    3. Rests: {Rests}.");
            Console.WriteLine($" quit. Quits the application.");
            Console.WriteLine($" trip. Computes your total.");
            Console.WriteLine();
            Console.Write("~> ");

            try {
                Line = Console.ReadLine();
                if (Line is not null) {
                    Line = Line.Trim();
                    Choice = Int32.Parse(Line);
                }

            } catch {
                if ((Line is not null) && (String.Equals(Line, "quit"))) {
                    return;
                }
            }

            try {
                if ((Line is not null) && (String.Equals(Line, "trip"))) {
                    var total = Service.TotalTripCost(Distance, Passengers, Rests);
                    Console.WriteLine($"~> Your total is: {total}.");
                    Console.ReadKey();
                    continue;
                }
            } catch (Exception e) {
                Console.WriteLine($"Error: {e.Message}");
                Console.ReadKey();
                continue;
            }

            if (Choice == 1) {
                Console.WriteLine("~> Change Distance to: ");
                changed = false;
                while (true) {
                    try {
                        Distance = Double.Parse(Console.ReadLine() ?? "0.0", CultureInfo.InvariantCulture);
                        changed = true;
                    } catch {
                        Console.WriteLine("Error: invalid distance.");
                    }

                    if (changed) break;
                }
            } else if (Choice == 2) {
                Console.WriteLine("~> Change Passengers to: ");
                changed = false;
                while (true) {
                    try {
                        Passengers = Int32.Parse(Console.ReadLine() ?? "0");
                        changed = true;
                    } catch {
                        Console.WriteLine("Error: invalid number of passengers.");
                    }

                    if (changed) break;
                }
            } else if (Choice == 3) {
                Console.WriteLine("~> Change Rests to: ");
                changed = false;
                while (true) {
                    try {
                        Rests = Boolean.Parse(Console.ReadLine() ?? "false");
                        changed = true;
                    } catch {
                        Console.WriteLine("Error: invalid choice for boolean Rests.");
                    }

                    if (changed) break;
                }
            } else {
                Console.WriteLine("Error: invalid choice.");
                Console.ReadKey();
            }
        }
    }
}