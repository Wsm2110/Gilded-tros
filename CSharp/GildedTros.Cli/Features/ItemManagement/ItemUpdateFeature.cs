using FluentValidation;
using GildedTros.Cli.Common;
using GildedTros.Cli.Contracts;
using GildedTros.Cli.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GildedTros.Cli.Features.ItemManagement
{
    public static class ItemUpdateFeature
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackStage = "Backstage passes to a TAFKAL80ETC concert";

        internal class EntryPoint(IMediator mediator)
        {
            public async Task<string> Process(IList<Item> items)
            {                
                var command = new Command(items);
                var output = new StringBuilder();

                for (var i = 0; i < 31; i++)
                {
                    output.Append("-------- day " + i + " --------");
                    output.Append("name, sellIn, quality");

                    for (var j = 0; j < items.Count; j++)
                    {
                        output.Append(items[j]);
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

        internal class CommandHandler : IRequestHandler<Command>
        {
            public Task Handle(Command request, CancellationToken cancellationToken)
            {
                var validator = new CommandValidator();
                if (!validator.Validate(request).IsValid)
                {
                    return default;
                }

                foreach (var item in request.Items)
                {
                    IUpdateQualityStrategy strategy = item.Name switch
                    {
                        AgedBrie => new AgedBrieualityStrategy(),
                        BackStage => new BackstagePassUpdateStrategy(),
                        //default
                        _ => new StandardItemUpdateStrategy(),
                    };

                    strategy.UpdateQuality(item);
                }

                return Task.CompletedTask;
            }
        }
    }
}
