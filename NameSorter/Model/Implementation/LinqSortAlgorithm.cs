namespace NameSorter.Model.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using NameSorter.Model;
    using com.zhusmelb.Util.Logging;

    /// <summary>
    /// Full name model
    /// </summary>
    internal class Name 
    {
        /// <summary>
        /// Construct a Name with string of full name.
        /// </summary>
        /// <param name="name">a full name</param>
        /// <exception cref="System.ArgumentException">
        /// while the <c>name</c> is empty or null
        /// </exception>
        public Name(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"\"{nameof(name)}\" cannot be empty.");

            var sections = Regex.Split(name.Trim(), @"\s+");
            // sections will never be 0 length, it has at least one element.
            LastName = sections[sections.Length-1];
            GivenName = sections.Length>1 ? string.Join(" ", sections, 0, sections.Length - 1)
                                : string.Empty;
        }
        public string LastName { get; set; }
        public string GivenName {get; set; }
    }

   public class LinqAscSortAlgorithm : ISortAlgorithm 
   {
      private readonly ISortAlgorithm _algorithm 
        = new LinqSortAlgorithm(ns => ns.OrderBy(n=>n.LastName).ThenBy(n=>n.GivenName));
      public IEnumerable<string> Sort(IEnumerable<string> names)
        =>  _algorithm.Sort(names);
   }

   public class LinqDescSortAlgorithm : ISortAlgorithm 
   {
      private readonly ISortAlgorithm _algorithm 
        = new LinqSortAlgorithm(ns => ns.OrderByDescending(n=>n.LastName)
                      .ThenByDescending(n=>n.GivenName));
      public IEnumerable<string> Sort(IEnumerable<string> names)
        =>  _algorithm.Sort(names);
   }

    /// <summary>
    /// Implement a sorting algorithm with LINQ service.
    /// </summary>
    /// <value></value>
    internal class LinqSortAlgorithm : ISortAlgorithm
    {
        private static readonly ILogger _log =
            LogHelper.GetLogger(typeof(LinqSortAlgorithm).FullName);

        private Func<IEnumerable<Name>, IEnumerable<Name>> _sortAction;
        public LinqSortAlgorithm(Func<IEnumerable<Name>, IEnumerable<Name>> sortAction) {
            if (sortAction==null)
                throw new ArgumentNullException(nameof(sortAction));
            _sortAction = sortAction;
        }

        /// <summary>
        /// Implement Sort method of ISortAlgorithm
        /// </summary>
        /// <param name="names">list of names to be sorted</param>
        /// <returns>a sorted list</returns>
        /// <remarks>
        /// <para>This implementation will ignore any lines with only white spaces and 
        /// trim the name before processing.</para>
        /// <para>A name is separated into sections by spaces, the last
        /// section of the name is Last Name while the rest are Given Names.</para>
        ///
        /// <para>If only one section exist for a name, it is treated as Last Name and
        /// the Given Names will be set to empty.</para>
        ///
        /// <para>This algorithm first sort name by its Last Name, then sort the Given
        /// Name as a whole.</para>
        /// </remarks>
        public IEnumerable<string> Sort(IEnumerable<string> names) {
            var result = names.Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n =>
                {
                    var name = new Name(n);
                    _log.Verbose("lastName: {0}, givenName: {1}", 
                        name.LastName, name.GivenName);
                    return name;
                });
            return _sortAction(result).Select(n => $"{n.GivenName} {n.LastName}");
        }
    }
}