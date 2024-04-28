using GildedTros.Cli.Domain;

namespace GildedTros.Cli.Contracts
{
    public interface IRule
    {

        public byte Order { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Indicates if we need to apply the next rule or just stop</returns>
        bool Apply(Item item);
    }
}