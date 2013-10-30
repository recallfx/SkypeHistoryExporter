using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SkypeHistoryExporter.Serialization;
using System.Threading;

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


namespace SkypeHistoryExporter
{
    public partial class Window : Form
    {
        public Window()
        {
            InitializeComponent();

            _job = new ThreadStart(this.FastLoad);
            _thread = new Thread(_job);

            this.FormClosing += new FormClosingEventHandler(Window_FormClosing);

        }

        void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closing = true;
            Thread.Sleep(500);
        }

        private ThreadStart _job = null;
        private Thread _thread = null;
        private bool _closing = false;

        private SKYPE4COMLib.SkypeClass _skype = null;
        public SKYPE4COMLib.SkypeClass Skype
        {
            get
            {
                if (_skype == null)
                {
                    _skype = new SKYPE4COMLib.SkypeClass();
                }

                // Open skype, if it is not running
                if (_skype != null && !_skype.Client.IsRunning)
                {
                    _skype.Client.Start(true, true);

                    while (!_skype.Client.IsRunning)
                    {
                        System.Threading.Thread.Sleep(2000);
                    }
                }

                return _skype;
            }
        }

        private string MainText
        {
            set
            {
                if (this.lstErrors.InvokeRequired)
                {
                    // It's on a different thread, so use Invoke.
                    SetTextCallback d = new SetTextCallback(SetMainText);
                    this.Invoke(d, new object[] { value });
                }
                else
                {
                    // It's on the same thread, no need for Invoke
                    this.lstErrors.Items.Add(value);
                }
            }
        }

        private string ErrorText
        {
            set
            {
                if (this.lstErrors.InvokeRequired)
                {
                    // It's on a different thread, so use Invoke.
                    SetTextCallback d = new SetTextCallback(SetErrorText);
                    this.Invoke(d, new object[] { value });
                }
                else
                {
                    // It's on the same thread, no need for Invoke
                    this.lstErrors.Items.Add(value);
                }
            }
        }

        private string LabelText
        {
            set
            {
                if (this.lstErrors.InvokeRequired)
                {
                    // It's on a different thread, so use Invoke.
                    SetTextCallback d = new SetTextCallback(SetLabel);
                    this.Invoke(d, new object[] { value });
                }
                else
                {
                    // It's on the same thread, no need for Invoke
                    this.lstErrors.Items.Add(value);
                }
            }
        }


        delegate void SetTextCallback(string text);

        private void SetErrorText(string text)
        {
            lstErrors.Items.Add(text);
        }

        private void SetMainText(string text)
        {
            //lstBox.Items.Add(text);

            lstBox.DataSource = null;
            lstBox.DataSource = ExportCM.Configuration.Chats;
            lstBox.DisplayMember = "Name";
        }

        private void SetLabel(string text)
        {
            lblStatus.Text = text;
        }

        public void Init()
        {
            try
            {
                if (Skype != null)
                {
                    MainText = "Found " + Skype.Chats.Count + " chat groups.";
                }
                else
                {
                    ErrorText = "Unable to Initialize Skype Connectivity";
                }
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorText = "Unable to Initialize Skype Connectivity";
            }
        }

        public void LoadChats()
        {
            int count = Skype.Chats.Count;
            int i = 0;

            //Iterate chats
            foreach (SKYPE4COMLib.IChat chat in Skype.Chats)
            {
                if (_closing) return;

                i++;

                if (chat.Messages.Count > 0)
                {

                    LabelText = string.Format("[chat {0} of {1}] [{2}] {3} [messages {4}]", i, count, chat.Timestamp.ToString(), chat.DialogPartner, chat.Messages.Count);

                    // get chat members
                    ExportMemberCollection memberCollection = new ExportMemberCollection();

                    foreach (SKYPE4COMLib.IUser chatMember in chat.Members)
                    {
                        if (_closing) return;

                        ExportMember member = new ExportMember();
                        member.Name = chatMember.Handle;
                        member.DisplayName = chatMember.FullName;

                        if (!memberCollection.MemberExists(member.Name))
                        {
                            memberCollection.Add(member);
                        }
                    }

                    ExportChat exportChat = ExportCM.Configuration.Chats.GetChat(memberCollection);

                    if (exportChat == null)
                    {
                        exportChat = new ExportChat();

                        ExportCM.Configuration.Chats.Add(exportChat);

                        MainText = "[+] " + memberCollection.ToString();

                        exportChat.Name = memberCollection.ToString();
                        exportChat.Members = memberCollection;
                    }
                    else
                    {
                        MainText = "[ ] " + memberCollection.ToString();
                    }

                    foreach (SKYPE4COMLib.IChatMessage chatMessage in chat.Messages)
                    {
                        if (_closing) return;

                        ExportMessage exportMessage = new ExportMessage();
                        exportMessage.Id = chatMessage.Id.ToString();
                        exportMessage.From = chatMessage.FromHandle;

                        exportMessage.Timestamp = chatMessage.Timestamp.ToString();
                        exportMessage.Body = chatMessage.Body;

                        //LabelText = string.Format("[{0}:{1}] [{2} -> {3}]: {4}", exportMessage.Id, exportMessage.Timestamp, exportMessage.From, exportMessage.To, exportMessage.Body);

                        if (!string.IsNullOrEmpty(chatMessage.EditedBy))
                        {
                            exportMessage.EditBy = chatMessage.EditedBy;
                        }

                        exportMessage.MessageType = chatMessage.Type.ToString();

                        if (!exportChat.Messages.MessageExists(exportMessage))
                        {
                            exportChat.Messages.Add(exportMessage);
                        }

                        // -----
                        //break;
                    }
                }
            }

            LabelText = "";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Init();

            try
            {
                ExportCM.Load();
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;

                ExportCM.New();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ExportCM.Save();
                MainText = "Saved.";
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
            }
        }

        private void btnFastLoad_Click(object sender, EventArgs e)
        {
            ThreadingJob();
        }

        private void FastLoad()
        {
            LoadChats();
        }

        private void ThreadingJob()
        {
            Init();

            _thread.Start();
        }

        int index = 0;

        private void lstBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBox.SelectedIndex != index)
            {
                index = lstBox.SelectedIndex;

                ExportChat chat = (ExportChat)lstBox.SelectedItem;

                if (chat != null && chat.Messages != null)
                {
                    lstMessages.DataSource = null;
                    lstMessages.DataSource = chat.Messages;
                    lstMessages.DisplayMember = "Display";
                }
            }
        }
    }
}
