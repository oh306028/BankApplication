using BankApplication.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace BankApplication.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddScoped<ExceptionHandlingMiddleware>();

            builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.Scan(scan => scan
                    .FromAssemblies(Assembly.GetExecutingAssembly())
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());


            var jwtOptionSection = builder.Configuration.GetRequiredSection("Jwt");
            builder.Services.Configure<JwtOptions>(jwtOptionSection);


            var jwtOptions = new JwtOptions();
            jwtOptionSection.Bind(jwtOptions);
            builder.Services.AddSingleton(jwtOptions);

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                    new UnprocessableEntityObjectResult(context.ModelState);
            });



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(jwtOptions =>
            {
                var configKey = jwtOptionSection["Key"];
                var key = Encoding.UTF8.GetBytes(configKey);



                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptionSection["Issuer"],
                    ValidAudience = jwtOptionSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                };
            });


            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builder =>
                        builder.AllowAnyHeader()
                        .AllowAnyMethod()
                         .AllowAnyHeader()
                        .WithExposedHeaders("Location")
                        .WithOrigins("http://localhost:5173")

                );
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors("FrontEndClient");

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthorization();

            

            app.MapControllers();

            app.Run();
        }
    }
}
