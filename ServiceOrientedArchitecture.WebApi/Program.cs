using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceOrientedArchitecture.Data.Data;
using ServiceOrientedArchitecture.Repositories;
using ServiceOrientedArchitecture.Services;
using SoftwareEngineering.Profiles;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
    .Build();

services.AddDbContext<DataContext>(options =>
    options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

services.AddControllers();
services.AddEndpointsApiExplorer();

services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<UserProfile>();
});

IMapper mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);

services.AddTransient<IUserService, UserService>();
services.AddTransient<IBankingService, BankingService>();

services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
