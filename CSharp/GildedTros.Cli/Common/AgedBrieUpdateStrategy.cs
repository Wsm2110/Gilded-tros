using GildedTros.Cli.Contracts;
using GildedTros.Cli.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.Cli.Common
{
    public class AgedBrieualityStrategy : IUpdateQualityStrategy
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn--;
           // AdjustQuality(item, 1);
            if (item.SellIn < 0)
            {
             //   AdjustQuality(item, 2);
            }
        }
    }
}