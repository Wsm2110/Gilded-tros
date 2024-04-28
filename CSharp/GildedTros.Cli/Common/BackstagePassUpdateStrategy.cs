using GildedTros.Cli.Contracts;
using GildedTros.Cli.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.Cli.Common
{
    internal class BackstagePassUpdateStrategy : IUpdateQualityStrategy
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn--;
            if (item.SellIn < 0)
            {
                item.Quality = 0;
                return;
            }

            int qualityIncrease = 1;
            if (item.SellIn < 10)
            {
                qualityIncrease = 2;
            }
            if (item.SellIn < 5)
            {
                qualityIncrease = 3;
            }
            //AdjustQuality(item, qualityIncrease);

        }
    }
}
