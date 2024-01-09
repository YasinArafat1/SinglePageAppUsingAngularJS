using Microsoft.EntityFrameworkCore;
using PipilikaAPI.Data;
using PipilikaAPI.Mappings;
using PipilikaAPI.Repositories;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PipilikaDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("appCon")));
builder.Services.AddScoped<IDepartmentRepositories, SqlDepartmentRepositories>();
builder.Services.AddScoped<IEmployeeRepositories, SqlEmployeeRepositories>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddCors(opt => opt.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin()
.AllowAnyMethod().AllowAnyHeader()
));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");
app.MapControllers();
app.Run();
