using WebApp2.ApplicationLayer.Mappings;
using WebApp2.BusinessLayer;
using WebApp2.BusinessLayer.Services.Implementation;
using WebApp2.BusinessLayer.Services.Interface;
namespace WebApp2.ApplicationLayer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddBussinessLayer();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddHttpClient();


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
    }
}
