namespace NameSorter.Model.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using NameSorter.Model;
    using com.zhusmelb.Util.Logging;

    public class Name 
    {
        public string LastName { get; set; }
        public string GivenName {get; set; }
    }

    /// <summary>
    /// Implement a sorting algorithm with LINQ service.
    /// </summary>
    /// <value></value>
    public class LinqSortAlgorithm : ISortAlgorithm
    {
        private static readonly ILogger _log =
            LogHelper.GetLogger(typeof(LinqSortAlgorithm).FullName);

        private Func<IEnumerable<Name>, IEnumerable<Name>> _sortAction;
        public LinqSortAlgorithm() {}
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
                    var sections = Regex.Split(n.Trim(), @"\s+");
                    // sections will never be 0 length, it has at least one element.
                    var lastName = sections[sections.Length-1];
                    var givenName = sections.Length>1 ? string.Join(" ", sections, 0, sections.Length - 1)
                                        : string.Empty;
                    _log.Verbose("lastName: {0}, givenName: {1}", lastName, givenName);
                    return new Name
                    {
                        LastName = lastName,
                        GivenName = givenName
                    };
                });
            return _sortAction(result).Select(n => $"{n.GivenName} {n.LastName}");
        }
    }
}