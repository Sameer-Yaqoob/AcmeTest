using AcmeCorpAPI.DBContext;
using AcmeCorpAPI.Handler;
using AcmeCorpAPI.Interfaces;
using AcmeCorpAPI.Repositories;
using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication("ApiKey")
            .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", null);
        
        builder.Services.AddDbContext<AcmeCorpDBContext>(options =>
            options.UseInMemoryDatabase(databaseName: "AcmeCorpDB"));
        // Authorization policy for API Key
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiKeyPolicy", policy =>
            {
                policy.AuthenticationSchemes.Add("ApiKey");
                policy.RequireAuthenticatedUser();
            });
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

        var app = builder.Build();
        app.UseWebSockets();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAnyOrigin");
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers().RequireAuthorization("ApiKeyPolicy");
        });

        app.MapControllers();

        app.Run();
    }
}