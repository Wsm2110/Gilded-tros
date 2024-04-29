using System;
using System.IO;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace GildedTros.App
{
    [UseReporter(typeof(DiffReporter))]
    public class ApprovalTest
    {
        [Fact]
        // - Note that legendaries and backstage passes act differently since the refactoring.
        // - Legendary item requirement never states that the 'sellin' is static and wont change, only quality.
        // - Backstage passes while sellin is larger than 10 shouldn`t increase the quality, however this was the general rule before the refactoring
        public async Task ThirtyDays()
        {
            var fakeoutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeoutput));
            Console.SetIn(new StringReader("a\n"));

            await Program.Main(new string[] { });

            var output = fakeoutput.ToString();

            Approvals.Verify(output);
        }
    }
}
