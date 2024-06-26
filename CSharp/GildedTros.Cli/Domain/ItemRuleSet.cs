﻿using GildedTros.Cli.Contracts;
using System;

namespace GildedTros.Cli.Domain
{
    /// <summary>
    /// Contains various rules for updating item quality and sell-in values.   
    /// Note. the last rules defined are "QualityNeverExceedsFiftyRule" "QualityIsNeverNegativeRule" "QualityLegendariesStaysTheSameRule"
    /// These rules should never change
    /// </summary>
    internal class ItemRuleSet
    {
        /// <summary>
        ///  Implements a rule for updating quality of "Backstage passes" items.     
        /// </summary>
        public class BackstageItemsForVeryInterestingConferencesRule : IRule
        {
            /// <summary>
            /// This rule increases the quality of "Backstage passes" items as their sell-in value approaches.
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public void Apply(Item item)
            {
                if (!item.Name.Contains("Backstage passes"))
                {
                    return;
                }

                // Keep in mind the default rule which will always decrease the quality by 1. 
                if (item.SellIn <= 0)
                {
                    // Quality drops to 0 after the conference
                    item.Quality = 0;
                }

                if (item.SellIn > 0 && item.SellIn <= 5)
                {
                    // quality increases 3 when there are 5 days
                    item.Quality += 4;
                }

                if (item.SellIn > 5 && item.SellIn <= 10)
                {
                    // Quality increases by 2
                    item.Quality += 3;
                }
            }
        }

        /// <summary>
        /// Implements a rule for decreasing the sell-in value of items.
        /// </summary>
        public class DecreaseSellinRule : IRule
        {
            /// <summary>
            ///  This rule decreases the sell-in value of items by 1 each day.
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public void Apply(Item item)
            {
                item.SellIn--;
            }
        }

        /// <summary>
        ///  Implements a rule for decreasing item quality as time passes.        
        /// </summary>
        public class QualityDecreasesAsTimePassesRule : IRule
        {
            /// <summary>
            /// Comments: This rule decreases the quality of items by 1 each day.
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public void Apply(Item item)
            {
                item.Quality--;
            }
        }

        /// <summary>
        /// Implements a rule for degrading item quality twice as fast after sell-by date.
        /// </summary>
        public class QualityDegradesTwiceAsFastRule : IRule
        {
            /// <summary>
            /// This rule degrades the quality of items by 2 each day after the sell-by date has passed.
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public void Apply(Item item)
            {
                if (item.SellIn < 0)
                {
                    item.Quality--;
                }
            }
        }

        /// <summary>
        ///   Implements a rule for increasing quality of "Good Wine" as it gets older.
        /// </summary>
        public class QualityOfGoodWineIncreasesWhileItGetsOlder : IRule
        {
            /// <summary>
            /// This rule increases the quality of "Good Wine" items by 2 each day.
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public void Apply(Item item)
            {
                if (item.Name == "Good Wine")
                {
                    // In order to negate the (decrease of quality rule), we have to add 2 instead of 1
                    item.Quality += 2;
                }
            }
        }

        /// <summary>
        /// Implements a rule for degrading quality of smelly items twice as fast.
        /// </summary>
        public class QualityOfSmellyItemsDegradeTwiceAsFastRule : IRule
        {
            /// <summary>
            ///This rule degrades the quality of smelly items ("Duplicate Code", "Long Methods", "Ugly Variable Names") by 1 each day.
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public void Apply(Item item)
            {
                if (item.Name is "Duplicate Code" or "Long Methods" or "Ugly Variable Names")
                {
                    item.Quality -= 1;
                }

                return;
            }
        }

        /// <summary>
        ///  Implements a rule which states that quality never exceeds fifty
        /// </summary>        
        public class QualityNeverExceedsFiftyRule : IRule
        {
            /// <summary>
            /// This rule resets the quality to 50 whenever it exceeds
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public void Apply(Item item)
            {
                item.Quality = Math.Min(item.Quality, 50);
            }
        }

        /// <summary>
        /// Implements a rule which states that quality can never be lower than 0
        /// </summary>
        public class QualityIsNeverNegativeRule : IRule
        {
            /// <summary>
            /// This rule resets the quality to 0 whenever it decreases beyond 0
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            public void Apply(Item item)
            {
                item.Quality = Math.Max(item.Quality, 0);
            }
        }

        /// <summary>
        /// Implements a rule which states that legendary items always have quality of 80
        /// </summary>
        public class QualityLegendariesStaysTheSameRule : IRule
        {
            /// <summary>
            /// Applies the rule to only Legendary items, will break the for-loop, since no other rules are applicable
            /// </summary>
            /// <param name="item"></param>
            /// <returns>Cancel for loop when return value is false </returns>
            public void Apply(Item item)
            {
                if (item.Name == "B-DAWG Keychain")
                {                              
                    item.Quality = 80;
                }
            }
        }
    }
}