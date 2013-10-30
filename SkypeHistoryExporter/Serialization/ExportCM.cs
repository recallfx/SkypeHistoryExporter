using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

/*
The MIT License (MIT)

Copyright (c) 2010 Marius Bieliauskas

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

namespace SkypeHistoryExporter.Serialization
{
    /// <summary>
    /// Xml serializer wrapper. A helper class for initializing objects from XML files.
    /// </summary>
    public class ExportCM
    {
        /// <summary>
        /// The current type doesn't not support instantiating.
        /// </summary>
        private ExportCM()
        {
        }

        public static readonly string ConfigurationFile = "skypeExport.xml";
        public static readonly string Version = "1.00";

        private static ExportConfiguration _Configuration;

        public static ExportConfiguration Configuration
        {
            get
            {
                if (_Configuration == null)
                {
                    New();
                }
                
                return _Configuration;
            }
        }

        public static void New()
        {
            _Configuration = new ExportConfiguration();
            _Configuration.Version = Version;
        }

        public static void Save()
        {
            string xml = string.Empty;

            if (_Configuration != null)
            {
                xml = ExportCM.SerializeToXml(_Configuration);
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            xdoc.Save(ConfigurationFile);

            xml = string.Empty;
            xdoc = null;
        }


        /// <summary>
        /// Loads the specified type from the specified stream.
        /// </summary>
        /// <param name="stream">Stream object containg the XML declaration of the type.</param>
        /// <param name="type">Type that should be instantiated and initialized.</param>
        /// <returns>Returns the initialized object of the specified type.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        private static object LoadConfiguration(Stream stream, Type type)
        {
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            XmlSerializer serializer = new XmlSerializer(type);

            try
            {
                return serializer.Deserialize(stream);
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to deserialize the stream to the \"" + type.ToString() + "\" type.", exception);
            }
        }

        /// <summary>
        /// Loads the specified type from the specified XML file.
        /// </summary>
        /// <param name="configurationFile">Configuration file containing XML declaration of the type.</param>
        /// <param name="type">Type that should be instantiated and initialized.</param>
        /// <returns>Returns the initialized object of the specified type.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        public static void Load()
        {
            string configurationFilePath = ConfigurationFile;
            //DebugFileAccess(configurationFilePath);

            if (!File.Exists(configurationFilePath))
            {
                throw new Exception(string.Format("Configuration file ({0}) cannot be found. Either it doesn't exist or access is denied.", ConfigurationFile));
            }

            FileStream fs = null;
            try
            {
                fs = new FileStream(configurationFilePath, FileMode.Open, FileAccess.Read);
                _Configuration = (ExportConfiguration)LoadConfiguration(fs, typeof(ExportConfiguration));
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// Deserializes the specified type from the xml string.
        /// </summary>
        /// <param name="xml">The serialized xml string.</param>
        /// <param name="type">The type to be deserialized.</param>
        /// <returns>Returns the deserialized type from the specified xml string.</returns>
        private static object DeserializeFromXml(string xml, Type type)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException("xml");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(reader);
            }
        }

        private static string SerializeToXml(object o)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }

            XmlSerializer serializer = new XmlSerializer(o.GetType());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, o);
            }

            return sb.ToString();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private static void DebugFileAccess(string file)
        {
            string threadPrincipalName = null;
            string windowsPrincipalName = null;

            System.Security.Principal.IIdentity identity = null;
            System.Security.Principal.IPrincipal principal = null;


            principal = System.Threading.Thread.CurrentPrincipal;
            if (principal != null)
            {
                identity = principal.Identity;
                if (identity != null)
                {
                    threadPrincipalName = identity.Name;
                }
            }

            identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                windowsPrincipalName = identity.Name;
            }

            System.Diagnostics.Debug.WriteLine(string.Format("Accessing file '{0}', thread user: '{1}', windows user: '{2}'.", file, threadPrincipalName, windowsPrincipalName));
        }
    }
}
