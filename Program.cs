using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.Data;
using MovieManagementSystem.Models;
using MovieManagementSystem.Repository;
using MovieManagementSystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ManagementContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("ConString")));
builder.Services.AddScoped<ICRUD<Actor>, ActorRepository>();

builder.Services.AddScoped<ICRUD<Director>, DirectorRepository>();
builder.Services.AddScoped<ICRUD<Movie>, MovieRepository>();
builder.Services.AddScoped<ICRUD<Review>, ReviewRepository>();
builder.Services.AddScoped<ICRUD<Genre>, GenreRepository>();

builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IGenreService, GenreService>();

var app = builder.Build();

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
