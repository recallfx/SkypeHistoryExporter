using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

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
    public class ExportMessage
    {
        private string _Id;

        [XmlAttribute("id")]
        public string Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        private string _From;

        [XmlAttribute("from")]
        public string From
        {
            get
            {
                return _From;
            }
            set
            {
                _From = value;
            }
        }

        private string _Timestamp;

        [XmlAttribute("timestamp")]
        public string Timestamp
        {
            get
            {
                return _Timestamp;
            }
            set
            {
                _Timestamp = value;
            }
        }

        private string _EditedTimestamp;

        [XmlAttribute("editedtimestamp")]
        public string EditedTimestamp
        {
            get
            {
                return _EditedTimestamp;
            }
            set
            {
                _EditedTimestamp = value;
            }
        }

        private string _EditBy;

        [XmlAttribute("editby")]
        public string EditBy
        {
            get
            {
                return _EditBy;
            }
            set
            {
                _EditBy = value;
            }
        }

        private string _Name;

        [XmlAttribute("name")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private string _MessageType;

        [XmlAttribute("messageType")]
        public string MessageType
        {
            get
            {
                return _MessageType;
            }
            set
            {
                _MessageType = value;
            }
        }

        private string _Value;

        [XmlText()]
        public string Body
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        [XmlIgnore()]
        public string Display
        {
            get
            {
                return string.Format("[{0} {1}] {2}: {3}", Id, Timestamp, From, Body);
            }
        }
    }
}
