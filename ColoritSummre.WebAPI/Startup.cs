using ColoritSummer.Data.MySQL.Context;
using ColoritSummer.Data.MySQL.Repositories;
using ColoritSummer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ColoritSummer.WebAPI
{
    public record class Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString("MySQLDatabase");
            services.AddDbContext<ColoritSummerDbContext>(opt => 
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped(typeof(IUserRepository<>), typeof(DbUserRepository<>));

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ColoritSummer.API", Version = "v1" });
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherAcquisition.API v1"));
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
