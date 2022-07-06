using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ColoritSummer.Interfaces.Repositories;
using ColoritSummer.Models.Entities;
using ColoritSummery.WebAPIClient.Repository;

namespace ColoritSummer.TestConsole
{
    internal class Programm
    {
        private static IHost? _hosting;
        public static IHost Hosting => _hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        public static IServiceProvider Services => Hosting.Services;

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddHttpClient<IRepository<UserInfo>, WebRepository<UserInfo>>(client => 
                client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}api/Users/"));
        }

        public static async Task Main(string[] args)
        {
            var host = Hosting;
            await host.StartAsync();

            Console.WriteLine("Старт");

            var userRep = Services.GetRequiredService<IRepository<UserInfo>>();

            try
            {
                /* var user = await userRep.Update(new UserInfo
                 {
                     Id = 1,
                     Name = "Василмй2",
                     Description = "Des1",
                     Email = "vasw@ya.ru",
                     Password = "123"
                 });

                 Console.WriteLine(user);*/

                /*  await userRep.Add(new UserInfo
                  {
                      Name = "Ivan",
                      Description = "DesIvan",
                      Email = "ivan@ya.ru",
                      Password = "1234"
                  });
                  await userRep.Add(new UserInfo
                  {
                      Name = "Sergey",
                      Description = "Desqwe",
                      Email = "ser@ya.ru",
                      Password = "12344"
                  });
                  await userRep.Add(new UserInfo
                  {
                      Name = "",
                      Description = "Best",
                      Email = "hutao@ya.ru",
                      Password = "agdrsty65wsrhts54"
                  });*/

                // var users = await userRep.GetAll();
                // var users = await userRep.Get(0, 20);
                /* foreach (var user in  users)
                 {
                     Console.WriteLine(user);
                 }*/
                //Console.WriteLine(await userRep.GetById(6));
                //Console.WriteLine(await userRep.DeleteById(3));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();

            Console.WriteLine("Стоп");
            await host.StopAsync();
        }
    }
}