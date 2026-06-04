using Omne.Screen.Api.Inventory;
using Xunit;

namespace Omne.Screen.Api.Tests;

public class InventoryMathTests
{
    [Fact]
    public void TotalQuantity_SumsPositiveValues()
    {
        Assert.Equal(49, InventoryMath.TotalQuantity([42, 7]));
    }

    [Fact]
    public void TotalQuantity_TreatsNegativesAsZero()
    {
        Assert.Equal(10, InventoryMath.TotalQuantity([10, -5]));
    }

    [Fact]
    public void TotalQuantity_EmptyIsZero()
    {
        Assert.Equal(0, InventoryMath.TotalQuantity([]));
    }
}
