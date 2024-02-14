using FitandFun.Models;
using FitandFun.Services;

var builder = WebApplication.CreateBuilder(args);

// Dodajte usluge u kontejner.

builder.Services.AddControllers();
// Naučite više o konfigurisanju Swagger/OpenAPI na https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DatabseSettings>(
    builder.Configuration.GetSection("MyDb")
);
builder.Services.AddTransient<IUserService,UserService>(); 
builder.Services.AddTransient<IWorkoutService,WorkoutService>();
builder.Services.AddTransient<IExerciseService,ExerciseService>();

// Dodaj CORS servis pre Build poziva
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Konfigurišite HTTP zahtevni pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReact");

app.UseAuthorization();


app.MapControllers();

app.Run();
