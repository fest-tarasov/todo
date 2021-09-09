using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Test_IBS.Interfaces;
using Test_IBS.Models;

namespace Test_IBS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateDefaultBuilder(args).Build();
            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;
            var coreService = services.GetRequiredService<ICoreService>();
            await coreService.StartAsync();
        }

        static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.None);
                })
                .ConfigureServices((_, services) =>
                    services.AddSingleton<ICoreService, CoreService>()
                        .AddSingleton<IUserService, UserService>()
                        .AddSingleton<ITodoService, TodoService>()
                        .AddSingleton<IPostService, PostService>()
                        .AddSingleton<IDataRepository<User>, UserRepository>()
                        .AddSingleton<IDataRepository<Todo>, TodoRepository>()
                        .AddSingleton<IDataRepository<Post>, PostRepository>()
                        .AddHttpClient()
                        .AddLogging());
        }
    }
}
