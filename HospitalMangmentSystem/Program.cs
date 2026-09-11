
namespace HospitalMangmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddProblemDetails();
            builder.Services.AddProcurementApplication();
            builder.Services.AddProcurementInfrastructure(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseExceptionHandler(exceptionApp =>
            {
                exceptionApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await Results.Problem(
                        title: "Domain error",
                        detail: "The requested operation violates a business rule.",
                        statusCode: StatusCodes.Status400BadRequest)
                        .ExecuteAsync(context);
                });
            });

            app.MapProcurementEndpoints();


            app.Run();
        }
    }
}
