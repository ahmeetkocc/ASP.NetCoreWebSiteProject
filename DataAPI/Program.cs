using DataAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container using the extension method
builder.Services.AddApplicationServices();

var app = builder.Build();

// Use CORS with the specified policy name
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
