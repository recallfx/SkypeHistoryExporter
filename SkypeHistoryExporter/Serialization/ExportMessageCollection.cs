using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

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
    public class ExportMessageCollection : Collection<ExportMessage>
    {
        public bool MessageExists(ExportMessage exportMessage)
        {
            foreach (ExportMessage message in this)
            {
                if ((message.Timestamp == exportMessage.Timestamp) && (message.From == exportMessage.From))
                {
                    return true;
                }
            }

            return false;
        }

        public bool MessageExists(string id)
        {
            if (FindMessage(id) != null)
                return true;
            return false;
        }

        public ExportMessage FindMessage(string id)
        {
            foreach (ExportMessage message in this)
            {
                if (message.Id == id)
                    return message;
            }

            return null;
        }
    }
}
