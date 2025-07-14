using Check_In_Check_Out_System.Data;
using Check_In_Check_Out_System.Interfaces;
using Check_In_Check_Out_System.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));

});
builder.Services.AddScoped<IRecordRepository, RecordRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
