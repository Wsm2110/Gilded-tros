using GildedTros.Cli.Domain;
using GildedTros.Cli.Features.ItemManagement;
using GildedTros.Tests.Fixtures;

namespace GildedTros.App
{
    public class GildedTrosTest : IClassFixture<MediatRFixture>
    {
        MediatRFixture _fixture;

        public GildedTrosTest(MediatRFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldGoThroughTheNormalFlow()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Ring of Cleansening Code", SellIn = 3, Quality = 4 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Ring of Cleansening Code", items[0].Name);
            Assert.Equal(2, items[0].SellIn);
            Assert.Equal(3, items[0].Quality);
        }

        [Fact]
        public async Task ShouldLowerTheQualityDoubleAsFastWhenSellInisUnderZero()
        {
            // Arrange 
            var items = new List<Item> { new Item { Name = "Ring of Cleansening Code", SellIn = -1, Quality = 4 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Ring of Cleansening Code", items[0].Name);
            Assert.Equal(-2, items[0].SellIn);
            Assert.Equal(2, items[0].Quality);
        }

        [Fact]
        public async Task ShouldNeverLowerTheQualityUnderZero()
        {
            // Arrage
            var items = new List<Item> { new Item { Name = "Ring of Cleansening Code", SellIn = 0, Quality = 0 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Ring of Cleansening Code", items[0].Name);
            Assert.Equal(-1, items[0].SellIn);
            Assert.Equal(0, items[0].Quality);
        }

        [Fact]
        public async Task ShouldNeverIncreaseQualityAbove50()
        {
            // Arrage
            var items = new List<Item> { new Item { Name = "Ring of Cleansening Code", SellIn = 50, Quality = 100 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Ring of Cleansening Code", items[0].Name);  
            Assert.Equal(50, items[0].Quality);
        }

        [Fact]
        public async Task ShouldIncreaseGoodWinesQualityWhenSellInDrops()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Good Wine", SellIn = 2, Quality = 3 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Good Wine", items[0].Name);
            Assert.Equal(1, items[0].SellIn);
            Assert.Equal(4, items[0].Quality);
        }

        [Fact]
        public async Task ShouldNeverIncreaseQualityAboveFifty()
        {
            // Assign
            var items = new List<Item> { new Item { Name = "Good Wine", SellIn = 2, Quality = 60 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Good Wine", items[0].Name);
            Assert.Equal(1, items[0].SellIn);
            Assert.Equal(50, items[0].Quality);
        }

        [Fact]
        public async Task ShoulQualityDecreasesForVeryInterestingConferenceswhileSellinIsAboveTen()
        {
            // Assign
            var items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 20, Quality = 32 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Backstage passes for Re:factor", items[0].Name);
            Assert.Equal(19, items[0].SellIn);
            Assert.Equal(31, items[0].Quality);
        }

        [Fact]
        public async Task ShouldIncreaseQualityForVeryInterestingConferencesByTwoWhenSellInIsUnderOrEqualToTen()
        {
            // Assign
            var items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 32 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Backstage passes for Re:factor", items[0].Name);
            Assert.Equal(9, items[0].SellIn);
            Assert.Equal(34, items[0].Quality);
        }

        [Fact]
        public async Task ShouldIncreaseQualityForVeryInterestingConferencesByThreeWhenSellInIsUnderOrEqualToFive()
        {
            // Assign
            var items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 5, Quality = 32 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Backstage passes for Re:factor", items[0].Name);
            Assert.Equal(4, items[0].SellIn);
            Assert.Equal(35, items[0].Quality);
        }

        [Fact]
        public async Task ShouldLowerQuantityToZeroForVeryInterestingConferencesWhenSellInIsNegative()
        {
            // Assign
            var items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 0, Quality = 32 } };
            var command = new ItemUpdateFeature.Command(items);

            // Act
            await _fixture.Mediator.Send(command);

            // Assert
            Assert.Equal("Backstage passes for Re:factor", items[0].Name);
            Assert.Equal(-1, items[0].SellIn);
            Assert.Equal(0, items[0].Quality);
        }
    }
}