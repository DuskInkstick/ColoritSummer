using ColoritSummer.Interfaces.Auth;
using ColoritSummer.Interfaces.Repositories;
using ColoritSummer.Models.DTO;
using ColoritSummer.Models.Entities;
using ColoritSummery.WebAPIClient.Auth;
using ColoritSummery.WebAPIClient.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddHttpClient<IAuthClient<LoginInfo, RegistrationInfo>,
                AuthClient<LoginInfo, RegistrationInfo>>(client =>
                {
                    client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}api/Auth/");
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
                });
        }
    

        public static async Task Main(string[] args)
        {
            var host = Hosting;
            await host.StartAsync();

            Console.WriteLine("Старт");

            var userRep = Services.GetRequiredService<IRepository<UserInfo>>();
            var authClient = Services.GetRequiredService<IAuthClient<LoginInfo, RegistrationInfo>>();
            
            try
            {
                #region Тестирование репозитория
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
                #endregion

                #region Тестирование авторизации и регистрации

                /*var res = await authClient.Registration(new RegistrationInfo
                {
                    City = "Mond",
                    OrganizationName = "Ordo Favonius",
                    Login = "Jien",
                    Password = "123"
                });*/

                var token = await authClient.Login(new LoginInfo
                {
                    Login = "Jien",
                    Password = "123"
                });


                Console.WriteLine(token);
                Console.WriteLine();
                ((IAuthorizationRequired)userRep).SetAccesToken(token);
                Console.WriteLine(await userRep.GetCount());
                userRep = Services.GetRequiredService<IRepository<UserInfo>>();
                Console.WriteLine(await userRep.GetCount());
                #endregion
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