using BidCalculatorLibrary;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // In a bigger application I probably would not put these as Singletons, but as calculation is the only purpose of this app, I think it makes sense
        builder.Services.AddKeyedSingleton<IFeeCalculator>(serviceKey: (object)AuctionTypesEnum.Common, GetCommonFeeCalculator());
        builder.Services.AddKeyedSingleton<IFeeCalculator>(serviceKey: (object)AuctionTypesEnum.Luxury, GetLuxuryFeeCalculator());

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
        ;
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options => {
            options.AddPolicy("AllowAll", policy => {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    // These should be built from config or DB
    static IFeeCalculator GetCommonFeeCalculator()
    {
        return new FeeCalculator() {
            FeeList = new List<ICalculatable>() {
                new PercentCapedFee() { FeeType = FeeTypesEnum.Basic, Percent = 0.1m, MinFee = 10m, MaxFee = 50m },
                new PercentFee() { FeeType = FeeTypesEnum.Special, Percent = 0.02m },
                new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1m, MaxRange = 500m, FeeAmount = 5m },
                new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 501m, MaxRange = 1000m, FeeAmount = 10m },
                new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1001m, MaxRange = 3000m, FeeAmount = 15m },
                new FixedMinRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 3001m, FeeAmount = 20m },
                new FixedFee() { FeeType = FeeTypesEnum.Storage, FeeAmount = 100m }
            }
        };
    }

    static IFeeCalculator GetLuxuryFeeCalculator()
    {
        return new FeeCalculator() {
            FeeList = new List<ICalculatable>() {
                new PercentCapedFee() { FeeType = FeeTypesEnum.Basic, Percent = 0.1m, MinFee = 25m, MaxFee = 200m },
                new PercentFee() { FeeType = FeeTypesEnum.Special, Percent = 0.04m },
                new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1m, MaxRange = 500m, FeeAmount = 5m },
                new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 501m, MaxRange = 1000m, FeeAmount = 10m },
                new FixedMinMaxRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 1001m, MaxRange = 3000m, FeeAmount = 15m },
                new FixedMinRangeFee() { FeeType = FeeTypesEnum.Association, MinRange = 3001m, FeeAmount = 20m },
                new FixedFee() { FeeType = FeeTypesEnum.Storage, FeeAmount = 100m }
            }
        };
    }
}