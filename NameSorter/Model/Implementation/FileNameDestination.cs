namespace NameSorter.Model.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using NameSorter.Model;
    using com.zhusmelb.Util.Logging;

    public class FileNameDestination : WriterNameDestination
    {
        private static readonly ILogger _log = 
            LogHelper.GetLogger(typeof(FileNameDestination).FullName);

        private string _fileName;

        public FileNameDestination(string fileName) : base() {
            if (fileName==null)
                throw new ArgumentNullException(nameof(fileName));
            if (!string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException($"{nameof(fileName)} cannot be empty.");
            _log.Info("New Name destination created with file {0}", _fileName);
            _fileName = fileName;

            try {
                Writer = new StreamWriter(_fileName);
            }
            catch (IOException e) when (e is FileNotFoundException 
                                     || e is DirectoryNotFoundException) {
                _log.Error(e, "Specified file {0} does not exist", _fileName);
                throw;
            }
            catch (IOException e) {
                _log.Error(e, "Unexpected error while operating on {0}", _fileName);
                throw;
            }

        }
    }
}