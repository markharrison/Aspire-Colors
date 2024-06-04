using Swashbuckle.AspNetCore.SwaggerGen;
using ColorsX.Shared;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ColorX.API");
});

var colorItems = new List<ColorItem>
{
    new ColorItem("Red", "#FF0000"),
    new ColorItem("Yellow", "#FFFF00"),
    new ColorItem("Black", "#000000")
};

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

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






