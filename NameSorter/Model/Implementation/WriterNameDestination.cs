namespace NameSorter.Model.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using NameSorter.Model;
    using com.zhusmelb.Util.Logging;

    /// <summary>
    /// Allow sorted name list be output into a TextWriter
    /// </summary>
    /// <remarks>
    /// This class own the TextWriter passed in, make sure you dispose it
    /// after use.
    /// </remarks>
    public class WriterNameDestination : INameDestination, IDisposable
    {
        private static readonly ILogger _log = 
            LogHelper.GetLogger(typeof(WriterNameDestination).FullName);

        protected TextWriter Writer { get; set; }

        public WriterNameDestination() {}
        
        public WriterNameDestination(TextWriter writer) {
            if (writer==null)
                throw new ArgumentNullException(nameof(writer));

            _log.Info("A write name destination created");
            Writer = writer;
        }
    
        public virtual void OutputNames(IEnumerable<string> names) {
            try {
                _log.Verbose("To output names to writer");
                foreach (var n in names) {
                    _log.Verbose(n);
                    Writer.WriteLine(n);
                }
                _log.Verbose("Finish output names.");
            }
            catch (ObjectDisposedException e) {
                _log.Error(e, "Writer is closed.");
                throw;
            }
            catch (IOException e) {
                _log.Error(e, "Unexpected error while operating on writer");
                throw;
            }
        }

#region IDisposable implementation
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (disposing) {
            Writer?.Close();
            Writer = null;
        }
    }
#endregion        
    }
}