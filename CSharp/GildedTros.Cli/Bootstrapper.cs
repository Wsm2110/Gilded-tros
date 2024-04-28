using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Stashbox.Configuration;
using Stashbox;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.ComponentModel;


namespace GildedTros.Cli
{

    public class WrappingWriter : TextWriter
    {


        public override Encoding Encoding => Encoding.UTF8;
    }

    internal class Bootstrapper
    {
        public IMediator Mediator { get; set; }

        public StashboxContainer Container { get; set; } = new StashboxContainer();

        public void StartUp()
        { 
            Mediator = BuildMediator(Container, new WrappingWriter());
        }

        private static IMediator BuildMediator(StashboxContainer container, WrappingWriter writer)
        {
            container.RegisterInstance<TextWriter>(writer)
                     .RegisterAssemblies(new[] { typeof(Mediator).Assembly, typeof(Bootstrapper).Assembly },
                     serviceTypeSelector: Rules.ServiceRegistrationFilters.Interfaces, registerSelf: false);

            return container.GetRequiredService<IMediator>();
        }
    }
}
