var builder = WebApplication.CreateBuilder(args);

// The real BFF terminates OIDC (Keycloak) and proxies to the API via YARP.
// For the screen we keep just the reverse-proxy half; auth belongs to the
// longer exercise.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// The BFF's own liveness — independent of whether the API is up.
app.MapGet("/health", () => Results.Ok(new { status = "healthy", service = "bff" }));

app.MapReverseProxy();

app.Run();
