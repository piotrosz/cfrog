using CookingFrog.Domain;
using CookingFrog.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRecipesRepo, RecipesStaticTestRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/recipes", (IRecipesRepo recipesRepo) =>
    {
        return recipesRepo.GetRecipes();
    })
    .WithName("GetRecipes")
    .WithOpenApi();

app.Run();
