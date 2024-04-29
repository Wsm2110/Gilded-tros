using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GildedTros.Cli.Contracts;
using GildedTros.Cli.Domain;
using GildedTros.Cli.Features.ItemManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GildedTros.App
{
    public class Program
    {
        #region Fields

        // Static list of items to manage in the inventory
        private static IList<Item> Items = [

                new Item {Name = "Ring of Cleansening Code", SellIn = 10, Quality = 20},
                new Item {Name = "Good Wine", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the SOLID", SellIn = 5, Quality = 7},
                new Item {Name = "B-DAWG Keychain", SellIn = 0, Quality = 80},
                new Item {Name = "B-DAWG Keychain", SellIn = -1, Quality = 80},
                new Item {Name = "Backstage passes for Re:factor", SellIn = 15, Quality = 20},
                new Item {Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 49},
                new Item {Name = "Backstage passes for HAXX", SellIn = 5, Quality = 49},
                // these smelly items do not work properly yet
                new Item {Name = "Duplicate Code", SellIn = 3, Quality = 6},
                new Item {Name = "Long Methods", SellIn = 3, Quality = 6},
                new Item {Name = "Ugly Variable Names", SellIn = 3, Quality = 6}
            ];

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ServiceProvider StartUp(ServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly)); // Startup is your application's entry point
            services.AddValidatorsFromAssemblies(new[] { typeof(Program).Assembly }, ServiceLifetime.Singleton, null, true);

            // Setup entrypoint
            services.AddSingleton<ItemUpdateFeature.EntryPoint>();

            // Define ruleset 
            services.AddSingleton<IList<IRule>>(p => p.GetServices<IRule>().ToList());

            var rules = typeof(Program).Assembly.GetTypes().Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(nameof(IRule)) == typeof(IRule));

            foreach (var rule in rules)
            {
                services.Add(new ServiceDescriptor(typeof(IRule), rule, ServiceLifetime.Singleton));
            }

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var provider = StartUp(new ServiceCollection());
            var entryPoint = provider.GetRequiredService<ItemUpdateFeature.EntryPoint>();
            var output = await entryPoint.Process(Items);
            Console.WriteLine(output);
        }

        #endregion
    }
}
