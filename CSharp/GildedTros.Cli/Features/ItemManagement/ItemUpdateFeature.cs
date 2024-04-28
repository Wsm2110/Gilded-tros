using FluentValidation;
using GildedTros.Cli.Domain;
using GildedTros.Cli.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GildedTros.Cli.Features.ItemManagement
{
    public static class ItemUpdateFeature
    {
        internal class EntryPoint(IMediator mediator)
        {
            public async Task<string> Process(IList<Item> items)
            {
                var command = new Command(items);
                var output = new StringBuilder();

                for (var i = 0; i < 31; i++)
                {
                    output.AppendLine("-------- day " + i + " --------");
                    output.AppendLine("name, sellIn, quality");

                    for (var j = 0; j < items.Count; j++)
                    {
                        output.AppendLine(items[j].ToString());
                    }

                    await mediator.Send(command);
                }

                return output.ToString();
            }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Items).NotEmpty();
                RuleFor(x => x.Items).NotNull();
            }
        }

        internal record class Command(IList<Item> Items) : IRequest;

        internal class CommandHandler(IList<IRule> ruleset) : IRequestHandler<Command>
        {
            public Task Handle(Command request, CancellationToken cancellationToken)
            {
                var validator = new CommandValidator();
                if (!validator.Validate(request).IsValid)
                {
                    //TODO impl Resu
                    return default;
                }
                      
                foreach (var item in request.Items)
                {
                    foreach (var rule in ruleset)
                    {
                        rule.Apply(item);
                    }
                }

                return Task.CompletedTask;
            }
        }
    }
}
