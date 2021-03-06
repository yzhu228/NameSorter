namespace NameSorter.Model.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NameSorter.Model;
    using com.zhusmelb.Util.Logging;

    public class SimpleNameSorter : INameSorter, IDisposable 
    {
        private static readonly ILogger _log = 
            LogHelper.GetLogger(typeof(SimpleNameSorter).FullName);

        public INameSource Source { get; set; }
        public ISortAlgorithm Algorithm { get; set; }
        public IEnumerable<INameDestination> Destinations { get; set; }

        public void Sort() {
            if (Source==null || Algorithm==null || Destinations==null) {
                var ex = new InvalidOperationException("Cannot perform Sort operation as dependencies not set correctly.");
                _log.Error(ex, "Source: {0}, Algorithm: {1}, Destinations: {2}", 
                    _checkNull(Source),_checkNull(Algorithm),_checkNull(Destinations)
                );
                throw ex;
            }
            _log.Info("Start sorting names ...");
            var names = Source.GetNames();
            _logNames(names, "Read names from source ...");

            var sortedNames = Algorithm.Sort(names).ToArray();
            _logNames(sortedNames, "Names sorted");

            foreach (var dest in Destinations) {
                _log.Debug("Output to destionation {0}", dest?.GetType().FullName);
                dest?.OutputNames(sortedNames);
            }
        }

        private string _checkNull(Object obj) =>
            obj==null ? "invalid" : "valid";

        private void _logNames(IEnumerable<string> names, string message) {
            _log.Info(message);
            _log.CollectionIf(LogLevel.Verbose, names, ns=>ns!=null, n=>n);
        }

#region IDisposable implementation
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                foreach(var dest in Destinations) {
                    (dest as IDisposable)?.Dispose();
                }
            }
        }
#endregion        
    }
}