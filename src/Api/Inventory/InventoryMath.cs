namespace Omne.Screen.Api.Inventory;

/// <summary>
/// Pure inventory helpers — deliberately free of EF/HTTP so they are trivially
/// unit-testable. The screening CI is expected to run the tests that cover this.
/// </summary>
public static class InventoryMath
{
    /// <summary>Sums quantities, treating negatives as zero (stock can't go below empty).</summary>
    public static int TotalQuantity(IEnumerable<int> quantities)
    {
        ArgumentNullException.ThrowIfNull(quantities);
        return quantities.Sum(q => q < 0 ? 0 : q);
    }
}
