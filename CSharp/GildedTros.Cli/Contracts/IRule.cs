using GildedTros.Cli.Domain;

namespace GildedTros.Cli.Contracts;

public interface IRule
{
    /// <summary>
    /// Applies the rule logic to a given Item object.
    /// </summary>
    /// <param name="item">The Item object to which the rule should be applied.</param>
    /// <returns>
    /// Indicates whether subsequent rules in the ruleset should be applied to the item.
    /// </returns>
    void Apply(Item item);
}
