using Microsoft.EntityFrameworkCore;
using Orders.BackEnd.Data;
using Orders.BackEnd.UnitsOfWork.Implementations;
using Orders.BackEnd.UnitsOfWork.Interfaces;
using Orders.BackEnd.Repositories.Implementations;
using Orders.BackEnd.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(op => op.UseSqlServer("name=LocalConnection"));

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

app.UseCors(op =>
{    
    op.
    AllowAnyMethod().
    AllowAnyHeader().
    SetIsOriginAllowed(origin => true).
    AllowCredentials();    
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
