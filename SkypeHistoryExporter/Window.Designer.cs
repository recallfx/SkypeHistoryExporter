namespace SkypeHistoryExporter
{
    partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lstErrors = new System.Windows.Forms.ListBox();
            this.lstBox = new System.Windows.Forms.ListBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFastLoad = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lstMessages = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // lstErrors
            // 
            this.lstErrors.BackColor = System.Drawing.SystemColors.Info;
            this.lstErrors.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.lstErrors.FormattingEnabled = true;
            this.lstErrors.ItemHeight = 14;
            this.lstErrors.Location = new System.Drawing.Point(0, 0);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.ScrollAlwaysVisible = true;
            this.lstErrors.Size = new System.Drawing.Size(727, 46);
            this.lstErrors.TabIndex = 5;
            // 
            // lstBox
            // 
            this.lstBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.lstBox.FormattingEnabled = true;
            this.lstBox.ItemHeight = 14;
            this.lstBox.Location = new System.Drawing.Point(12, 52);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(258, 312);
            this.lstBox.TabIndex = 6;
            this.lstBox.SelectedIndexChanged += new System.EventHandler(this.lstBox_SelectedIndexChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 377);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Load";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(174, 377);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnFastLoad
            // 
            this.btnFastLoad.Location = new System.Drawing.Point(93, 377);
            this.btnFastLoad.Name = "btnFastLoad";
            this.btnFastLoad.Size = new System.Drawing.Size(75, 23);
            this.btnFastLoad.TabIndex = 9;
            this.btnFastLoad.Text = "Export";
            this.btnFastLoad.UseVisualStyleBackColor = true;
            this.btnFastLoad.Click += new System.EventHandler(this.btnFastLoad_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.lblStatus.Location = new System.Drawing.Point(255, 382);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(460, 18);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Start";
            // 
            // lstMessages
            // 
            this.lstMessages.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lstMessages.FormattingEnabled = true;
            this.lstMessages.Location = new System.Drawing.Point(276, 52);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(439, 316);
            this.lstMessages.TabIndex = 12;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 412);
            this.Controls.Add(this.lstMessages);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnFastLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lstBox);
            this.Controls.Add(this.lstErrors);
            this.Name = "Window";
            this.Text = "Skype history exporter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ListBox lstErrors;
        private System.Windows.Forms.ListBox lstBox;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnFastLoad;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ListBox lstMessages;
    }
}

