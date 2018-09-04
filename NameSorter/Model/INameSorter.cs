namespace NameSorter
{
    using System.Collections.Generic;

    /// <summary>
    /// Provide name sorting service.
    /// </summary>
    /// <remarks>
    /// A name sorter reads names from an instance of <c>INameSource</c>, sorts them 
    /// with a <c>ISortAlgorithm</c> instance. The sorted names then are passed to 
    /// an <c>INameDestination</c> object.
    /// </remarks>
    /// <seealso cref="NameSorter.INameSource" />
    /// <seealso cref="NameSorter.ISortAlgorithm" />
    /// <seealso cref="NameSorter.INameDestination" />
    public interface INameSorter
    {
        void Sort(INameSource source, ISortAlgorithm algorithm, INameDestination destination);
    }
}