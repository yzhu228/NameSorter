namespace NameSorter.Model
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

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
    public interface INameSorter : IDisposable
    {
        INameSource Source { get; set; }
        ISortAlgorithm Algorithm { get; set; }
        IEnumerable<INameDestination> Destinations { get; set; }
        void Sort();
    }

    public interface INameSorterAsync : INameSorter
    {
        Task SortAsync();
        Task SortAsync(CancellationToken token);
        Task SortAsync<T>(CancellationToken toke, IProgress<T> progress);
    }
}