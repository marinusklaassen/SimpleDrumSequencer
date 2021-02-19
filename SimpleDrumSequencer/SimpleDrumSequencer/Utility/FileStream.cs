using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SimpleDrumSequencer.Utility
{
    public class FileLocator
    {
        public static Stream GetFileStreamFromAssembly(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(filename);
            if (stream == null)
                throw new InvalidOperationException($"File not found {filename}");
            return stream;
        }
    }
}
