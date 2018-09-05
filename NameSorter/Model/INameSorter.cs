namespace NameSorter.Model
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
    /// <seealso cref="NameSorter.Model.INameSource" />
    /// <seealso cref="NameSorter.Model.ISortAlgorithm" />
    /// <seealso cref="NameSorter.Model.INameDestination" />
    public interface INameSorter
    {
        INameSource Source { get; set; }
        ISortAlgorithm Algorithm { get; set; }
        IEnumerable<INameDestination> Destinations { get; set; }
        void Sort();
    }
}