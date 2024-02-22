
using Business.Cafes.Handlers;
using Business.Validators;
using DAL.Context;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DapperDAL.Repositories;
using DapperDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DapperDAL;
using DAL.Services;
using System.Text.Json.Serialization;

namespace BenchAPI
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
			builder.Services.AddDbContext<BeerContext>(cfg =>
			{
				cfg.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
			});
			builder.Services.AddSingleton<DapperContext>();

			builder.Services.AddScoped<ICafeRepository, Caferepository>();
			builder.Services.AddScoped<IBeerRepository, BeerRepository>();
			builder.Services.AddScoped<IOrderRepository, OrderRepository>();
			builder.Services.AddScoped<IOrderRepositoryDapper, OrderRepositoryDapper>();
			builder.Services.AddScoped<IEmailService,  EmailService>();

			builder.Services.AddScoped<CafeValidator>();

			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(GetCafesByEmailHandler).Assembly);
				//cfg.RegisterServicesFromAssembly(typeof(GetOrdersForCustomerQuery).Assembly);
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors(builder => builder.WithOrigins("http://localhost:4200", "http://localhost:65243", 
				"https://localhost:7221").AllowAnyHeader().AllowAnyMethod());

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
