using BusinessLogicLayer.Services;
using BussinessLogicLayer.Services.IService_Interface;
using BussinessLogicLayer.Services.Service_Class;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repository.IRepository_Interface;
using DataAccessLayer.Repository.Repository_Class;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IRepository<Doctors>, DoctorsRepository>();
builder.Services.AddScoped<IDoctorsService, DoctorsService>();



builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
builder.Services.AddScoped<IPatientsService, PatientsService>();


builder.Services.AddScoped <IMailRepository, MailRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DP", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DP");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
