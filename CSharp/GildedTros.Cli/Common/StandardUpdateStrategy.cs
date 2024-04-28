using GildedTros.Cli.Contracts;
using GildedTros.Cli.Domain;
using System;

namespace GildedTros.Cli.Common;

internal class StandardItemUpdateStrategy : IUpdateQualityStrategy
{
    public void UpdateQuality(Item item)
    {
        item.SellIn--;
        AdjustQuality(item, -1);
        if (item.SellIn < 0)
        {
            AdjustQuality(item, -2);
        }
    }

    private static void AdjustQuality(Item item, int qualityChange)
    {
        item.Quality = Math.Max(0, item.Quality + qualityChange);
    }
}
