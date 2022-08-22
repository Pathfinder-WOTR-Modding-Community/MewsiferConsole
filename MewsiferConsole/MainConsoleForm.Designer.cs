namespace MewsiferConsole
{
    partial class MainConsoleForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.topPanelTable = new System.Windows.Forms.TableLayoutPanel();
            this.errCount = new System.Windows.Forms.Label();
            this.warnCount = new System.Windows.Forms.Label();
            this.infoCount = new System.Windows.Forms.Label();
            this.shownCount = new System.Windows.Forms.Label();
            this.OmniFilter = new System.Windows.Forms.TextBox();
            this.TailToggle = new System.Windows.Forms.CheckBox();
            this.Clear = new System.Windows.Forms.Button();
            this.verCount = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logTable = new System.Windows.Forms.DataGridView();
            this.detailView = new MewsiferConsole.LogEventView();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.Severity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChannelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.topPanelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.topPanelTable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1664, 1011);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // topPanelTable
            // 
            this.topPanelTable.ColumnCount = 8;
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.topPanelTable.Controls.Add(this.errCount, 4, 0);
            this.topPanelTable.Controls.Add(this.warnCount, 5, 0);
            this.topPanelTable.Controls.Add(this.infoCount, 6, 0);
            this.topPanelTable.Controls.Add(this.shownCount, 3, 0);
            this.topPanelTable.Controls.Add(this.OmniFilter, 0, 0);
            this.topPanelTable.Controls.Add(this.TailToggle, 1, 0);
            this.topPanelTable.Controls.Add(this.Clear, 2, 0);
            this.topPanelTable.Controls.Add(this.verCount, 7, 0);
            this.topPanelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanelTable.Location = new System.Drawing.Point(2, 2);
            this.topPanelTable.Margin = new System.Windows.Forms.Padding(2);
            this.topPanelTable.Name = "topPanelTable";
            this.topPanelTable.RowCount = 1;
            this.topPanelTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topPanelTable.Size = new System.Drawing.Size(1660, 42);
            this.topPanelTable.TabIndex = 1;
            // 
            // errCount
            // 
            this.errCount.AutoSize = true;
            this.errCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errCount.Location = new System.Drawing.Point(1263, 0);
            this.errCount.Name = "errCount";
            this.errCount.Size = new System.Drawing.Size(94, 42);
            this.errCount.TabIndex = 2;
            this.errCount.Text = "E: 0";
            this.errCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // warnCount
            // 
            this.warnCount.AutoSize = true;
            this.warnCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.warnCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.warnCount.Location = new System.Drawing.Point(1363, 0);
            this.warnCount.Name = "warnCount";
            this.warnCount.Size = new System.Drawing.Size(94, 42);
            this.warnCount.TabIndex = 3;
            this.warnCount.Text = "W: 0";
            this.warnCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // infoCount
            // 
            this.infoCount.AutoSize = true;
            this.infoCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.infoCount.Location = new System.Drawing.Point(1463, 0);
            this.infoCount.Name = "infoCount";
            this.infoCount.Size = new System.Drawing.Size(94, 42);
            this.infoCount.TabIndex = 4;
            this.infoCount.Text = "I: 0";
            this.infoCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shownCount
            // 
            this.shownCount.AutoSize = true;
            this.shownCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shownCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.shownCount.Location = new System.Drawing.Point(1113, 0);
            this.shownCount.Name = "shownCount";
            this.shownCount.Size = new System.Drawing.Size(144, 42);
            this.shownCount.TabIndex = 5;
            this.shownCount.Text = "0/0";
            this.shownCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OmniFilter
            // 
            this.OmniFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.OmniFilter.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OmniFilter.Location = new System.Drawing.Point(2, 3);
            this.OmniFilter.Margin = new System.Windows.Forms.Padding(2);
            this.OmniFilter.Name = "OmniFilter";
            this.OmniFilter.Size = new System.Drawing.Size(946, 35);
            this.OmniFilter.TabIndex = 1;
            // 
            // TailToggle
            // 
            this.TailToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.TailToggle.Checked = true;
            this.TailToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TailToggle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TailToggle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TailToggle.Location = new System.Drawing.Point(952, 2);
            this.TailToggle.Margin = new System.Windows.Forms.Padding(2);
            this.TailToggle.Name = "TailToggle";
            this.TailToggle.Size = new System.Drawing.Size(76, 38);
            this.TailToggle.TabIndex = 0;
            this.TailToggle.Text = "Tail";
            this.TailToggle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TailToggle.UseVisualStyleBackColor = true;
            // 
            // Clear
            // 
            this.Clear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Clear.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Clear.Location = new System.Drawing.Point(1033, 3);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(74, 36);
            this.Clear.TabIndex = 6;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // verCount
            // 
            this.verCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.verCount.AutoSize = true;
            this.verCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.verCount.Location = new System.Drawing.Point(1583, 8);
            this.verCount.Name = "verCount";
            this.verCount.Size = new System.Drawing.Size(54, 25);
            this.verCount.TabIndex = 7;
            this.verCount.Text = "V: 0";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 49);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.logTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.detailView);
            this.splitContainer1.Size = new System.Drawing.Size(1658, 959);
            this.splitContainer1.SplitterDistance = 692;
            this.splitContainer1.TabIndex = 2;
            // 
            // logTable
            // 
            this.logTable.AllowUserToAddRows = false;
            this.logTable.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.logTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.logTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Severity,
            this.ChannelName});
            this.logTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTable.Location = new System.Drawing.Point(0, 0);
            this.logTable.Margin = new System.Windows.Forms.Padding(2);
            this.logTable.Name = "logTable";
            this.logTable.ReadOnly = true;
            this.logTable.RowHeadersVisible = false;
            this.logTable.RowHeadersWidth = 62;
            this.logTable.RowTemplate.Height = 33;
            this.logTable.Size = new System.Drawing.Size(1658, 692);
            this.logTable.TabIndex = 0;
            this.logTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // detailView
            // 
            this.detailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailView.Location = new System.Drawing.Point(0, 0);
            this.detailView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.detailView.Name = "detailView";
            this.detailView.Size = new System.Drawing.Size(1658, 263);
            this.detailView.TabIndex = 0;
            this.detailView.ViewModel = null;
            // 
            // tooltip
            // 
            this.tooltip.IsBalloon = true;
            this.tooltip.ShowAlways = true;
            this.tooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // Severity
            // 
            this.Severity.DataPropertyName = "Severity";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Severity.DefaultCellStyle = dataGridViewCellStyle2;
            this.Severity.HeaderText = "L";
            this.Severity.MinimumWidth = 55;
            this.Severity.Name = "Severity";
            this.Severity.ReadOnly = true;
            this.Severity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Severity.ToolTipText = "Log Severity";
            this.Severity.Width = 55;
            // 
            // ChannelName
            // 
            this.ChannelName.DataPropertyName = "ChannelName";
            this.ChannelName.HeaderText = "Channel";
            this.ChannelName.MinimumWidth = 300;
            this.ChannelName.Name = "ChannelName";
            this.ChannelName.ReadOnly = true;
            this.ChannelName.Width = 300;
            // 
            // MainConsoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1664, 1011);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainConsoleForm";
            this.Text = "MewsiferConsole";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.topPanelTable.ResumeLayout(false);
            this.topPanelTable.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logTable)).EndInit();
            this.ResumeLayout(false);

        }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
        private DataGridView logTable;
        private TableLayoutPanel topPanelTable;
        private CheckBox TailToggle;
        private TextBox OmniFilter;
        private Label errCount;
        private Label warnCount;
        private Label infoCount;
        private Label shownCount;
        private ToolTip tooltip;
    private Button Clear;
    private Label verCount;
        private SplitContainer splitContainer1;
        private MewsiferConsole.LogEventView detailView;
    private DataGridViewTextBoxColumn Severity;
    private DataGridViewTextBoxColumn ChannelName;
  }
}