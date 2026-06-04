namespace Omne.Screen.Api.Models;

/// <summary>A trivial domain entity — just enough to exercise EF Core + Postgres.</summary>
public sealed class Widget
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
}
