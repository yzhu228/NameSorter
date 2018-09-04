namespace NameSorter.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Service to process a set of names.
    /// 
    /// A name destination should not change the order and value of the set
    /// of names passed in.
    /// </summary>
    public interface INameDestination
    {
        void OutputNames(IEnumerable<string> names);
    }
}