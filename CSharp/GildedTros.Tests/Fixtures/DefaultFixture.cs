using GildedTros.App;
using GildedTros.Cli.Contracts;
using GildedTros.Cli.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace GildedTros.Tests.Fixtures
{

    public class MediatRFixture : IDisposable
    {
        private bool disposedValue;

        public IMediator Mediator { get; set; }

        public MediatRFixture() => Setup();

        public void Setup()
        {
            var services = new ServiceCollection();

            // Register MediatR
            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly)); // Startup is your application's entry point


            services.AddSingleton<IList<IRule>>(p => p.GetServices<IRule>().ToList());

            var rules = typeof(Program).Assembly.GetTypes().Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(nameof(IRule)) == typeof(IRule));

            foreach (var rule in rules)
            {
                services.Add(new ServiceDescriptor(typeof(IRule), rule, ServiceLifetime.Singleton));
                // Replace Transient with whatever lifetime you need
            }

            // Register other dependencies

            var serviceProvider = services.BuildServiceProvider();
            Mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MediatRFixture()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}