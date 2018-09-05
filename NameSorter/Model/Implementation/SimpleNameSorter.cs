namespace NameSorter.Model.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using NameSorter.Model;
    using com.zhusmelb.Util.Logging;

    public class SimpleNameSorter : INameSorter 
    {
        private static readonly ILogger _log = 
            LogHelper.GetLogger(typeof(SimpleNameSorter).FullName);

        public void Sort(INameSource source, ISortAlgorithm algorithm, IEnumerable<INameDestination> destinations) {
            if (source==null)
                throw new ArgumentNullException(nameof(source));
            if (algorithm==null)
                throw new ArgumentNullException(nameof(algorithm));
            if (destinations==null)
                throw new ArgumentNullException(nameof(destinations));

            _log.Info("Start sorting names ...");

            var names = source.GetNames();
            _logNames(names, "Read names from source ...");

            var sortedNames = algorithm.Sort(names);
            _logNames(sortedNames, "Names sorted");

            foreach (var dest in destinations) {
                dest.OutputNames(sortedNames);
            }
        }

        private void _logNames(IEnumerable<string> names, string message) {
            _log.Info(message);
            _log.CollectionIf(LogLevel.Verbose, names, ns=>ns!=null, n=>n);

        }
    }
}