using Hangman.DAL;
using Hangman.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<GameContext>();

builder.Services.AddTransient<GamesRepository>();
builder.Services.AddTransient<PlayersRepository>();
builder.Services.AddTransient<WordsRepository>();
builder.Services.AddTransient<StatsRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("frontend");


// map api controllers: om razor pages api te gebruiken
app.MapControllers();
app.MapRazorPages();

app.Run();
