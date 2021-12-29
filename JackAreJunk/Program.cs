using System;
using Microsoft.Extensions.DependencyInjection;

namespace JackAreJunk
{
    class Program
    {
        static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            ConfigureServices();
            var game = serviceProvider.GetService<Game>();
            game.Initialize();
            game.Play();
        }
        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<ICard, Card>();
            services.AddTransient<IGameTurn, GameTurnConsole>();
            services.AddTransient<IPlayerEntry, PlayerEntryConsole>();
            services.AddTransient<Game>();
            services.AddTransient<IGameDisplay, GameDispalyConsole>();
            serviceProvider = services.BuildServiceProvider();
        }
    }
}
