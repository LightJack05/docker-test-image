using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;

namespace DockerTestImage;


public static class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DatabaseHandle>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.MapGet("/ping", (DatabaseHandle databaseHandle) =>
        {
            return "pong";
        });

        app.MapGet("/increment", (DatabaseHandle databaseHandle) =>
        {
            databaseHandle.Database.EnsureCreated();
            if (databaseHandle.Counters.Count() == 0)
            {
                databaseHandle.Counters.Add(new());
                databaseHandle.SaveChanges();
            }
            databaseHandle.Counters.FirstOrDefault().State++;
            databaseHandle.SaveChanges();
            return databaseHandle.Counters.FirstOrDefault().State;
        });

        app.MapGet("/reset", (DatabaseHandle databaseHandle) =>
        {
            databaseHandle.Database.EnsureCreated();
            if (databaseHandle.Counters.Count() == 0)
            {
                databaseHandle.Counters.Add(new());
                databaseHandle.SaveChanges();
            }
            databaseHandle.Counters.FirstOrDefault().State = 0;
            databaseHandle.SaveChanges();
            return databaseHandle.Counters.FirstOrDefault().State;
        });

        app.Run();

    }
}
