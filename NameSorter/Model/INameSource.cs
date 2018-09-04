namespace NameSorter.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Provide a set of names.
    /// 
    /// The names are returned as IEnumerable<string>
    /// </summary>
    public interface INameSource
    {
        IEnumerable<string> GetNames();
    }
}