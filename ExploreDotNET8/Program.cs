var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); // Register controllers

builder.Services.AddCors((options) => 
{
    options.AddPolicy("DevCORS", (corsbuilder) =>
    {
       corsbuilder.WithOrigins("http://localhost:8000","http://localhost:4200","http://localhost:3000")
        .AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader();
    });

    options.AddPolicy("ProdCORS", (corsbuilder) =>
    {
       corsbuilder.WithOrigins("https://myProductionSite.com")
        .AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{   
    app.UseCors("DevCORS");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdCORS");
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers(); // Map controllers
});

app.Run();
