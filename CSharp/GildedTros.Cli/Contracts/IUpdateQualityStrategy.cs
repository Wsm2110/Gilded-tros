using GildedTros.Cli.Domain;

namespace GildedTros.Cli.Contracts
{
    public interface IUpdateQualityStrategy
    {
        void UpdateQuality(Item item);
    }
}