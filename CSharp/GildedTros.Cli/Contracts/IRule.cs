using GildedTros.Cli.Domain;

namespace GildedTros.Cli.Contracts
{
    public interface IRule
    {  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Indicates if we need to apply the next rule or just stop</returns>
        void Apply(Item item);
    }
}