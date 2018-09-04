namespace NameSorter.Model.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using NameSorter.Model;
    using com.zhusmelb.Util.Logging;

    /// <summary>
    /// A NameSource from a text file.
    /// </summary>
    public class FileNameSource : INameSource 
    {
        private static readonly ILogger _log = 
            LogHelper.GetLogger(typeof(FileNameSource).FullName);

        private string _fileName;

        public FileNameSource(string fileName) {
            if (fileName==null)
                throw new ArgumentNullException(nameof(fileName));
            if (!string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException($"{nameof(fileName)} cannot be empty.");
            _log.Info("New source created with file {0}", _fileName);
            _fileName = fileName;
        }

        public IEnumerable<string> GetNames() {
            try{
                var names = new List<string>();
                using (var sr = new StreamReader(_fileName)) 
                {
                    string line;
                    _log.Verbose("Start read names from {0}", _fileName);
                    while ((line = sr.ReadLine()) != null) 
                    {
                        _log.Verbose(line);
                        names.Add(line);
                    }
                    _log.Verbose("End read names from {0}.", _fileName);
                }
                return names;
            }
            catch (OutOfMemoryException e) {
                _log.Error(e, "Out of memory while reading from {0}", _fileName);
                throw;
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