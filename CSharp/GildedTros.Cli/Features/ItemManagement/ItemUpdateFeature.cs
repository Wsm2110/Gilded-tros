using FluentValidation;
using GildedTros.Cli.Domain;
using MediatR;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GildedTros.Cli.Common;
using GildedTros.Cli.Contracts;

namespace GildedTros.Cli.Features.ItemManagement
{
    public static class ItemUpdateFeature
    {
        /// <summary>
        /// This class represents the entry point for processing item updates
        /// </summary>
        /// <param name="mediator"></param>
        internal class EntryPoint(IMediator mediator)
        {
            public async Task<string> Process(IList<Item> items)
            {
                // Create a command object containing the list of items to update
                var command = new Command(items);
                var output = new StringBuilder();

                // Loop for 31 days, simulating item updates over time
                for (var i = 0; i < 31; i++)
                {
                    output.AppendLine("-------- day " + i + " --------");
                    output.AppendLine("name, sellIn, quality");

                    // Loop through each item and potentially update its properties
                    for (var j = 0; j < items.Count; j++)
                    {
                        output.AppendLine(items[j].ToString());
                    }

                    // Send the update command through the mediator pipeline
                    await mediator.Send(command);
                }

                // Return the formatted output string
                return output.ToString();
            }
        }

        /// <summary>
        /// This class defines validation rules for the Command object
        /// </summary>
        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                // Ensure the Items list is not empty or null
                RuleFor(x => x.Items).NotEmpty().NotNull();
            }
        }

        /// <summary>
        /// This record class represents the command data for updating items
        /// </summary>
        /// <param name="Items"></param>
        internal record class Command(IList<Item> Items) : IRequest<Result>;

        /// <summary>
        /// This class handles the Command object and performs item updates
        /// </summary>
        /// <param name="ruleset"></param>
        /// <param name="validator"></param>
        internal class CommandHandler(IList<IRule> ruleset, CommandValidator validator) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                // Validate the command object using the defined validator
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    // If validation fails, return an error result
                    return Error.ValidationFailed;
                }

                // Loop through each item in the command
                foreach (var item in request.Items)
                {
                    // Apply all defined rules (potentially for updating the item)
                    foreach (var rule in ruleset)
                    {
                        rule.Apply(item);
                    }
                }

                // If validation passes and updates are successful, return success result
                return Result.Success();
            }
        }
    }
}
