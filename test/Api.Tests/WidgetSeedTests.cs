using Microsoft.EntityFrameworkCore;
using Omne.Screen.Api.Data;
using Xunit;

namespace Omne.Screen.Api.Tests;

public class WidgetSeedTests
{
    private static AppDbContext NewContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"widgets-{Guid.NewGuid()}")
            .Options;
        var ctx = new AppDbContext(options);
        ctx.Database.EnsureCreated();
        return ctx;
    }

    [Fact]
    public void Seed_PopulatesTwoWidgets()
    {
        using var ctx = NewContext();
        Assert.Equal(2, ctx.Widgets.Count());
    }

    [Fact]
    public void Seed_ContainsSprocket()
    {
        using var ctx = NewContext();
        Assert.Contains(ctx.Widgets, w => w.Name == "Sprocket" && w.Quantity == 42);
    }
}
