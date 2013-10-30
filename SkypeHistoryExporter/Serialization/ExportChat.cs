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
    public class ExportChat
    {
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

        private string _Blob;

        [XmlAttribute("blob")]
        public string Blob
        {
            get
            {
                return _Blob;
            }
            set
            {
                _Blob = value;
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

        private string _ActivityTimestamp;

        [XmlAttribute("activitytimestamp")]
        public string ActivityTimestamp
        {
            get
            {
                return _ActivityTimestamp;
            }
            set
            {
                _ActivityTimestamp = value;
            }
        }

        private ExportMemberCollection _Members;

        [XmlArray("members")]
        [XmlArrayItem("member", typeof(ExportMember))]
        public ExportMemberCollection Members
        {
            get
            {
                if (null == _Members)
                {
                    _Members = new ExportMemberCollection();
                }

                return _Members;
            }
            set
            {
                _Members = value;
            }
        }


        private ExportMessageCollection _Messages;

        [XmlArray("messages")]
        [XmlArrayItem("message", typeof(ExportMessage))]
        public ExportMessageCollection Messages
        {
            get
            {
                if (null == _Messages)
                {
                    _Messages = new ExportMessageCollection();
                }

                return _Messages;
            }
            set
            {
                _Messages = value;
            }
        }
    }
}