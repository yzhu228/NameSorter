namespace NameSorter.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Algorithm implemented to sort the names
    /// </summary>
    public interface ISortAlgorithm
    {
        /// <summary>
        /// Implementation of sorting algorithm. 
        /// </summary>
        /// <param name="names">Set of names to be sorted</param>
        /// <returns>set of names sorted by the algorithm</returns>
        IEnumerable<string> Sort(IEnumerable<string> names);
    }
}