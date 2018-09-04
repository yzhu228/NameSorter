namespace NameSorter.Model.Implementation
{
    using System;
    using System.Collections.Generic;
 
    using NameSorter.Model;
    using com.zhusmelb.Util.Logging;

    public class ConsoleNameDestination : WriterNameDestination
    {
        public ConsoleNameDestination() : base(Console.Out) {}

        protected override void Dispose(bool disposing) {
            // we don't want to call Close method on
            // Console.Out writer.
        }
    }
}