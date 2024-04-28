using GildedTros.Cli.Domain;
using System.Collections.Generic;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest : 
    {
        [Fact]
        public void ShouldGoThroughTheNormalFlow()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Ring of Cleansening Code", SellIn = 3, Quality = 4 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal("Ring of Cleansening Code", Items[0].Name);
            Assert.Equal(2, Items[0].SellIn);
            Assert.Equal(3, Items[0].Quality);

        }

        [Fact]
        public void ShouldLowerTheQualityDoubleAsFastWhenSellInisUnderZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Ring of Cleansening Code", SellIn = -1, Quality = 4 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal("Ring of Cleansening Code", Items[0].Name);
            Assert.Equal(-2, Items[0].SellIn);
            Assert.Equal(2, Items[0].Quality);

        }

        [Fact]
        public void ShouldNeverLowerTheQualityUnderZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Ring of Cleansening Code", SellIn = 0, Quality = 0 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal("Ring of Cleansening Code", Items[0].Name);
            Assert.Equal(-1, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);

        }

        [Fact]
        public void ShouldIncreaseGoodWinesQualityWhenSellInDrops()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 2, Quality = 3 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal("Good Wine", Items[0].Name);
            Assert.Equal(1, Items[0].SellIn);
            Assert.Equal(4, Items[0].Quality);

        }

        [Fact]
        public void ShouldNeverIncreaseQualityAboveFifty()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 2, Quality = 50 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal("Good Wine", Items[0].Name);
            Assert.Equal(1, Items[0].SellIn);
            Assert.Equal(50, Items[0].Quality);

        }

        [Fact]
        public void ShouldIncreaseQualityForVeryInterestingConferences()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 20, Quality = 32 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal("Backstage passes for Re:factor", Items[0].Name);
            Assert.Equal(19, Items[0].SellIn);
            Assert.Equal(33, Items[0].Quality);

        }

        [Fact]
        public void ShouldIncreaseQualityForVeryInterestingConferencesByTwoWhenSellInIsUnderOrEqualToTen()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 32 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal("Backstage passes for Re:factor", Items[0].Name);
            Assert.Equal(9, Items[0].SellIn);
            Assert.Equal(34, Items[0].Quality);

        }

        [Fact]
        public void ShouldIncreaseQualityForVeryInterestingConferencesByThreeWhenSellInIsUnderOrEqualToFive()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 5, Quality = 32 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal("Backstage passes for Re:factor", Items[0].Name);
            Assert.Equal(4, Items[0].SellIn);
            Assert.Equal(35, Items[0].Quality);

        }

        [Fact]
        public void ShouldLowerQuantityToZeroForVeryInterestingConferencesWhenSellInIsNegative()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 0, Quality = 32 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal("Backstage passes for Re:factor", Items[0].Name);
            Assert.Equal(-1, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);

        }

    }
}