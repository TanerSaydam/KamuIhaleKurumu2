using FuzzyString;

namespace FuzzyStringConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        List<string> names = new()
        {
            "Book",
            "Kitap",
            "Buch",
            "Katip"
        };

        string searchTerm = "İ"; // Your search term, slightly misspelled

        List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>
        {
            FuzzyStringComparisonOptions.UseJaccardDistance,
            FuzzyStringComparisonOptions.UseHammingDistance,
            FuzzyStringComparisonOptions.UseJaroDistance,
            FuzzyStringComparisonOptions.UseLevenshteinDistance,
            // Add more options here depending on what aspects of the comparison you care about
        };

        FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Normal; // Set the tolerance level

        foreach (var name in names)
        {
            if (name.ApproximatelyEquals(searchTerm, options, tolerance))
            {
                Console.WriteLine($"'{searchTerm}' is approximately equal to '{name}'");
            }
        }

        Console.WriteLine("Hello, World!");
    }
}