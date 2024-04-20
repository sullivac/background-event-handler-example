using System.Text.Json;
using BackgroundEventHandlerExample.MessageProcessing;
using BackgroundEventHandlerExample.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMessageHandlers();

builder.Services.AddSingleton(new JsonSerializerOptions(JsonSerializerDefaults.Web));

builder.Services.AddScoped<IProductRepository, ConsoleProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet(
    "/api/v1/products/{id}",
    async (int id, IProductRepository productRepository) =>
    {
        var product = await productRepository.GetAsync(id);
        if (product is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(product);
    })
    .WithName("GetProduct")
    .WithOpenApi();

app.MapPost(
    "/api/v1/commands",
    (MessageDto messageDto, IMessageQueue messageQueue, HttpContext httpContext) =>
    {
        _ = messageQueue.EnqueueAsync(messageDto, httpContext.RequestAborted);

        return Results.Accepted();
    })
    .WithName("PostCommand")
    .WithOpenApi();

app.Run();
