using GildedTros.Cli.Domain;
using static GildedTros.Cli.Domain.ItemRuleSet;

namespace GildedTros.Tests
{
    public class ItemRulesetTests
    {
        [Fact]
        public void AssertLastDefinedRulesInRulesetAreLegendaryNegativeAndMax()
        {
            // Assign & Act            
            var types = typeof(ItemRuleSet).GetNestedTypes().ToArray();

            // Assert
            Assert.Equal(typeof(QualityNeverExceedsFiftyRule), types[types.Length - 3]);
            Assert.Equal(typeof(QualityIsNeverNegativeRule), types[types.Length - 2]);
            Assert.Equal(typeof(QualityLegendariesStaysTheSameRule), types[types.Length -1]);
        }
    }
}
