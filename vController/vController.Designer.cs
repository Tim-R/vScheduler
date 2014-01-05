namespace vMixControler
{
    partial class vMixControler
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(vMixControler));
            this.event_title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.event_start = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.event_duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.event_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.event_path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvEventList = new System.Windows.Forms.ListView();
            this.column_title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_start = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_connected = new System.Windows.Forms.Label();
            this.bn_connect_ = new System.Windows.Forms.Button();
            this.bn_showpreferences_ = new System.Windows.Forms.Button();
            this.bn_load_schedule_ = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lbl_clock = new System.Windows.Forms.ToolStripLabel();
            this.bn_load_schedule = new System.Windows.Forms.ToolStripButton();
            this.bn_showpreferences = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // event_title
            // 
            this.event_title.Text = "Title";
            this.event_title.Width = 120;
            // 
            // event_start
            // 
            this.event_start.Text = "Start";
            this.event_start.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // event_duration
            // 
            this.event_duration.Text = "Duration";
            this.event_duration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // event_type
            // 
            this.event_type.Text = "Type";
            this.event_type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // event_path
            // 
            this.event_path.Text = "Path";
            this.event_path.Width = 300;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Title";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Start";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Duration";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Path";
            this.columnHeader5.Width = 300;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Title";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Start";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Duration";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Type";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Path";
            this.columnHeader10.Width = 300;
            // 
            // lvEventList
            // 
            this.lvEventList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvEventList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_title,
            this.column_start,
            this.column_duration,
            this.column_type});
            this.lvEventList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEventList.FullRowSelect = true;
            this.lvEventList.GridLines = true;
            this.lvEventList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvEventList.HideSelection = false;
            this.lvEventList.Location = new System.Drawing.Point(0, 0);
            this.lvEventList.Margin = new System.Windows.Forms.Padding(1);
            this.lvEventList.MultiSelect = false;
            this.lvEventList.Name = "lvEventList";
            this.lvEventList.Size = new System.Drawing.Size(667, 263);
            this.lvEventList.TabIndex = 2;
            this.lvEventList.UseCompatibleStateImageBehavior = false;
            this.lvEventList.View = System.Windows.Forms.View.Details;
            this.lvEventList.VirtualMode = true;
            this.lvEventList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvEventList_RetrieveVirtualItem);
            // 
            // column_title
            // 
            this.column_title.Text = "Title";
            this.column_title.Width = 223;
            // 
            // column_start
            // 
            this.column_start.Text = "Start";
            this.column_start.Width = 179;
            // 
            // column_duration
            // 
            this.column_duration.Text = "Duration";
            this.column_duration.Width = 125;
            // 
            // column_type
            // 
            this.column_type.Text = "Type";
            this.column_type.Width = 138;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbl_connected);
            this.panel1.Controls.Add(this.bn_connect_);
            this.panel1.Controls.Add(this.bn_showpreferences_);
            this.panel1.Controls.Add(this.bn_load_schedule_);
            this.panel1.Location = new System.Drawing.Point(524, 113);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 149);
            this.panel1.TabIndex = 13;
            // 
            // lbl_connected
            // 
            this.lbl_connected.BackColor = System.Drawing.Color.Transparent;
            this.lbl_connected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_connected.ForeColor = System.Drawing.Color.Red;
            this.lbl_connected.Location = new System.Drawing.Point(10, 273);
            this.lbl_connected.Name = "lbl_connected";
            this.lbl_connected.Size = new System.Drawing.Size(98, 29);
            this.lbl_connected.TabIndex = 17;
            this.lbl_connected.Text = "OFFLINE";
            this.lbl_connected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bn_connect_
            // 
            this.bn_connect_.Dock = System.Windows.Forms.DockStyle.Top;
            this.bn_connect_.Location = new System.Drawing.Point(0, 37);
            this.bn_connect_.Name = "bn_connect_";
            this.bn_connect_.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.bn_connect_.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bn_connect_.Size = new System.Drawing.Size(107, 37);
            this.bn_connect_.TabIndex = 30;
            this.bn_connect_.Text = "Connect to vMix";
            this.bn_connect_.UseVisualStyleBackColor = true;
            // 
            // bn_showpreferences_
            // 
            this.bn_showpreferences_.Dock = System.Windows.Forms.DockStyle.Top;
            this.bn_showpreferences_.Location = new System.Drawing.Point(0, 0);
            this.bn_showpreferences_.Name = "bn_showpreferences_";
            this.bn_showpreferences_.Size = new System.Drawing.Size(107, 37);
            this.bn_showpreferences_.TabIndex = 14;
            this.bn_showpreferences_.Text = "Settings";
            this.bn_showpreferences_.UseVisualStyleBackColor = true;
            this.bn_showpreferences_.Click += new System.EventHandler(this.bn_showpreferences_Click);
            // 
            // bn_load_schedule_
            // 
            this.bn_load_schedule_.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bn_load_schedule_.Location = new System.Drawing.Point(0, 110);
            this.bn_load_schedule_.Name = "bn_load_schedule_";
            this.bn_load_schedule_.Size = new System.Drawing.Size(107, 37);
            this.bn_load_schedule_.TabIndex = 13;
            this.bn_load_schedule_.Text = "Reload Schedule";
            this.bn_load_schedule_.UseVisualStyleBackColor = true;
            this.bn_load_schedule_.Click += new System.EventHandler(this.bn_load_schedule_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_clock,
            this.bn_load_schedule,
            this.bn_showpreferences});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(667, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lbl_clock
            // 
            this.lbl_clock.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lbl_clock.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbl_clock.Name = "lbl_clock";
            this.lbl_clock.Size = new System.Drawing.Size(0, 22);
            // 
            // bn_load_schedule
            // 
            this.bn_load_schedule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bn_load_schedule.Image = ((System.Drawing.Image)(resources.GetObject("bn_load_schedule.Image")));
            this.bn_load_schedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bn_load_schedule.Name = "bn_load_schedule";
            this.bn_load_schedule.Size = new System.Drawing.Size(23, 22);
            this.bn_load_schedule.Text = "Reload Schedule";
            this.bn_load_schedule.Click += new System.EventHandler(this.bn_load_schedule_Click);
            // 
            // bn_showpreferences
            // 
            this.bn_showpreferences.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bn_showpreferences.Image = ((System.Drawing.Image)(resources.GetObject("bn_showpreferences.Image")));
            this.bn_showpreferences.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bn_showpreferences.Name = "bn_showpreferences";
            this.bn_showpreferences.Size = new System.Drawing.Size(23, 22);
            this.bn_showpreferences.Text = "Settings";
            this.bn_showpreferences.Click += new System.EventHandler(this.bn_showpreferences_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvEventList);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(667, 263);
            this.panel2.TabIndex = 15;
            // 
            // vMixControler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 288);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(464, 262);
            this.Name = "vMixControler";
            this.Text = "vController";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.vMixControler_FormClosing);
            this.Load += new System.EventHandler(this.vMaster_Load);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader event_title;
        private System.Windows.Forms.ColumnHeader event_start;
        private System.Windows.Forms.ColumnHeader event_duration;
        private System.Windows.Forms.ColumnHeader event_type;
        private System.Windows.Forms.ColumnHeader event_path;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ListView lvEventList;
        private System.Windows.Forms.ColumnHeader column_title;
        private System.Windows.Forms.ColumnHeader column_start;
        private System.Windows.Forms.ColumnHeader column_duration;
        private System.Windows.Forms.ColumnHeader column_type;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bn_load_schedule_;
        private System.Windows.Forms.Button bn_showpreferences_;
        private System.Windows.Forms.Button bn_connect_;
        private System.Windows.Forms.Label lbl_connected;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel lbl_clock;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton bn_showpreferences;
        private System.Windows.Forms.ToolStripButton bn_load_schedule;
    }
}

