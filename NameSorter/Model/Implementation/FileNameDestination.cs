namespace NameSorter.Model.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using NameSorter.Model;
    using com.zhusmelb.Util.Logging;

    /// <summary>
    /// Use a named file as the destination.
    /// </summary>
    /// <remarks>
    /// Remember to call Dispose after use to release the file resource.
    /// </remarks>
    public class FileNameDestination : WriterNameDestination
    {
        private static readonly ILogger _log = 
            LogHelper.GetLogger(typeof(FileNameDestination).FullName);

        public FileNameDestination(string fileName) : base() {
            if (fileName==null)
                throw new ArgumentNullException(nameof(fileName));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException($"{nameof(fileName)} cannot be empty.");

            _log.Info("New destination created with file {0}", fileName);

            try {
                Writer = new StreamWriter(fileName);
            }
            catch (UnauthorizedAccessException e) {
                _log.Error(e, "Access denined.");
                throw;
            }
            catch (IOException e) when (e is PathTooLongException 
                                     || e is DirectoryNotFoundException) {
                _log.Error(e, "Specified file {0} is not valid", fileName);
                throw;
            }
            catch (IOException e) {
                _log.Error(e, "Unexpected error while operating on {0}", fileName);
                throw;
            }
        }
    }
}