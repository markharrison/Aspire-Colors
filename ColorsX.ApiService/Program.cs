var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseCors();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var colorItems = new List<ColorItem>
{
    new ColorItem("Red", "#FF0000"),
    new ColorItem("Yellow", "#FFFF00"),
    new ColorItem("Black", "#000000")
};

// Add route to get color items
app.MapGet("/colors", () =>
{
    return colorItems;
});

// add route to get random color
app.MapGet("/colors/random", () =>
{
    var randomColor = colorItems[Random.Shared.Next(colorItems.Count)];
    return randomColor;
});

// add route to add color item
app.MapPost("/colors", (ColorItem colorItem) =>
{
    colorItems.Add(colorItem);
    return Results.Created($"/colors/{colorItem.Name}", colorItem);
});

app.MapDefaultEndpoints();

app.Run();

// Add color item - include name and hexcode 
public record ColorItem(string Name, string HexCode);






