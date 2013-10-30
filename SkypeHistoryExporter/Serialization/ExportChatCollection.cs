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
    public class ExportChatCollection : Collection<ExportChat>
    {

        public bool ChatExists(string name)
        {
            if (GetChat(name) != null)
                return true;

            return false;
        }

        public ExportChat GetChat(string name)
        {
            foreach (ExportChat chat in this)
            {
                if (chat.Name == name)
                    return chat;
            }

            return null;
        }

        public bool ChatExists(ExportMemberCollection exportMembers)
        {
            if (GetChat(exportMembers) != null)
                return true;

            return false;
        }

        public ExportChat GetChat(ExportMemberCollection exportMembers)
        {
            foreach (ExportChat chat in this)
            {
                bool match = true;

                if (chat.Members.Count == exportMembers.Count)
                {
                    foreach (ExportMember exportMember in exportMembers)
                    {
                        if (!chat.Members.MemberExists(exportMember.Name))
                        {
                            match = false;
                            break;
                        }

                    }

                    if (match)
                    {
                        foreach (ExportMember member in chat.Members)
                        {
                            if (!exportMembers.MemberExists(member.Name))
                            {
                                match = false;
                                break;
                            }
                        }
                    }

                    if (match)
                        return chat;
                }
            }

            return null;
        }
    }
}
